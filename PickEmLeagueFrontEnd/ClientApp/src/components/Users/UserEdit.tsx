import React, { useEffect, useState } from "react";
import { Container, Row } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { User } from "../../Apis";
import { useGetUser } from "../../Data/User/useGetUser";

type UserEditParams = {
  userId: string;
};

export function UserEdit() {
  const [user, setUser] = useState<User>({});
  const id = parseInt(useParams<UserEditParams>().userId);

  useEffect(() => {
    async function GetUser() {
      setUser(await useGetUser(id));
    }
    GetUser();
  }, [id]);

  return (
    <Container>
      <Row>Name: {user.name}</Row>
      <Row>Email: {user.email}</Row>
      <Row>Admin: {user.isAdmin}</Row>
    </Container>
  );
}
