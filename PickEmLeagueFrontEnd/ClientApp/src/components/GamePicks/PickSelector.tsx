import React, { useContext, useEffect, useState } from "react";
import { Button, Row, Col, Label, Card, CardBody } from "reactstrap";
import { GamePick, GameResult } from "../../Apis";
import { useUpdateGamePicks } from "../../Data/GamePicks/useUpdateGamePicks";
import { TeamDisplay } from "../Teams/TeamDisplay";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { GamePickContext } from "../../Data/Contexts/GamePickContext";
import { UserContext } from "../../Data/Contexts/UserContext";

export const PickSelector = () => {
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
    return actualPick === expectedPick ? "primary" : "secondary";
  };

  const editable = () => {
    return gamePick.editable || user?.isAdmin;
  }

  return (
    <Card color={gamePick.editable ? "light" : "secondary"}>
      <CardBody>
        <Row>
          <Col className="col-2">
            <Label>{gamePick.wager}</Label>
          </Col>
          <Col className="col-4 text-center">
            <Button
              color={getColor(gamePick.pick, GameResult.HomeWin)}
              onClick={() => UpdatePick(GameResult.HomeWin)}
              disabled={!editable()}
            >
              <TeamDisplay id={gamePick.game?.homeTeamId}></TeamDisplay>
            </Button>
          </Col>
          <Col className="col-4 text-center">
            <Button
              color={getColor(gamePick.pick, GameResult.AwayWin)}
              onClick={() => UpdatePick(GameResult.AwayWin)}
              disabled={!editable()}
            >
              <TeamDisplay id={gamePick.game?.awayTeamId}></TeamDisplay>
            </Button>
          </Col>
          <Col className="col-2 text-center">
            <Label>{gamePick.editable ? "" : "Locked"}</Label>
          </Col>
        </Row>
      </CardBody>
    </Card>
  );
};
