import React, { useContext, useEffect, useState } from "react";
import { Col, Container, Row } from "reactstrap";
import { Tab, Nav } from "react-bootstrap";
import { UserWeekSummary, WeekSummary } from "./Home/WeekSumary";
import { SeasonSummary, UserSeasonSummary } from "./Home/SeasonSummary";
import { ProfilePicture } from "./Images/ProfilePicture";
import { Game, User, UserSummary } from "../Apis";
import ReactTooltip from "react-tooltip";
import { useGetGamesByWeek } from "../Data/Game/useGetGamesByWeek";
import { useGetScoreSummaryByWeek } from "../Data/ScoreSummary/useGetScoreSummaryByWeek";
import { WeekContext } from "../Data/Contexts/WeekContext";

export function userDisplay(user: User, index: number) {
  return (
    <div className="row align-items-center">
      <div className="col">{usersNameDisplay(user, index)}</div>
      <div className="col">
        <ProfilePicture userId={user.id} />
      </div>
    </div>
  );
}

export function usersNameDisplay(user: User, index: number) {
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
        <ReactTooltip id={user.id?.toString()} type="info">
          <span>{user.name}</span>
        </ReactTooltip>
      </div>
    );
  }
  return (
    <div>
      <label>{user.name}</label>
    </div>
  );
}

export function Home() {
  const { week, setWeek } = useContext(WeekContext);
  const [scoreSummary, setScoreSummary] = useState<Array<UserSummary>>([]);
  const [games, setGames] = useState<Game[]>([]);

  useEffect(() => {
    async function GetData() {
      setScoreSummary(await useGetScoreSummaryByWeek(week));
      setGames(await useGetGamesByWeek(week!));
    }
    GetData();
  }, [week]);

  function buildWeekSummaries(): UserWeekSummary[] {
    let weekSummaries: Array<UserWeekSummary> = [];

    scoreSummary.forEach(item => {
      weekSummaries.push({
        user: item.user!,
        displayName: item.user?.username ? item.user?.username! : item.user?.name!,
        pickStatus: item.weekSummary?.weekPickStatus!,
        score: item.weekSummary?.weekScore!,
        correctPicks: item.weekSummary?.correctPicks!
      })
    });

    return weekSummaries;
  }

  function buildSeasonSummaries(): UserSeasonSummary[] {
    let seasonSummaries: Array<UserSeasonSummary> = [];

    scoreSummary.forEach(item => {
      seasonSummaries.push({
        user: item.user!,
        displayName: item.user?.username ? item.user?.username! : item.user?.name!,
        score: item.seasonSummary?.seasonScore!,
        correctPicks: item.seasonSummary?.correctPicks!
      })
    });

    return seasonSummaries;
  }

  return (
    <Container className="data-table">
      <Tab.Container id="left-tabs-example" defaultActiveKey="week">
        <Row>
          <Col sm={2}>
            <Nav variant="pills" className="flex-column">
              <Nav.Item>
                <Nav.Link eventKey="week">Week Summary</Nav.Link>
              </Nav.Item>
              <Nav.Item>
                <Nav.Link eventKey="season">Season Summary</Nav.Link>
              </Nav.Item>
            </Nav>
          </Col>
          <Col sm={10}>
            <Tab.Content>
              <Tab.Pane eventKey="week">
                <WeekSummary weekSummaries={buildWeekSummaries()} games={games} />
              </Tab.Pane>
              <Tab.Pane eventKey="season">
                <SeasonSummary seasonSummaries={buildSeasonSummaries()} />
              </Tab.Pane>
            </Tab.Content>
          </Col>
        </Row>
      </Tab.Container>
    </Container>
  );
}
