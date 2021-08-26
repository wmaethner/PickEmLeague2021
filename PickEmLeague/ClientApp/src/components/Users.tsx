import React, { Component } from 'react';
// import { makeStyles, TableBody } from '@material-ui/core';
// import { TableCell, TableContainer, Table, TableHead, TableRow } from '@material-ui/core';
import { Button, Table } from 'react-bootstrap';

export interface State {
    users: User[];
    loading: boolean;
    editableRow: number | null;
}

export interface User {
    id: number;
    name: string;
    email: string;
}

export interface UserTableProps {
    users: User[];
}

interface UserTableState {
    users: User[];
    editableRow: number | null;
}

class UserTable extends React.Component<UserTableProps, UserTableState> {

    constructor(props: UserTableProps) {
        super(props);
        this.setState({ 
            users: props.users,
            editableRow: null,
        });
    }

    handleEditRowClick(row: number) {
        this.setState({
            users: this.state.users,
            editableRow: row
        });
    }

    render() {
        return (
            this.state == null ? "" :
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.users.map(user =>
                        <tr key={user.id} contentEditable={this.state.editableRow === user.id}>
                            <td>{user.id}</td>
                            <td>{user.name}</td>
                            <td>{user.email}</td>
                            <td><Button onClick={() => this.handleEditRowClick(user.id)}></Button></td>
                        </tr>
                    )}
                </tbody>
            </Table>
        );
    }
}



export class Users extends React.Component<any, State> {
    static displayName = Users.name;
    state: State;

    constructor(props: any) {
        super(props);
        this.state = { users: [], loading: true, editableRow: null };
    }


    componentDidMount() {
        this.getUsers();
    }

    handleEditRowClick(row: number) {
        this.setState({
            editableRow: row
        });
    }

    userTable() {
        return (
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.users.map(user =>
                        <tr key={user.id} contentEditable={this.state.editableRow === user.id}>
                            <td>{user.id}</td>
                            <td>{user.name}</td>
                            <td>{user.email}</td>
                            <td><Button onClick={() => this.handleEditRowClick(user.id)}></Button></td>
                        </tr>
                    )}
                </tbody>
            </Table>
        );
    }

    // static renderForecastsTable(users: User[]) {
    //     return (
    //         <Table striped bordered hover>
    //             <thead>
    //                 <tr>
    //                     <th>Id</th>
    //                     <th>Name</th>
    //                     <th>Email</th>
    //                     <th></th>
    //                 </tr>
    //             </thead>
    //             <tbody>
    //                 {users.map(user =>
    //                     <tr key={user.id} >
    //                         <td>{user.id}</td>
    //                         <td>{user.name}</td>
    //                         <td>{user.email}</td>
    //                         <td><Button></Button></td>
    //                     </tr>
    //                 )}
    //             </tbody>
    //         </Table>
    //         // <TableContainer>
    //         //     <Table>
    //         //         <TableHead>
    //         //             <TableRow>
    //         //                 <TableCell>Id</TableCell>
    //         //                 <TableCell>Name</TableCell>
    //         //                 <TableCell>Email</TableCell>
    //         //             </TableRow>
    //         //         </TableHead>
    //         //         <TableBody>
    //         //             {users.map(user =>
    //         //                 <TableRow>
    //         //                     <TableCell>{user.id}</TableCell>
    //         //                     <TableCell>{user.name}</TableCell>
    //         //                     <TableCell>{user.email}</TableCell>
    //         //                 </TableRow>)}
    //         //         </TableBody>
    //         //     </Table>
    //         // </TableContainer>          
    //     );
    // }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.userTable(); //Users.renderForecastsTable(this.state.users);

        return (
            <div>
                <h1 id="tabelLabel">Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                <button onClick={() => { this.addUser(); }}>
                    Add User
                </button>
                {contents}
            </div>
        );
    }

    async getUsers() {
        const response = await fetch('user');
        const data = await response.json();
        this.setState({ users: data, loading: false });
    }

    addUser() {
        fetch('user', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: "Will Maethner",
                email: "test@gmail.com"
            })
        }).then(() => { this.getUsers() });
    }
}
