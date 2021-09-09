import React, { useEffect, useState } from "react";
import {
  Button,
  Row,
  Col,
  Label,
  Card,
  CardBody,
} from "reactstrap";
import { GamePick, GameResult } from "../../Apis";
import { useUpdateGamePicks } from "../../Data/GamePicks/useUpdateGamePicks";
import { TeamDisplay } from "../Teams/TeamDisplay";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";

type Props = {
  gamePick: GamePick;
  onPickChanged: (result: GameResult) => void;
};

export const PickSelector: React.FC<Props> = ({ gamePick, onPickChanged }) => {
  const [pick, setPick] = useState<GamePick>(gamePick);

  const UpdatePick = async (result: GameResult) => {
    setPick({ ...pick, pick: result });
  };

  useEffect(() => {
    async function UpdateGamePick() {
      useUpdateGamePicks([pick]);
    }
    UpdateGamePick();
  }, [pick]);

  const getColor = (actualPick: GameResult | undefined,
    expectedPick: GameResult) => {
      return (actualPick === expectedPick) ? "primary" : "secondary";
    }

  return (
    <Card>
      <CardBody>
        <Row>
          <Col className="col-2">
            <Label>{pick.wager}</Label>
          </Col>
          <Col className="col-5 text-center">
            <Button
              color={getColor(pick.pick, GameResult.HomeWin)}
              onClick={() => UpdatePick(GameResult.HomeWin)}
            >
              <TeamDisplay id={pick.game?.homeTeamId}></TeamDisplay>
            </Button>
          </Col>
          <Col className="col-5 text-center">
            <Button
              color={getColor(pick.pick, GameResult.AwayWin)}
              onClick={() => UpdatePick(GameResult.AwayWin)}
            >
              <TeamDisplay id={pick.game?.awayTeamId}></TeamDisplay>
            </Button>
          </Col>
        </Row>
      </CardBody>
    </Card>
  );
};
