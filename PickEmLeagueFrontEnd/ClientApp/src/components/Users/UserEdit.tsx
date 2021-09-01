import { Button, Container } from "react-bootstrap";
import React, { Component } from "react";
import { Table } from "react-bootstrap";
import { User } from "../../Apis";
import { useGetUserList } from "../../Data/User/useGetUserList";
import { Link, useParams, useRouteMatch } from "react-router-dom";


interface UserEditState {
    userId: number;
}

type UserEditParams = {
    userId: string;
}

export function UserEdit() {
    //let { url } = useRouteMatch();
    let id = parseInt(useParams<UserEditParams>().userId);

    return (
        <div>
            <h3>ID: {id}</h3>
        </div>
    );
}

// export class UserEdit extends React.Component<any, UserEditState> {
//   constructor(props: any) {
//     super(props);
//     const url = useRouteMatch();
//     const id = parseInt(useParams());

//     this.state = {
//       userId: id
//     };
//   }

//   componentDidMount() {
//   }

//   render() {
//     return (
//         <div>
//         <h3>ID: {this.state.userId}</h3>

//       </div>
//     );
//   }
// }
