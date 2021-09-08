import React, { FormEvent } from "react";
import { useState } from "react";
import { useContext } from "react";
import { Col, Container, Form, Label, Row } from "reactstrap";
import { useAttemptLogin } from "../../Data/Authentication/useAttemptLogin";
import { UserContext, useUserContext } from "../../Data/Contexts/UserContext";

export interface LoginParams {
  email: string;
  password: string;
}

export function LoginForm() {
  const { setUser } = useUserContext();
  const [hasError, setError] = useState<boolean>(false);
  const [loginParams, setParams] = useState<LoginParams>({
    email: "",
    password: "",
  });

  const HandleLogin = async (evt: FormEvent) => {
    evt.preventDefault();
    const user = await useAttemptLogin(loginParams.email, loginParams.password);
    if (user) {
      setUser(user);
    } else {
      setError(true);
    }
  };

  let errorMessage = () => {
    return hasError ? <div>There was an error logging in</div> : <div></div>;
  };

  return (
    <Container>
      {errorMessage()}
      <Form onSubmit={HandleLogin}>
        <Row>
          <Col>
            <Label>Email: </Label>
          </Col>
          <Col>
            <input
              type="text"
              value={loginParams.email}
              onChange={(e) =>
                setParams({ ...loginParams, email: e.target.value })
              }
            />
          </Col>
        </Row>
        <Row>
          <Col>
            <Label>Password: </Label>
          </Col>
          <Col>
            <input
              type="text"
              value={loginParams.password}
              onChange={(e) =>
                setParams({ ...loginParams, password: e.target.value })
              }
            />
          </Col>
        </Row>
        <input type="submit" value="Save" />
      </Form>
    </Container>
  );
}
