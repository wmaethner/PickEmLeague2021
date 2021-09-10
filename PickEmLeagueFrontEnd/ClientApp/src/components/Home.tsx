import React, { useContext, useEffect, useState } from "react";
import { useUserContext } from "../Data/Contexts/UserContext";
import { WeekContext } from "../Data/Contexts/WeekContext";
import { Container, Table } from "reactstrap";
import { useGetScoreSummaryByWeek } from "../Data/ScoreSummary/useGetScoreSummaryByWeek";
import { UsersWeeklyScoreSummary } from "../Apis/models/UsersWeeklyScoreSummary";
import { WeekSelector } from "./Week/WeekSelector";

export function Home() {
  const { user, loggedIn } = useUserContext();
  const { week, setWeek } = useContext(WeekContext);
  const [scoreSummary, setScoreSummary] = useState<
    Array<UsersWeeklyScoreSummary>
  >([]);

  useEffect(() => {
    (async () => {
      await GetScoreSummary();
    })();
  }, [week]);

  const GetScoreSummary = async (): Promise<void> => {
    setScoreSummary(await useGetScoreSummaryByWeek(week!));
  };

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
  );
}
