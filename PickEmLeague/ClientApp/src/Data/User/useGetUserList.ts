import { User, UserApi } from "../../Apis";
import { useApi } from "../useApi";

export const useGetUserList = async (): Promise<User[]> => {
  const userApi = useApi(UserApi);
  return await userApi.userGet();
};
