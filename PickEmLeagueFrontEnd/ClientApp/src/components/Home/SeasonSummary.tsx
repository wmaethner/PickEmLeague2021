import React, { useEffect, useState } from "react";
import { Table } from "reactstrap";
import { Game, GameResult, SeasonSummary, User } from "../../Apis";
import { useGetAllGames } from "../../Data/Game/userGetGameAll";
import { useGetSeasonSummaries } from "../../Data/ScoreSummary/useGetSeasonSummaries";
import { userDisplay } from "../Home";
import { WinnersCircle } from "./WinnersCircle";

export function SeasonSummaryPage() {
  const [games, setGames] = useState<Game[]>([]);
  const [summaries, setSummaries] = useState<SeasonSummary[]>([]);

  useEffect(() => {
    async function GetGames() {
      setGames(await useGetAllGames());
      setSummaries(await useGetSeasonSummaries());
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
        winner={summaries.sort((a, b) => (a.score! < b.score! ? 1 : -1))[0]?.user}
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
          {summaries?.sort((a, b) => (a.place! > b.place! ? 1 : -1))
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
