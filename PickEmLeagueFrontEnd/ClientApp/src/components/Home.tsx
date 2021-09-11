import React, { useContext, useEffect, useState } from "react";
import { useUserContext } from "../Data/Contexts/UserContext";
import { WeekContext } from "../Data/Contexts/WeekContext";
import { Container, Table, Tooltip } from "reactstrap";
import { useGetScoreSummaryByWeek } from "../Data/ScoreSummary/useGetScoreSummaryByWeek";
import { UsersWeeklyScoreSummary } from "../Apis/models/UsersWeeklyScoreSummary";
import { WeekSelector } from "./Week/WeekSelector";
import { User } from "../Apis";

export function Home() {
  const { user, loggedIn } = useUserContext();
  const { week, setWeek } = useContext(WeekContext);
  const [scoreSummary, setScoreSummary] = useState<
    Array<UsersWeeklyScoreSummary>
  >([]);
  const [tooltipOpen, setTooltipOpen] = useState(false);

  const toggle = () => setTooltipOpen(!tooltipOpen);

  async function GetScoreSummary() {
    setScoreSummary(await useGetScoreSummaryByWeek(week!));
  };

  function userDisplay(user: User, index: number) {
    if (user.username) {
      return (
        <div>
          <label id={"user-label-" + index}>{user.username}</label>
          {/* <Tooltip placement="right" isOpen={tooltipOpen} target={"user-label-" + index} toggle={toggle}>
            {user.name!}
          </Tooltip> */}
        </div>);
    }
    return <label>{user.name}</label>;
  }

  useEffect(() => {
    (async () => {
      await GetScoreSummary();
    })();
  }, [week]);

  return (
    <Container className="data-table">
      <WeekSelector />
      <Table>
        <thead>
          <tr>
            <th>User</th>
            <th>Week Score</th>
            <th>Season Score</th>
          </tr>
        </thead>
        <tbody>
          {scoreSummary?.map((userScore, index) => (
            <tr key={userScore.user?.id}>
              <td>{userDisplay(userScore.user!, index)}</td>
              <td>{userScore.weekScore}</td>
              <td>{userScore.seasonScore}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Container>
  );
}
