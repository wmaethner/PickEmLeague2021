import React, { FormEvent, useEffect, useState } from "react";
import { Container, Form } from "react-bootstrap";
import { useHistory, useParams } from "react-router-dom";
import { Game } from "../../Apis/models/Game";
import { GameResult } from "../../Apis";
import { useEditGame } from "../../Data/Game/useEditGame";
import { useGetGame } from "../../Data/Game/useGetGame";
import "react-datepicker/dist/react-datepicker.css";
import { TeamSelector } from "../Teams/TeamSelector";
import { FormGroup, Label } from "reactstrap";
import { DateTimePicker } from "./DateTimePicker";

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
    <Container className="data-table">
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label>Home Team: </Label>
          <TeamSelector
            id={game.homeTeamId!}
            onTeamChanged={(e) => setGame({ ...game, homeTeamId: e })}
          />
        </FormGroup>
        <FormGroup>
          <Label>Away Team: </Label>
          <TeamSelector
            id={game.awayTeamId!}
            onTeamChanged={(e) => setGame({ ...game, awayTeamId: e })}
          />
        </FormGroup>
        <FormGroup>
          <Label>Week: </Label>
          <input
            type="number"
            value={game.week || 0}
            onChange={(e) =>
              setGame({ ...game, week: Number.parseInt(e.target.value) })
            }
          />
        </FormGroup>
        <FormGroup>
          <Label>Game Time: </Label>
          <DateTimePicker
            date={game.gameTime!}
            handleDateChange={(date: Date) =>
              setGame({
                ...game,
                gameTime: date,
                gameTimeString: date.toISOString(),
                gameIsoString: date.toISOString(),
              })
            }
          ></DateTimePicker>
        </FormGroup>
        <FormGroup>
          <Label>Game Result: </Label>
          <select
            id="result"
            value={game.gameResult}
            onChange={(e) =>
              setGame({
                ...game,
                gameResult:
                  GameResult[e.target.value as keyof typeof GameResult],
              })
            }
          >
            {Object.keys(GameResult).map((key) => (
              <option value={key}>
                {GameResult[key as keyof typeof GameResult]}
              </option>
            ))}
          </select>
        </FormGroup>
        <input type="submit" value="Save" />
      </Form>
    </Container>
  );
}

async function SaveGame(game: Game) {
  await useEditGame(game);
}
