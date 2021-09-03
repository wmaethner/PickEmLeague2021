import React, { FormEvent, useEffect, useState } from "react";
import { Container, Form, Row } from "react-bootstrap";
import { useHistory, useParams } from "react-router-dom";
import { Game } from "../../Apis/models/Game";
import { useEditGame } from "../../Data/Game/useEditGame";
import { useGetGame } from "../../Data/Game/useGetGame";

import DateTimePicker from "@mui/lab/DateTimePicker";

import "react-datepicker/dist/react-datepicker.css";
import TextField from "@mui/material/TextField";

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

  return (
    <Container>
      <Form onSubmit={handleSubmit}>
        <Row>
          <label>
            Home Team:
            <input
              type="number"
              value={game.homeTeam || 0}
              onChange={(e) =>
                setGame({ ...game, homeTeam: Number.parseInt(e.target.value) })
              }
              name="homeTeam"
            />
          </label>
        </Row>
        <Row>
          <label>
            Away Team:
            <input
              type="number"
              value={game.awayTeam || 0}
              onChange={(e) =>
                setGame({ ...game, awayTeam: Number.parseInt(e.target.value) })
              }
              name="awayTeam"
            />
          </label>
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
