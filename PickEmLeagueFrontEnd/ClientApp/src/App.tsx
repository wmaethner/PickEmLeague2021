import React from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";

import AdapterDateFns from "@date-io/date-fns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import enLocale from "date-fns/locale/en-US";
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import { Switch } from "react-router";
import { Users } from "./components/Users/Users";
import { Games } from "./components/Games/Games";
import { TeamProvider } from "./Data/Contexts/TeamsContext";
import { LoginForm } from "./components/Authentication/LoginForm";
import { GamePicks } from "./components/GamePicks/GamePicks";
import { WeekProvider } from "./Data/Contexts/WeekContext";
import { UserContext } from "./Data/Contexts/UserContext";
import { useState } from "react";
import { User } from "./Apis";
import { Container } from "reactstrap";

import "./Styles/App.css";

//export default class App extends Component {
export default function App() {
  //static displayName = App.name;
  const [user, setUserState] = useState<User>({});
  const [loggedIn, setLoggedIn] = useState<boolean>(false);

  const setUser = (user: User) => {
    setUserState(user);
    setLoggedIn(true);
  };

  const pages = () => {
    return loggedIn ? (
      <div className="container-fluid app-container">
        <TeamProvider>
          <Layout>
            <LocalizationProvider
              dateAdapter={AdapterDateFns}
              locale={enLocale}
            >
              <Switch>
                <Route exact path="/">
                  <Home></Home>
                </Route>
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
      </div>
    ) : (
      <LoginForm></LoginForm>
    );
  };

  //render() {
  return (
    <UserContext.Provider value={{ user, loggedIn, setUser }}>
      {pages()}
      {/* <Home></Home> */}
    </UserContext.Provider>
    //<Home></Home>
    //<h2>TEST</h2>
  );
  //}
}
