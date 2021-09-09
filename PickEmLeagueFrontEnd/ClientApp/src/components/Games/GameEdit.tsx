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

  const handleDateTimeChange = (dateTime: Date) => {
    setGame({
      ...game,
      gameTime: dateTime,
      gameTimeString: dateTime.toISOString(),
    });
  };

  const handleDateChange = (date: Date | [Date | null, Date | null] | null) => {
    let dateToUse: Date = new Date();
    if (!date) {
      return new Date();
    }
    if (Array.isArray(date)) {
      const [startDate, endDate] = date;
      dateToUse = startDate!;
    } else {
      dateToUse = date!;
    }

    let newDate = new Date(game.gameTime!);
    newDate.setMonth(dateToUse.getMonth());
    newDate.setDate(dateToUse.getDate());
    newDate.setFullYear(dateToUse.getFullYear());
    newDate.setHours(dateToUse.getHours());
    newDate.setMinutes(dateToUse.getMinutes());

    setGame({ ...game, gameTime: newDate });
  };

  // const convertUTCToLocalTime = (date: Date | [Date | null, Date | null] | null) => {
  //   let milliseconds: number = 0;
  //   let dateToUse: Date = new Date();
  //   if (!date) { return new Date(); }
  //   if (Array.isArray(date)) {
  //     const [startDate, endDate] = date;
  //     dateToUse = startDate!;
  //   } else {
  //     dateToUse = date!;
  //   }

  //   milliseconds = Date.UTC(
  //     dateToUse.getFullYear(),
  //     dateToUse.getMonth(),
  //     dateToUse.getDate(),
  //     dateToUse.getHours(),
  //     dateToUse.getMinutes(),
  //     dateToUse.getSeconds()
  //   );

  //   return new Date(milliseconds);
  //   // localTime.getDate() // local date
  //   // localTime.getHours() // local hour
  // };

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
          {/* <DateTimePicker
            renderInput={(props) => <TextField {...props} />}
            label="DateTimePicker"
            value={game.gameTime}
            onChange={(newValue) => {
              setGame({ ...game, gameTime: newValue ?? undefined });
            }}
          /> */}
          {/* <DateTimeSetter
            dateTime={convertUTCToLocalTime(game.gameTime!)}
            setDateTime={handleDateTimeChange}
          ></DateTimeSetter> */}
          {/* <DatePicker selected={game.gameTime} 
                      selectsRange={false} 
                      showMonthDropdown
                      showYearDropdown
                      showTimeSelect
                      timeFormat="HH:mm"
                      timeIntervals={5}
                      timeCaption="time"
                      dateFormat="MMMM d, yyyy h:mm aa"
                      onChange={(date) => handleDateChange(date)}></DatePicker> */}
          <DateTimePicker
            date={game.gameTime!}
            handleDateChange={(date: Date) =>
              setGame({
                ...game,
                gameTime: date,
                gameTimeString: date.toISOString(),
              })
            }
          ></DateTimePicker>
        </FormGroup>
        <FormGroup>
          <Label>Game Result: </Label>
          <select
            id="result"
            name="country"
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

          {/* <select id="country" name="country" value={country} onChange={onChange}>
            <option value="AX">Aaland Islands</option>
            <option value="AF">Afghanistan</option>
          </select>
          <div onChange={(e) => handleGameResultChange(e)}>
            <input type="radio" value="Male" name="gender" /> Male
            <input type="radio" value="Female" name="gender" /> Female
            <input type="radio" value="Other" name="gender" /> Other
          </div> */}
        </FormGroup>

        <input type="submit" value="Save" />
      </Form>
    </Container>
  );
}

async function SaveGame(game: Game) {
  await useEditGame(game);
}
