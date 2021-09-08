import React from "react";
import { TeamProvider } from "../Data/Contexts/TeamsContext";
import { useUserContext } from "../Data/Contexts/UserContext";
import { LoginForm } from "./Authentication/LoginForm";
import AdapterDateFns from "@date-io/date-fns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import enLocale from "date-fns/locale/en-US";
import { Route, Switch } from "react-router-dom";
import { Games } from "./Games/Games";
import { WeekProvider } from "../Data/Contexts/WeekContext";
import { GamePicks } from "./GamePicks/GamePicks";
import { Users } from "./Users/Users";
import { Layout } from "./Layout";
import { Container } from "reactstrap";

export function Home() {
  const { user, loggedIn } = useUserContext();

  return loggedIn ? (
    <Container>
      <h2>Id: {user?.id}</h2>
      <h2>Name: {user?.name}</h2>
      <TeamProvider>
        <Layout>
          <LocalizationProvider dateAdapter={AdapterDateFns} locale={enLocale}>
            <Switch>
              {/* <Route exact path="/" component={Home} /> */}
              <Route path="/games">
                <Games />
              </Route>
              <Route path="/gamePicks">
                <WeekProvider>
                  <GamePicks />
                </WeekProvider>
              </Route>
              <Route path="/users">
                <Users />
              </Route>
            </Switch>
          </LocalizationProvider>
        </Layout>
      </TeamProvider>
    </Container>
  ) : (
    <LoginForm></LoginForm>
  );
}
