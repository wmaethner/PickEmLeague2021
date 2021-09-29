import React, { useEffect, useState } from "react";
import { Table } from "reactstrap";
import { Game, GameResult, User } from "../../Apis";
import { useGetAllGames } from "../../Data/Game/userGetGameAll";
import { userDisplay } from "../Home";
import { WinnersCircle } from "./WinnersCircle";

export type UserSeasonSummary = {
  user: User;
  displayName: string;
  score: number;
  correctPicks: number;
  place: number;
};

export type SeasonSummaryProps = {
  seasonSummaries: UserSeasonSummary[];
};

export function SeasonSummary(props: SeasonSummaryProps) {
  const [games, setGames] = useState<Game[]>([]);

  useEffect(() => {
    async function GetGames() {
      setGames(await useGetAllGames());
    }
    GetGames();
  }, []);

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
        winner={
          props.seasonSummaries?.sort((a, b) => (a.score < b.score ? 1 : -1))[0]
            ?.user
        }
        message={"Season Leader"}
      />
      <br />
      <Table>
        <thead>
          <tr>
            <th>Place</th>
            <th>User</th>
            <th>Correct Picks</th>
            <th>Season Score</th>
          </tr>
        </thead>
        <tbody>
          {props.seasonSummaries
            ?.sort((a, b) => (a.place > b.place ? 1 : -1))
            .map((summary, index) => (
              <tr key={summary.user?.id}>
                <td>{summary.place}</td>
                <td>{userDisplay(summary.user!, index)}</td>
                <td>{displayCorrectPicks(summary.correctPicks)}</td>
                <td>{summary.score}</td>
              </tr>
            ))}
        </tbody>
      </Table>
    </div>
  );
}
