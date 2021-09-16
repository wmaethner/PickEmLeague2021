import React, { useContext, useState } from "react";
import {
  Draggable,
  DraggingStyle,
  NotDraggingStyle,
} from "react-beautiful-dnd";
import { PickSelector } from "./PickSelector";
import { GamePick, GameResult } from "../../Apis";
import { GamePickContext } from "../../Data/Contexts/GamePickContext";
import { UserContext } from "../../Data/Contexts/UserContext";

type Props = {
  gamePick: GamePick;
  index: number;
  ignoreLock: boolean;
  setGamePick: (gamePick: GamePick) => void;
};

const grid = 4;

const getItemStyle = (
  isDragging: boolean,
  draggableStyle: DraggingStyle | NotDraggingStyle | undefined,
  gamePick: GamePick
): React.CSSProperties => ({
  // some basic styles to make the items look a bit nicer
  userSelect: "none",
  padding: grid * 2,
  margin: `0 0 ${grid}px 0`,

  // change background colour if dragging
  background: isDragging
    ? "lightgreen"
    : gamePick.game?.gameResult === GameResult.NotPlayed
    ? ""
    : gamePick.correctPick
    ? "green"
    : "red",

  // styles we need to apply on draggables
  ...draggableStyle,
});

export function SortablePickRow(props: Props) {
  const { user } = useContext(UserContext);

  const editable = () => {
    return props.gamePick.editable || user?.isAdmin;
  };

  const getDivClass = () => {
    let style = "border border-dark ";
    style += editable() ? "bg-light" : "bg-secondary";
    return style;
  };

  return (
    <div className={getDivClass()}>
      <Draggable
        key={props.gamePick.id}
        draggableId={props.gamePick.id?.toString()!}
        index={props.index}
        //TODO: time based disabled
        isDragDisabled={!editable()}
      >
        {(provided, snapshot): JSX.Element => (
          <div
            ref={provided.innerRef}
            {...provided.draggableProps}
            {...provided.dragHandleProps}
            style={getItemStyle(
              snapshot.isDragging,
              provided.draggableProps.style,
              props.gamePick
            )}
          >
            <GamePickContext.Provider
              value={{
                gamePick: props.gamePick,
                setGamePick: props.setGamePick,
              }}
            >
              <PickSelector ignoreLock={props.ignoreLock} />
            </GamePickContext.Provider>
          </div>
        )}
      </Draggable>
    </div>
  );
}
