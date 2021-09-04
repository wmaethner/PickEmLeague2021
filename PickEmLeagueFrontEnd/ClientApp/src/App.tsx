import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";

import AdapterDateFns from "@date-io/date-fns";
import LocalizationProvider from "@mui/lab/LocalizationProvider";
import enLocale from "date-fns/locale/en-US";

import "./custom.css";
import { Switch } from "react-router";
import { Users } from "./components/Users/Users";
import { Games } from "./components/Games/Games";
import { UserProvider } from "./Data/User/UserContext";
import { TeamProvider } from "./Data/Teams/TeamsContext";
import { LoginForm } from "./components/Authentication/LoginForm";



export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <UserProvider>
        <TeamProvider>
          <Layout>
            <LocalizationProvider dateAdapter={AdapterDateFns} locale={enLocale}>
              <LoginForm></LoginForm>
              <Switch>
                <Route exact path="/" component={Home} />
                <Route path="/games">
                  <Games />
                </Route>
                <Route path="/users">
                  <Users />
                </Route>
              </Switch>
            </LocalizationProvider>
          </Layout>
        </TeamProvider>
      </UserProvider>
    );
  }
}
