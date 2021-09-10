import React, { useState } from "react";
import { Route, Switch, useRouteMatch } from "react-router-dom";
import { WeekContext, WeekProvider } from "../../Data/Contexts/WeekContext";
import { GameEdit } from "./GameEdit";
import { GameList } from "./GameList";

export function Games() {
  let match = useRouteMatch();
  const [week, setWeek] = useState(1);
  const value = { week, setWeek };

  return (
    <Switch>
      <Route path={`${match.path}/:gameId`}>
        <GameEdit />
      </Route>
      <Route path={match.path}>
        <WeekProvider>
          <GameList />
        </WeekProvider>
      </Route>
    </Switch>
  );
}
