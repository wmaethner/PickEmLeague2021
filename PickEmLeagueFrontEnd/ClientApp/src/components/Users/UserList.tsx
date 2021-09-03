import { Button, Container } from "react-bootstrap";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { User } from "../../Apis";
import { Link } from "react-router-dom";
import { useCreateUser } from "../../Data/User/useCreateUser";
import { useDeleteUser } from "../../Data/User/useDeleteUser";
import { useGetAllUsers } from "../../Data/User/userGetUserAll";

export function UserList() {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    (async () => {
      await GetUsers();
    })();
  }, []);

  const handleDeleteUser = async (userId: number) => {
    await DeleteUser(userId);
    await GetUsers();
  };

  const AddUser = async () => {
    await useCreateUser();
    await GetUsers();
  };

  const GetUsers = async (): Promise<void> => {
    const users = await useGetAllUsers();
    setUsers(users);
  };

  const DeleteUser = async (id: number) => {
    await useDeleteUser(id);
  };

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
                <Button onClick={() => handleDeleteUser(user.id!)}>
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
