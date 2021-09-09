import React, { useContext, useEffect, useState } from "react";
import { useUserContext } from "../Data/Contexts/UserContext";
import { WeekContext } from "../Data/Contexts/WeekContext";
import { Container, Table } from "reactstrap";
import { useGetScoreSummaryByWeek } from "../Data/ScoreSummary/useGetScoreSummaryByWeek";
import { UsersWeeklyScoreSummary } from "../Apis/models/UsersWeeklyScoreSummary";

export function Home() {
  const { user, loggedIn } = useUserContext();
  const weekContext = useContext(WeekContext);
  const [scoreSummary, setScoreSummary] = useState<Array<UsersWeeklyScoreSummary>>([]);

  useEffect(() => {
    (async () => {
      await GetScoreSummary();
    })();
  }, []);

  const GetScoreSummary = async (): Promise<void> => {
    setScoreSummary(await useGetScoreSummaryByWeek(weekContext.week!));
  };

  return (
    <Container className="data-table">
      <Table>
        <thead>
          <tr>
            <th>User</th>
            <th>Week Score</th>
            <th>Season Score</th>
          </tr>
        </thead>
        <tbody>
          {scoreSummary?.map((userScore) => (
            <tr key={userScore.user?.id}>
              <td>{userScore.user?.name}</td>
              <td>{userScore.weekScore}</td>
              <td>{userScore.seasonScore}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </Container>
  )
}
