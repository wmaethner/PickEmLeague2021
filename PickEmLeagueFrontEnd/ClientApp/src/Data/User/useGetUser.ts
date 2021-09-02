import { User, UserApi } from "../../Apis";
import { useApi } from "../useApi";

export const useGetUser = async (id: number): Promise<User> => {
  const userApi = useApi(UserApi);
  return await userApi.userGetUserGet({ id: id });
};
