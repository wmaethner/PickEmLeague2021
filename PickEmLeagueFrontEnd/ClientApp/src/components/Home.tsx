import React, { useCallback, useContext, useEffect, useState } from "react";
import { useUserContext } from "../Data/Contexts/UserContext";
import { WeekContext } from "../Data/Contexts/WeekContext";
import { Container, Table, Button } from "reactstrap";
import { useGetScoreSummaryByWeek } from "../Data/ScoreSummary/useGetScoreSummaryByWeek";
import { WeekSelector } from "./Week/WeekSelector";
import { Game, GameResult, User, UserSummary, WeekPickStatus } from "../Apis";
import { BsCircleFill } from "react-icons/bs";
import ReactTooltip from "react-tooltip";
import { useGetGamesByWeek } from "../Data/Game/useGetGamesByWeek";
import { useGetUserImage } from "../Data/Images/useGetUserImage";
import { Image } from "react-bootstrap";

function useGetGames(week: number | undefined) {
  const [games, setGames] = useState<Game[]>([]);

  useEffect(() => {
    if (week === undefined) {
      return;
    }
    async function GetGames() {
      setGames(await useGetGamesByWeek(week!));
    }
    GetGames();
  }, [week]);

  return games;
}

function useGetScoreSummaries(week: number | undefined) {
  const [scoreSummary, setScoreSummary] = useState<Array<UserSummary>>([]);

  useEffect(() => {
    async function GetSummaries() {
      setScoreSummary(await useGetScoreSummaryByWeek(week));
    }
    GetSummaries();
  }, [week]);

  return scoreSummary;
}

export function Home() {
  const { user, loggedIn } = useUserContext();
  // const [image, setImage] = useState("");
  const { week } = useContext(WeekContext);
  // const [scoreSummary, setScoreSummary] = useState<Array<UserSummary>>([]);
  //const [games, setGames] = useState<Game[]>([]);
  const games = useGetGames(week);
  const scoreSummary = useGetScoreSummaries(week);

  // const GetGames = useCallback(async () => {
  //   const games = await useGetGamesByWeek(week!);
  //   setGames(games);
  // }, [week])
  // async function GetGames() {
  //   const games = await useGetGamesByWeek(week!);
  //   setGames(games);
  // }

  // const GetScoreSummary = useCallback(async () => {
  //   setScoreSummary(await useGetScoreSummaryByWeek(week!));
  // }, [week])

  // async function GetScoreSummary() {

  // }

  // async function GetImage(userId: number) {
  //   return await useGetUserImage(userId);
  //   // setImage("data:image/jpeg;base64," + response.valueOf());
  // }

  // useEffect(() => {
  //   (async () => {
  //     await GetGames();
  //   })();
  // }, [GetGames]);

  // useEffect(() => {
  //   (async () => {
  //     await GetScoreSummary();
  //   })();
  // }, [GetScoreSummary]);

  async function userDisplay(user: User, index: number) {
    if (user.username) {
      return (
        <div>
          <label
            id={"user-label-" + index}
            data-tip
            data-for={user.id?.toString()}
          >
            {user.username}
          </label>
          {/* <Image src={await GetImage(user.id!)} /> */}
          <ReactTooltip id={user.id?.toString()} type="info">
            <span>{user.name}</span>
          </ReactTooltip>
        </div>
      );
    }
    return <label>{user.name}</label>;
  }

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
    <Container className="data-table">
      <WeekSelector />
      {/* <Image src={image} /> */}
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
              <td>
                {displayPickStatus(userScore.weekSummary?.weekPickStatus)}
              </td>
              <td>{userScore.weekSummary?.weekScore}</td>
              <td>
                {displayCorrectPicks(userScore.weekSummary?.correctPicks)}
              </td>
              <td>{userScore.seasonSummary?.seasonScore}</td>
            </tr>
          ))}
        </tbody>
      </Table>
      {/* <Button onClick={() => GetImage()}>Image</Button> */}
      {/* <Button onClick={() => GetScoreSummary()}>Refresh</Button> */}
    </Container>
  );
}
