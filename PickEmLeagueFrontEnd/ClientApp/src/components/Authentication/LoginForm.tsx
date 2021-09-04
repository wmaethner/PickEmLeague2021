import React, { FormEvent } from "react";
import { useState } from "react";
import { useContext } from "react";
import { Col, Container, Form, Label, Row } from "reactstrap";
import { UserContext } from "../../Data/User/UserContext";


export interface LoginParams {
    email?: string;
    password?: string;
}

export function LoginForm() {
    const userContext = useContext(UserContext);
    const [loginParams, setParams] = useState<LoginParams>({});

    const handleLogin = async (evt: FormEvent) => {
        evt.preventDefault();
        //TODO: Actually attempt login
        userContext.setUser({ id: 2, name: "test", email: loginParams.email, isAdmin: true});
        //alert("Email: " + loginParams.email + " Password: " + loginParams.password);
    };

    return (
        <Container>
            <Form onSubmit={handleLogin}>
                <Row>
                    <Col><Label>Email: </Label></Col>
                    <Col>
                        <input type="text"
                            value={loginParams.email}
                            onChange={(e) => setParams({ ...loginParams, email: e.target.value})} />
                    </Col>
                </Row>
                <Row>
                    <Col><Label>Password: </Label></Col>
                    <Col>
                        <input type="text"
                            value={loginParams.password}
                            onChange={(e) => setParams({ ...loginParams, password: e.target.value})} />
                    </Col>
                </Row>
                <input type="submit" value="Save" />
            </Form>
        </Container>
    )
}