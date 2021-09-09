import React from "react";
import {
  Draggable,
  DraggingStyle,
  NotDraggingStyle,
} from "react-beautiful-dnd";
import { PickSelector } from "./PickSelector";
import { GamePick, GameResult } from "../../Apis";

type Props = {
  gamePick: GamePick;
  index: number;
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
  return (
    // <div ref={setNodeRef} className="moveable-item" {...attributes} {...listeners}>
    <Draggable
      key={props.gamePick.id}
      draggableId={props.gamePick.id?.toString()!}
      index={props.index}
      //TODO: time based disabled
      // isDragDisabled={props.index == 2}
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
          <PickSelector
            gamePick={props.gamePick}
            onPickChanged={(e: GameResult) => (props.gamePick.pick = e)}
          ></PickSelector>
        </div>
      )}
    </Draggable>
    // </div>
  );
}
