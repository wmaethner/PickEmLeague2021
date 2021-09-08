import React, { FormEvent, useEffect, useState } from "react";
import { Container, Form, Row } from "react-bootstrap";
import { useHistory, useParams } from "react-router-dom";
import { User } from "../../Apis";
import { useEditUser } from "../../Data/User/useEditUser";
import { useGetUser } from "../../Data/User/useGetUser";

type UserEditParams = {
  userId: string;
};

export function UserEdit() {
  const history = useHistory();
  const [user, setUser] = useState<User>({});
  const id = parseInt(useParams<UserEditParams>().userId);

  useEffect(() => {
    async function GetUser() {
      setUser(await useGetUser(id));
    }
    GetUser();
  }, [id]);

  const handleSubmit = async (evt: FormEvent) => {
    evt.preventDefault();
    SaveUser(user);
    history.goBack();
  };

  const SaveUser = async (user: User): Promise<void> => {
    await useEditUser(user);
  };

  return (
    <Container>
      <Form onSubmit={handleSubmit}>
        <Row>
          <label>
            Name:
            <input
              type="text"
              value={user.name || ""}
              onChange={(e) => setUser({ ...user, name: e.target.value })}
            />
          </label>
        </Row>
        <Row>
          <label>
            Email:
            <input
              type="text"
              value={user.email || ""}
              onChange={(e) => setUser({ ...user, email: e.target.value})}
            />
          </label>
        </Row>
        <Row>
          <label>
            Password:
            <input
              type="text"
              value={user.passwordHash || ""}
              onChange={(e) => setUser({ ...user, passwordHash: e.target.value})}
            />
          </label>
        </Row>
        <Row>
          <label>
            Admin:
            <input
              type="checkbox"
              checked={user.isAdmin || false}
              onChange={(e) => setUser({ ...user, isAdmin: e.target.checked})}
            />
          </label>
        </Row>
        <input type="submit" value="Save" />
      </Form>
    </Container>
  );
}

// async function SaveUser(user: User) {
//   await useEditUser(user);
// }
