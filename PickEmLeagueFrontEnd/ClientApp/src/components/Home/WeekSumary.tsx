import React, { useContext, useEffect, useState } from "react";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { Table } from "reactstrap";
import { Game, GameResult, User, WeekPickStatus } from "../../Apis";
import { BsCircleFill } from "react-icons/bs";
import ReactTooltip from "react-tooltip";
import { useGetGamesByWeek } from "../../Data/Game/useGetGamesByWeek";
import { userDisplay } from "../Home";
import { WeekSelector } from "../Week/WeekSelector";
import { WinnersCircle } from "./WinnersCircle";
import { useGetWeekWinner } from "../../Data/ScoreSummary/useGetWeekWinner";

export type UserWeekSummary = {
  user: User;
  displayName: string;
  pickStatus: WeekPickStatus;
  score: number;
  correctPicks: number;
};

export type WeekSummaryProps = {
  weekSummaries: UserWeekSummary[];
};

export function WeekSummary(props: WeekSummaryProps) {
  const { week } = useContext(WeekContext);
  const [prevWeekWinner, setPrevWeekWinner] = useState<User | undefined>(
    undefined
  );
  const [games, setGames] = useState<Game[]>([]);

  useEffect(() => {
    async function GetInfo() {
      setGames(await useGetGamesByWeek(week!));
      setPrevWeekWinner(await useGetWeekWinner(week! - 1));
    }
    GetInfo();
  }, [week]);

  function displayPickStatus(status: WeekPickStatus | undefined) {
    switch (status) {
      case WeekPickStatus.NotPicked:
        return (
          <div>
            <BsCircleFill color="red" size="1.5em" className="status-circle" />
          </div>
        );
      case WeekPickStatus.PartiallyPicked:
        return (
          <div>
            <BsCircleFill
              color="yellow"
              size="1.5em"
              className="status-circle"
            />
          </div>
        );
      case WeekPickStatus.FullyPicked:
        return (
          <div>
            <BsCircleFill
              color="green"
              size="1.5em"
              className="status-circle"
            />
          </div>
        );
    }
    return "";
  }

  function displayCorrectPicks(correctPicks: number | undefined) {
    let count = correctPicks ? correctPicks : 0;

    return (
      <div>
        <label>
          {count}/{getGamesPlayed()}
        </label>
      </div>
    );
  }

  function getGamesPlayed() {
    return games.filter((game) => game.gameResult !== GameResult.NotPlayed)
      .length;
  }

  return (
    <div>
      <WinnersCircle
        winner={prevWeekWinner}
        message={"Week " + (week! - 1) + " Winner"}
      />
      <br />
      <WeekSelector />
      <Table>
        <thead>
          <tr>
            <th>User</th>
            <th>
              <label id="pick-status" data-tip data-for="pick-status">
                Pick Status
              </label>
            </th>
            <th>Week Score</th>
            <th>Correct Picks</th>
          </tr>
        </thead>
        <tbody>
          {props.weekSummaries
            ?.sort((a, b) => (a.score < b.score ? 1 : -1))
            .map((userSummary, index) => (
              <tr key={userSummary.user?.id}>
                <td>{userDisplay(userSummary.user!, index)}</td>
                <td>{displayPickStatus(userSummary.pickStatus)}</td>
                <td>{userSummary.score}</td>
                <td>{displayCorrectPicks(userSummary.correctPicks)}</td>
              </tr>
            ))}
        </tbody>
      </Table>
      <ReactTooltip id="pick-status" type="info">
        <p>Green = Fully picked</p>
        <p>Yellow = Some picked</p>
        <p>Red = None picked</p>
      </ReactTooltip>
    </div>
  );
}
