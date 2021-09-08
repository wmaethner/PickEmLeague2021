import { User, UserApi } from "../../Apis";
import { AuthenticationApi } from "../../Apis/apis/AuthenticationApi";
import { LoginResponse } from "../../Apis/models/LoginResponse";
import { useApi } from "../useApi";

export const useAttemptLogin = async (
  email: string,
  passHash: string
): Promise<User | undefined> => {
  const authApi = useApi(AuthenticationApi);
  const response: LoginResponse = await authApi.authenticationAttemptLoginPost({
    email: email,
    passwordHash: passHash,
  });
  return response.loggedIn ? response.user : undefined;
};
