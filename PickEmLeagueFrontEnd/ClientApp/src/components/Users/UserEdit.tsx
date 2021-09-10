import React, { FormEvent, useEffect, useState } from "react";
import { Container, Form, Row } from "react-bootstrap";
import { useHistory, useParams } from "react-router-dom";
import { FormGroup, Label } from "reactstrap";
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
    <Container className="data-table">
      <Form onSubmit={handleSubmit}>
        <FormGroup>
          <Label>Name: </Label>
          <input
            type="text"
            value={user.name || ""}
            onChange={(e) => setUser({ ...user, name: e.target.value })}
          />
        </FormGroup>
        <FormGroup>
          <Label>Email: </Label>
          <input
            type="text"
            value={user.email || ""}
            onChange={(e) => setUser({ ...user, email: e.target.value })}
          />
        </FormGroup>
        <FormGroup>
          <Label>Password: </Label>
          <input
            type="text"
            value={user.passwordHash || ""}
            onChange={(e) => setUser({ ...user, passwordHash: e.target.value })}
          />
        </FormGroup>
        <FormGroup>
          <Label>Admin: </Label>
          <input
            type="checkbox"
            checked={user.isAdmin || false}
            onChange={(e) => setUser({ ...user, isAdmin: e.target.checked })}
          />
        </FormGroup>
        <input type="submit" value="Save" />
      </Form>
    </Container>
  );
}
