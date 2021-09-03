import { User, UserApi } from "../../Apis";
import { useApi } from "../useApi";

export const useGetAllUsers = async (): Promise<User[]> => {
  const userApi = useApi(UserApi);
  return await userApi.userGetAllGet();
};
