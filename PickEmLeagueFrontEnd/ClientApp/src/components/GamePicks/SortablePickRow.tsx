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
  setGamePick: (gamePick: GamePick) => void;
};

const grid = 8;

const getItemStyle = (
  isDragging: boolean,
  draggableStyle: DraggingStyle | NotDraggingStyle | undefined
): React.CSSProperties => ({
  // some basic styles to make the items look a bit nicer
  userSelect: "none",
  padding: grid * 2,
  margin: `0 0 ${grid}px 0`,

  // change background colour if dragging
  background: isDragging ? "lightgreen" : "",

  // styles we need to apply on draggables
  ...draggableStyle,
});

export function SortablePickRow(props: Props) {
  const { user } = useContext(UserContext);

  const editable = () => {
    return props.gamePick.editable || user?.isAdmin;
  }

  return (
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
            provided.draggableProps.style
          )}
        >
          <GamePickContext.Provider
            value={{ gamePick: props.gamePick, setGamePick: props.setGamePick }}
          >
            <PickSelector />
          </GamePickContext.Provider>
        </div>
      )}
    </Draggable>
  );
}
