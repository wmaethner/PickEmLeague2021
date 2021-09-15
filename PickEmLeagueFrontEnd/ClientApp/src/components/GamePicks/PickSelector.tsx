import React, { useContext, useEffect, useState } from "react";
import { Button, Row, Col, Label, Card, CardBody } from "reactstrap";
import { GamePick, GameResult } from "../../Apis";
import { useUpdateGamePicks } from "../../Data/GamePicks/useUpdateGamePicks";
import { TeamDisplay } from "../Teams/TeamDisplay";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { GamePickContext } from "../../Data/Contexts/GamePickContext";
import { UserContext } from "../../Data/Contexts/UserContext";

type Props = {
  ignoreLock: boolean;
};
export function PickSelector(props: Props) {
  const { user } = useContext(UserContext);
  const { gamePick, setGamePick } = useContext(GamePickContext);

  const UpdatePick = async (result: GameResult) => {
    setGamePick({ ...gamePick, pick: result });
  };

  useEffect(() => {
    async function UpdateGamePick() {
      useUpdateGamePicks([gamePick]);
    }
    UpdateGamePick();
  }, [gamePick, gamePick.pick]);

  const getColor = (
    actualPick: GameResult | undefined,
    expectedPick: GameResult
  ) => {
    return actualPick === expectedPick ? "primary" : "light";
  };

  const editable = () => {
    return gamePick.editable || props.ignoreLock; //user?.isAdmin;
  };

  return (
    // <Card color={gamePick.editable ? "light" : "secondary"}>
    //   <CardBody>
    <Row color={gamePick.editable ? "light" : "secondary"}>
      <Col className="col-2">
        <Label>{gamePick.wager}</Label>
      </Col>
      <Col className="col-4 text-center">
        <div className="d-grid gap-2">
          <Button
            color={getColor(gamePick.pick, GameResult.HomeWin)}
            className="border border-dark"
            onClick={() => UpdatePick(GameResult.HomeWin)}
            disabled={!editable()}
          >
            <TeamDisplay id={gamePick.game?.homeTeamId}></TeamDisplay>
          </Button>
        </div>
      </Col>
      <Col className="col-4 text-center">
        <div className="d-grid gap-2">
          <Button
            color={getColor(gamePick.pick, GameResult.AwayWin)}
            className="border border-dark"
            onClick={() => UpdatePick(GameResult.AwayWin)}
            disabled={!editable()}
          >
            <TeamDisplay id={gamePick.game?.awayTeamId}></TeamDisplay>
          </Button>
        </div>
      </Col>
      <Col className="col-2 text-center">
        <Label>{gamePick.editable ? "" : "Locked"}</Label>
      </Col>
    </Row>
    //   </CardBody>
    // </Card>
  );
}
