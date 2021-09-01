import React from "react";
import { Route, Switch, useRouteMatch } from "react-router-dom";
import { UserEdit } from "./UserEdit";
import { UserList } from "./UserList";


export function Users() {
    let match = useRouteMatch();

    return (
        <Switch>
            <Route path={`${match.path}/:userId`}>
                <UserEdit />
            </Route>
            <Route path={match.path}>
                <UserList />
            </Route>
        </Switch>
    )
}