import React, { FormEvent } from "react";
import { useState } from "react";
import { useHistory } from "react-router";
import { Container } from "reactstrap";
import { useAttemptLogin } from "../../Data/Authentication/useAttemptLogin";
import { useUserContext } from "../../Data/Contexts/UserContext";
import { routes } from "../routes";

export interface LoginParams {
  email: string;
  password: string;
}

export function LoginForm() {
  const { setUser } = useUserContext();
  const [hasError, setError] = useState<boolean>(false);
  const history = useHistory();
  const [loginParams, setParams] = useState<LoginParams>({
    email: "",
    password: "",
  });

  const HandleLogin = async (evt: FormEvent) => {
    evt.preventDefault();
    const user = await useAttemptLogin(loginParams.email, loginParams.password);
    if (user) {
      setUser(user);
      history.push(routes.root.path);
    } else {
      setError(true);
    }
  };

  let errorMessage = () => {
    return hasError ? <div>There was an error logging in</div> : <div></div>;
  };

  return (
    <Container>
      <div className="auth-wrapper">
        <div className="auth-inner">
          {errorMessage()}
          <form onSubmit={HandleLogin}>
            <h3>Sign In</h3>

            <div className="form-group">
              <label>Email address</label>
              <input
                type="email"
                className="form-control"
                value={loginParams.email}
                onChange={(e) => setParams({ ...loginParams, email: e.target.value })} />
            </div>

            <div className="form-group">
              <label>Password</label>
              <input
                type="password"
                className="form-control"
                value={loginParams.password}
                onChange={(e) => setParams({ ...loginParams, password: e.target.value })} />
            </div>

            {/* <div className="form-group">
        <div className="custom-control custom-checkbox">
          <input type="checkbox" className="custom-control-input" id="customCheck1" />
          <label className="custom-control-label" htmlFor="customCheck1">Remember me</label>
        </div>
      </div> */}

            <button type="submit" className="btn btn-primary btn-block">Submit</button>
            {/* <p className="forgot-password text-right">
        Forgot <a href="#">password?</a>
      </p> */}
          </form>
        </div>
      </div>
    </Container>
  );
}
