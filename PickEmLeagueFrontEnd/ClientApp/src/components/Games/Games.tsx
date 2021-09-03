import React from "react";
import { Route, Switch, useRouteMatch } from "react-router-dom";
import { GameEdit } from "./GameEdit";
import { GameList } from "./GameList";

export function Games() {
  let match = useRouteMatch();

  return (
    <Switch>
      <Route path={`${match.path}/:gameId`}>
        <GameEdit />
      </Route>
      <Route path={match.path}>
        <GameList />
      </Route>
    </Switch>
  );
}
