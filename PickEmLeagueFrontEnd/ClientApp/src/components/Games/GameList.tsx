import { Button, Container } from "react-bootstrap";
import React, { useContext, useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { Link } from "react-router-dom";
import { Game } from "../../Apis/models/Game";
import { useCreateGame } from "../../Data/Game/useCreateGame";
import { useDeleteGame } from "../../Data/Game/useDeleteGame";
import { TeamDisplay } from "../Teams/TeamDisplay";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { useGetGamesByWeek } from "../../Data/Game/useGetGamesByWeek";
import { WeekSelector } from "../Week/WeekSelector";

export function GameList() {
  const { week, setWeek } = useContext(WeekContext);
  const [games, setGames] = useState<Game[]>([]);

  useEffect(() => {
    (async () => {
      await GetGames();
    })();
  }, [week]);

  const GetGames = async (): Promise<void> => {
    const games = await useGetGamesByWeek(week!);
    setGames(games);
  };

  const handleDeleteGame = async (gameId: number) => {
    await DeleteGame(gameId);
    await GetGames();
  };

  const AddGame = async () => {
    await useCreateGame(week!);
    await GetGames();
  };

  const DeleteGame = async (id: number) => {
    await useDeleteGame(id);
  };

  return (
    <Container className="data-table">
      <WeekSelector />

      <Table striped bordered>
        <thead>
          <tr>
            <th>Id</th>
            <th>Home Team</th>
            <th>Away Team</th>
            <th>Week</th>
            <th>Game Time</th>
            <th>Result</th>
          </tr>
        </thead>
        <tbody>
          {games.map((game) => (
            <tr key={game.id}>
              <td>{game.id}</td>
              <td>
                <TeamDisplay id={game.homeTeamId} />
              </td>
              <td>
                <TeamDisplay id={game.awayTeamId} />
              </td>
              <td>{game.week}</td>
              <td>{game.gameTimeString}</td>
              <td>{game.gameResult}</td>
              <td>
                <Link to={"games/" + game.id} className="btn btn-primary">
                  Edit
                </Link>
              </td>
              <td>
                <Button onClick={(e) => handleDeleteGame(game.id!)}>
                  Delete
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button onClick={AddGame}>Add Game</Button>
      <Button onClick={GetGames}>Get Games</Button>
    </Container>
  );
}
