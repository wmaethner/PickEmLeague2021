import React, { useContext, useEffect, useState } from "react";
import { useUserContext } from "../Data/Contexts/UserContext";
import { WeekContext } from "../Data/Contexts/WeekContext";
import { Container, Table, Button } from "reactstrap";
import { useGetScoreSummaryByWeek } from "../Data/ScoreSummary/useGetScoreSummaryByWeek";
import { WeekSelector } from "./Week/WeekSelector";
import { Game, GameResult, User, UserSummary, WeekPickStatus } from "../Apis";
import { BsCircleFill } from 'react-icons/bs';
import ReactTooltip from 'react-tooltip';
import { useGetGamesByWeek } from "../Data/Game/useGetGamesByWeek";

export function Home() {
  const { user, loggedIn } = useUserContext();
  const { week } = useContext(WeekContext);
  const [scoreSummary, setScoreSummary] = useState<
    Array<UserSummary>
  >([]);
  const [games, setGames] = useState<Game[]>([]);

  async function GetGames() {
    const games = await useGetGamesByWeek(week!);
    setGames(games);
  }

  useEffect(() => {
    (async () => {
      await GetGames();
    })();
  }, [GetGames, week]);

  useEffect(() => {
    (async () => {
      await GetScoreSummary();
    })();
  }, [week]);

  async function GetScoreSummary() {
    setScoreSummary(await useGetScoreSummaryByWeek(week!));
  }

  function userDisplay(user: User, index: number) {
    if (user.username) {
      return (
        <div>
          <label id={"user-label-" + index} data-tip data-for={user.id?.toString()}>{user.username}</label>
          <ReactTooltip id={user.id?.toString()} type='info'>
            <span>{user.name}</span>
          </ReactTooltip>
        </div>
      );
    }
    return <label>{user.name}</label>;
  }

  function displayPickStatus(status: WeekPickStatus | undefined) {
    switch (status) {
      case WeekPickStatus.NotPicked: return <div><BsCircleFill color="red" size="1.5em" className="status-circle" /></div>
      case WeekPickStatus.PartiallyPicked: return <div><BsCircleFill color="yellow" size="1.5em" className="status-circle" /></div>
      case WeekPickStatus.FullyPicked: return <div><BsCircleFill color="green" size="1.5em" className="status-circle" /></div>
    }
    return "";
  }

  function displayCorrectPicks(correctPicks: number | undefined) {
    let count = correctPicks ? correctPicks : 0;

    return (
      <div>
        <label>{count}/{getGamesPlayed()}</label>
      </div>
    )
  }

  function getGamesPlayed() {
    return games.filter(game => game.gameResult !== GameResult.NotPlayed).length;
  }


  return (
    <Container className="data-table">
      <WeekSelector />
      <Table>
        <thead>
          <tr>
            <th>User</th>
            <th></th>
            <th>Week Score</th>
            <th>Correct Picks</th>
            <th>Season Score</th>
          </tr>
        </thead>
        <tbody>
          {scoreSummary?.map((userScore, index) => (
            <tr key={userScore.user?.id}>
              <td>{userDisplay(userScore.user!, index)}</td>
              <td>{displayPickStatus(userScore.weekSummary?.weekPickStatus)}</td>
              <td>{userScore.weekSummary?.weekScore}</td>
              <td>{displayCorrectPicks(userScore.weekSummary?.correctPicks)}</td>
              <td>{userScore.seasonSummary?.seasonScore}</td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button onClick={() => GetScoreSummary()}>Refresh</Button>
    </Container>
  );
}
