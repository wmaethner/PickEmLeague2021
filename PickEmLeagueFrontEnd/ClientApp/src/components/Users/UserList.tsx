import { Button, Container } from "react-bootstrap";
import React, { Component, useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { User } from "../../Apis";
import { useGetUserList } from "../../Data/User/useGetUserList";
import { Link } from "react-router-dom";
import { useCreateUser } from "../../Data/User/useCreateUser";
import { useHistory } from "react-router-dom";

export function UserList() {
  const history = useHistory();
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    async function GetUsers() {
      setUsers(await useGetUserList());
    }
    GetUsers();
  }, []);

  return (
    <Container>
      <Table striped bordered>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Admin</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.name}</td>
              <td>{user.email}</td>
              <td>{user.isAdmin}</td>
              <td>
                <Link to={"users/" + user.id} className="btn">
                  Edit
                </Link>
              </td>
              <td>
                <Link to={"users/" + user.id} className="btn btn-primary">
                  Delete
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button onClick={AddUser}>Add User</Button>
    </Container>
  );
}

async function AddUser() {
  const user = await useCreateUser();
  window.location.pathname += "/" + user.id;
}
