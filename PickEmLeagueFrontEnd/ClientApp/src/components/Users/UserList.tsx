import { Button, Container } from "react-bootstrap";
import React, { Component, useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { User } from "../../Apis";
import { useGetUserList } from "../../Data/User/useGetUserList";
import { Link, useHistory } from "react-router-dom";
import { useCreateUser } from "../../Data/User/useCreateUser";
import { useDeleteUser } from "../../Data/User/useDeleteUser";
import { routes } from "../routes";

export function UserList() {
  const history = useHistory();
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    (async () => {
      await GetUsers();
    })()
  }, []);

  const GetUsers = async (): Promise<void> => {
    const users = await useGetUserList();
    setUsers(users);
  }

  const handleDeleteUser = async (userId: number) => {
    await DeleteUser(userId);
    await GetUsers();
  }

  const AddUser = async () => {
    const user = await useCreateUser();
    await GetUsers();
  }

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
              <td>{user.isAdmin ? "Yes" : "No"}</td>
              <td>
                <Link to={"users/" + user.id} className="btn btn-primary">
                  Edit
                </Link>
              </td>
              <td>
                <Button onClick={(e) => handleDeleteUser(user.id!)}>
                  Delete
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button onClick={AddUser}>Add User</Button>
      <Button onClick={GetUsers}>Get Users</Button>
    </Container>
  );
}

async function AddUser() {
  const user = await useCreateUser();
  //window.location.pathname += "/" + user.id;
}

async function DeleteUser(userId: number) {
  await useDeleteUser(userId);
  //setUsers(await useGetUserList());
}

async function GetUsers(): Promise<User[]> {
  return await useGetUserList();
}
