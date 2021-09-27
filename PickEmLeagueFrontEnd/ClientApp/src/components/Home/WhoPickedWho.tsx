import React, { useContext, useEffect, useState } from "react";
import { Col, Container, Row, Button, Collapse } from "reactstrap";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { Game, GamePick, GameResult, User } from "../../Apis";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { useGetGamesByWeek } from "../../Data/Game/useGetGamesByWeek";
import { useGetGamePicksByWeek } from "../../Data/GamePicks/useGetGamePicksByWeek";
import { TeamDisplay } from "../Teams/TeamDisplay";
import { WeekSelector } from "../Week/WeekSelector";

type CollapsibleTeamAndPicksProps = {
  users: (User | undefined)[];
  game: Game;
  result: GameResult;
};

export function WhoPickedWho() {
  const { week } = useContext(WeekContext);
  const [gamePicks, setGamePicks] = useState<GamePick[]>([]);
  const [games, setGames] = useState<Game[]>([]);

  useEffect(() => {
    async function GetGamePicks() {
      setGamePicks(await useGetGamePicksByWeek(week!));
      setGames(await useGetGamesByWeek(week!));
    }
    GetGamePicks();
  }, [week]);

  function getFinishedGames() {
    return games.filter((g) => g.gameResult !== GameResult.NotPlayed);
  }

  function getInProgressGames() {
    return games.filter(
      (g) =>
        g.gameResult === GameResult.NotPlayed &&
        g.hasStarted
    );
  }

  function getWhoPickedGameResult(game: Game, result: GameResult) {
    return gamePicks
      .filter((gp) => gp.game?.id == game.id && gp.pick === result)
      .map((gp) => gp.user);
  }

  return (
    <div>
      <br />
      <WeekSelector />
      <Container>
        <Row>
          <Col className="col-6 text-center">
            <h2>Finished Games</h2>
            {getFinishedGames().map((g) => (
              <Row key={g.id}>
                <Col className="col-5">
                  <CollapsibleTeamAndPicks
                    users={getWhoPickedGameResult(g, GameResult.AwayWin)}
                    game={g}
                    result={GameResult.AwayWin}
                  />
                </Col>
                <Col className="col-2">
                  <h4>@</h4>
                </Col>
                <Col className="col-5">
                  <CollapsibleTeamAndPicks
                    users={getWhoPickedGameResult(g, GameResult.HomeWin)}
                    game={g}
                    result={GameResult.HomeWin}
                  />
                </Col>
              </Row>
            ))}
          </Col>
          <Col className="col-6 text-center">
            <h2>In Progress Games</h2>
            {getInProgressGames().map((g) => (
              <Row key={g.id}>
                <Col className="col-5">
                  <CollapsibleTeamAndPicks
                    users={getWhoPickedGameResult(g, GameResult.AwayWin)}
                    game={g}
                    result={GameResult.AwayWin}
                  />
                </Col>
                <Col className="col-2">
                  <h4>@</h4>
                </Col>
                <Col className="col-5">
                  <CollapsibleTeamAndPicks
                    users={getWhoPickedGameResult(g, GameResult.HomeWin)}
                    game={g}
                    result={GameResult.HomeWin}
                  />
                </Col>
              </Row>
            ))}
          </Col>
        </Row>
      </Container>
    </div>
  );
}

function CollapsibleTeamAndPicks(props: CollapsibleTeamAndPicksProps) {
  const [open, setOpen] = useState(false);

  function getClassName(result: GameResult) {
    var align = result === GameResult.AwayWin ? "text-left" : "text-right";
    return ["col-8", align].join(" ");
  }

  function getStyle(result: GameResult) {
    return result === props.game.gameResult ? "limegreen" : "";
  }

  return props.result === GameResult.AwayWin ? (
    <Container>
      <Row>
        <Col
          className={getClassName(GameResult.AwayWin)}
          style={{ backgroundColor: getStyle(GameResult.AwayWin) }}
        >
          <TeamDisplay id={props.game.awayTeamId!} />
        </Col>
        <Col className="col-4">
          <Button onClick={() => setOpen(!open)}>{props.users.length}</Button>
        </Col>
      </Row>
      <Row>
        <Collapse isOpen={open}>
          <div style={{ backgroundColor: "lightgray" }}>
            {props.users.map((user) => (
              <p key={props.game.id?.toString()! + "-" + user?.id?.toString()!}>
                {user?.name}
              </p>
            ))}
          </div>
        </Collapse>
      </Row>
    </Container>
  ) : (
    <Container>
      <Row>
        <Col className="col-4">
          <Button onClick={() => setOpen(!open)}>{props.users.length}</Button>
        </Col>
        <Col
          className={getClassName(GameResult.HomeWin)}
          style={{ backgroundColor: getStyle(GameResult.HomeWin) }}
        >
          <TeamDisplay id={props.game.homeTeamId!} />
        </Col>
      </Row>
      <Row>
        <Collapse isOpen={open}>
          <div style={{ backgroundColor: "lightgray" }}>
            {props.users.map((user) => (
              <p key={props.game.id?.toString()! + "-" + user?.id?.toString()!}>
                {user?.name}
              </p>
            ))}
          </div>
        </Collapse>
      </Row>
    </Container>
  );
}
