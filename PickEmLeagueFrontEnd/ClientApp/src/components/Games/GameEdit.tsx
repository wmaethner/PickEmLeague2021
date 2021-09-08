import React, { FormEvent, useEffect, useState } from "react";
import { Container, Form, Row, Col } from "react-bootstrap";
import { useHistory, useParams } from "react-router-dom";
import { Game } from "../../Apis/models/Game";
import { useEditGame } from "../../Data/Game/useEditGame";
import { useGetGame } from "../../Data/Game/useGetGame";

import DateTimePicker from "@mui/lab/DateTimePicker";

import "react-datepicker/dist/react-datepicker.css";
import TextField from "@mui/material/TextField";
import { TeamSelector } from "../Teams/TeamSelector";
import { Label } from "reactstrap";

type GameEditParams = {
  gameId: string;
};

export function GameEdit() {
  const history = useHistory();
  const [game, setGame] = useState<Game>({});
  const id = parseInt(useParams<GameEditParams>().gameId);

  useEffect(() => {
    async function GetGame() {
      setGame(await useGetGame(id));
    }
    GetGame();
  }, [id]);

  const handleSubmit = async (evt: FormEvent) => {
    evt.preventDefault();
    SaveGame(game);
    history.goBack();
  };

  const handleHomeTeamChange = (id: any) => {
    setGame({ ...game, homeTeamId: id.value });
  };
  const handleAwayTeamChange = (id: any) => {
    setGame({ ...game, awayTeamId: id.value });
  };

  return (
    <Container>
      <Form onSubmit={handleSubmit}>
        <Row>
          <Col>
            <Label>Home Team: </Label>
          </Col>
          <Col>
            <TeamSelector
              id={game.homeTeamId}
              onTeamChanged={handleHomeTeamChange}
            />
          </Col>
        </Row>
        <Row>
          <Col>
            <Label>Away Team: </Label>
          </Col>
          <Col>
            <TeamSelector
              id={game.awayTeamId}
              onTeamChanged={handleAwayTeamChange}
            />
          </Col>
        </Row>
        <Row>
          <label>
            Week:
            <input
              type="number"
              value={game.week || 0}
              onChange={(e) =>
                setGame({ ...game, week: Number.parseInt(e.target.value) })
              }
              name="week"
            />
          </label>
        </Row>
        <Row>
          <label>
            Game Time:
            <DateTimePicker
              renderInput={(props) => <TextField {...props} />}
              label="DateTimePicker"
              value={game.gameTime}
              onChange={(newValue) => {
                setGame({ ...game, gameTime: newValue ?? undefined });
              }}
            />
          </label>
        </Row>
        <input type="submit" value="Save" />
      </Form>
    </Container>
  );
}

async function SaveGame(game: Game) {
  await useEditGame(game);
}
