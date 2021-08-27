import React, { Component } from 'react';
import { Table } from 'react-bootstrap';
import { User } from '../../Models/User';

interface UserListState {
    users: User[];
    loading: boolean;
}

export class UserList extends React.Component<any, UserListState> {
    constructor(props: any) {
        super(props);
        this.state = { 
            users: [], 
            loading: true 
        };
    }

    componentDidMount() {
        this.getUsers();
    }

    render() {
        return (
            <Table striped bordered>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Admin</th>
                    </tr>
                </thead>
                <tbody>
                    {this.state.users.map(user =>
                        <tr>
                            <td>{user.id}</td>
                            <td>{user.name}</td>
                            <td>{user.email}</td>
                            <td>{user.admin}</td>
                        </tr>
                    )}
                </tbody>
            </Table>
        );
    }

    async getUsers() {
        const response = await fetch('user');
        const data = await response.json();
        this.setState({ users: data, loading: false });
    }
}