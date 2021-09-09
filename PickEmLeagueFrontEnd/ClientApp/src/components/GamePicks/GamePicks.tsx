import React, { useContext, useEffect, useState } from "react";
import { DragDropContext, Droppable, DropResult } from "react-beautiful-dnd";
import { useUserContext } from "../../Data/Contexts/UserContext";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { GamePick } from "../../Apis";
import { useGetGamePicksByUserAndWeek } from "../../Data/GamePicks/useGetGamePicksByUserAndWeek";
import { SortablePickRow } from "./SortablePickRow";
import { Col, Container, Row } from "reactstrap";
import { useUpdateGamePicks } from "../../Data/GamePicks/useUpdateGamePicks";
import { GamePickContext } from "../../Data/Contexts/GamePickContext";

// a little function to help us with reordering the result
const reorder = (
  list: GamePick[],
  startIndex: number,
  endIndex: number
): GamePick[] => {
  // Result will be the intended order
  // Next check for the locked rows
  const result = Array.from(list);
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);

  const copy = Array.from(result);
  const final = Array(result.length);

  // First put all locked picks in
  for (let i = 0; i < result.length; i++) {
    if (!result[i].editable) {
      let index = copy.findIndex((x) => x.id === result[i].id);
      const [pick] = copy.splice(index, 1);
      final[result.length - pick.wager!] = pick;
    }
  }

  for (let i = 0; i < copy.length; i++) {
    for (let j = 0; j < final.length; j++) {
      if (final[j] === undefined) {
        final[j] = copy[i];
        break;
      }
    }
  }

  return final;
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

  const setSingleGamePick = (gamePick: GamePick) => {
    console.log("Updating game pick " + gamePick.id);
    let index = gamePicks.findIndex((gp) => gp.id === gamePick.id);
    let newArr = [...gamePicks];
    newArr[index] = gamePick;
    setGamePicks(newArr);
  };

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
    <Container className="data-table">
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
                  setGamePick={setSingleGamePick}
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
