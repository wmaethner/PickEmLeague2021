import React, { useContext, useEffect, useState } from "react";
import { Col, Container, Row, Button, Collapse } from "reactstrap";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { Game, GamePick, GameResult, User } from "../../Apis";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { useGetGamesByWeek } from "../../Data/Game/useGetGamesByWeek";
import { useGetGamePicksByWeek } from "../../Data/GamePicks/useGetGamePicksByWeek";
import { TeamDisplay } from "../Teams/TeamDisplay";
import { WeekSelector } from "../Week/WeekSelector";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faChevronUp, faChevronDown } from '@fortawesome/free-solid-svg-icons'

type CollapsibleGamePicksDisplayProps = {
  game: Game;
  gamePicks: GamePick[];
}

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

  function getStartedGames() {
    return games.filter(
      (g) => g.hasStarted
    );
  }

  function getHalfOfStartedGames(first: boolean) {
    const list = getStartedGames();
    const half = Math.ceil(list.length / 2);

    return first ? list.slice(0, half) : list.slice(-half);
  }

  return (
    <div>
      <br />
      <WeekSelector />
      <Container>
        <Row>
          <Col className="col-6 text-center">
            {getHalfOfStartedGames(true).map((g) => (
              <Row key={g.id}>
                <CollapsibleGamePicksDisplay
                  game={g}
                  gamePicks={gamePicks.filter(gp => gp.game?.id == g.id)}
                />
              </Row>
            ))}
          </Col>
          <Col className="col-6 text-center">
            {getHalfOfStartedGames(false).map((g) => (
              <Row key={g.id}>
                <CollapsibleGamePicksDisplay
                  game={g}
                  gamePicks={gamePicks.filter(gp => gp.game?.id == g.id)}
                />
              </Row>
            ))}
          </Col>
        </Row>
      </Container>
    </div>
  );
}

function CollapsibleGamePicksDisplay(props: CollapsibleGamePicksDisplayProps) {
  const [homeOpen, setHomeOpen] = useState(false);
  const [awayOpen, setAwayOpen] = useState(false);

  function getWhoPickedGameResult(result: GameResult) {
    return props.gamePicks
      .filter((gp) => gp.pick === result)
      .map((gp) => gp.user);
  }

  function averageScore(game: Game, result: GameResult) {
    let picks = props.gamePicks.filter((gp) => gp.game?.id == game.id && gp.pick === result);

    if (picks.length == 0) { return 0; }

    const total = picks.map(gp => gp.wager)
      .reduce(function (a, b) {
        return a! + b!;
      });

    return (total! / picks.length).toFixed(1);
  }

  function getClassName(result: GameResult) {
    var align = result === GameResult.AwayWin ? "text-left" : "text-right";
    return ["col-6", align].join(" ");
  }

  function getStyle(game: Game, result: GameResult) {
    return result === game.gameResult ? "limegreen" : "";
  }

  return (
    <Container className="align-items-center border">
      <Row className="align-items-center">
        <Col
          className={getClassName(GameResult.AwayWin)}
          style={{ backgroundColor: getStyle(props.game, GameResult.AwayWin) }}
        >
          <TeamDisplay id={props.game.awayTeamId!} />
        </Col>
        <Col className="col-3">
          <h4>{averageScore(props.game, GameResult.AwayWin)} pts</h4>
        </Col>
        <Col className="col-3">
          <Row>
            <Col className="col-6">
              <h4>
                {getWhoPickedGameResult(GameResult.AwayWin).length}
              </h4>
            </Col>
            <Col className="col-6">
              <Button onClick={() => setAwayOpen(!awayOpen)}>
                <FontAwesomeIcon icon={awayOpen ? faChevronUp : faChevronDown} />
              </Button>
            </Col>
          </Row>
        </Col>
      </Row>
      <Row>
        <Collapse isOpen={awayOpen}>
          <div style={{ backgroundColor: "lightgray" }}>
            {getWhoPickedGameResult(GameResult.AwayWin).map((user) => (
              <p key={props.game.id?.toString()! + "-" + user?.id?.toString()!}>
                {user?.name}
              </p>
            ))}
          </div>
        </Collapse>
      </Row>
      <Row className="align-items-center">
        <Col
          className={getClassName(GameResult.HomeWin)}
          style={{ backgroundColor: getStyle(props.game, GameResult.HomeWin) }}
        >
          <TeamDisplay id={props.game.homeTeamId!} />
        </Col>
        <Col className="col-3">
          <h4>{averageScore(props.game, GameResult.HomeWin)} pts</h4>
        </Col>
        <Col className="col-3">
          <Row>
            <Col className="col-6">
              <h4>
                {getWhoPickedGameResult(GameResult.HomeWin).length}
              </h4>
            </Col>
            <Col className="col-6">
              <Button onClick={() => setHomeOpen(!homeOpen)}>
                <FontAwesomeIcon icon={homeOpen ? faChevronUp : faChevronDown} />
              </Button>
            </Col>
          </Row>
        </Col>
      </Row>
      <Row>
        <Collapse isOpen={homeOpen}>
          <div style={{ backgroundColor: "lightgray" }}>
            {getWhoPickedGameResult(GameResult.HomeWin).map((user) => (
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
