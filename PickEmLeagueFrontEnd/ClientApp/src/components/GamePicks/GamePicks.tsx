import React, { useContext, useEffect, useState } from "react";
import { DragDropContext, Droppable, DropResult } from "react-beautiful-dnd";
import { useUserContext } from "../../Data/Contexts/UserContext";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { GamePick } from "../../Apis";
import { useGetGamePicksByUserAndWeek } from "../../Data/GamePicks/useGetGamePicksByUserAndWeek";
import { SortablePickRow } from "./SortablePickRow";
import { Col, Container, Row } from "reactstrap";
import { useUpdateGamePicks } from "../../Data/GamePicks/useUpdateGamePicks";

// a little function to help us with reordering the result
const reorder = (
  list: GamePick[],
  startIndex: number,
  endIndex: number
): GamePick[] => {
  const result = Array.from(list);
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);

  return result;
};

const grid = 8;

const getListStyle = (isDraggingOver: boolean): React.CSSProperties => ({
  background: isDraggingOver ? "lightblue" : "lightgrey",
  padding: grid,
  //width: 250
});

export function GamePicks() {
  const { user } = useUserContext();
  const weekContext = useContext(WeekContext);
  const [gamePicks, setGamePicks] = useState<GamePick[]>([]);

  useEffect(() => {
    async function GetGamePicks() {
      let picks = await useGetGamePicksByUserAndWeek(
        user?.id!,
        weekContext.week!
      );
      picks.sort((a, b) => (a.wager! < b.wager! ? 1 : -1));
      setGamePicks(picks);
    }
    GetGamePicks();
  }, [user, weekContext.week]);

  const onDragEnd = (result: DropResult): void => {
    // dropped outside the list
    if (!result.destination) {
      return;
    }

    const picks: GamePick[] = reorder(
      gamePicks,
      result.source.index,
      result.destination.index
    );

    for (let i = 0; i < picks.length; i++) {
      picks[i].wager = picks.length - i;
    }

    setGamePicks(picks);

    // TODO: Getting away with this since we are using picks
    // but the setGamePicks call is async so the state may not be
    // updated. Normally we use useEffect, but that handles the getter.
    // Need to research how to handle both getter and setting in useEffect
    // Looks like multiple useEffects is allowed
    SavePicks(picks);
  };

  const SavePicks = async (picks: GamePick[]): Promise<void> => {
    await useUpdateGamePicks(picks);
  };

  // Normally you would want to split things out into separate components.
  // But in this example everything is just done in one place for simplicity
  return (
    <Container>
      <Row>
        <Col className="col-2 text-center">Wager</Col>
        <Col className="col-5 text-center">Home Team</Col>
        <Col className="col-5 text-center">Away Team</Col>
      </Row>
      <DragDropContext onDragEnd={onDragEnd}>
        <Droppable droppableId="droppable">
          {(provided, snapshot): JSX.Element => (
            <div
              {...provided.droppableProps}
              ref={provided.innerRef}
              style={getListStyle(snapshot.isDraggingOver)}
            >
              {gamePicks?.map((gamePick, index) => (
                <SortablePickRow
                  key={gamePick.id}
                  gamePick={gamePick}
                  index={index}
                  
                ></SortablePickRow>
              ))}
              {provided.placeholder}
            </div>
          )}
        </Droppable>
      </DragDropContext>
    </Container>
  );
}
