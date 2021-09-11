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
import { format, formatDistance, formatRelative, subDays } from "date-fns";
import { UserContext, useUserContext } from "../../Data/Contexts/UserContext";
import { Row } from "reactstrap";

export function GameList() {
  const { user } = useContext(UserContext);
  const { week, setWeek } = useContext(WeekContext);
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

  const formatDate = (isoString: string | null | undefined) => {
    if (!isoString) return "";
    let dt = new Date(isoString);
    return format(dt, "eeee dd, MMM 'at' h:mm aa");
  };

  return (
    <Container className="data-table">
      <WeekSelector />

      <Table striped bordered>
        <thead>
          <tr>
            <th>Home Team</th>
            <th>Away Team</th>
            <th>Game Time (ET)</th>
            <th>Result</th>
          </tr>
        </thead>
        <tbody>
          {games.map((game) => (
            <tr key={game.id}>
              <td>
                <TeamDisplay id={game.homeTeamId} />
              </td>
              <td>
                <TeamDisplay id={game.awayTeamId} />
              </td>
              <td>{formatDate(game.gameIsoString)}</td>
              <td>{game.gameResult}</td>
              <td hidden={!user?.isAdmin}>
                <Link to={"games/" + game.id} className="btn btn-primary">
                  Edit
                </Link>
              </td>
              <td hidden={!user?.isAdmin}>
                <Button onClick={(e) => handleDeleteGame(game.id!)}>
                  Delete
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Row hidden={!user?.isAdmin}>
        <Button onClick={AddGame}>Add Game</Button>
        <Button onClick={GetGames}>Get Games</Button>
      </Row>
    </Container>
  );
}
