import { User, UserApi } from "../../Apis";
import { useApi } from "../useApi";

export const useDeleteUser = async (id: number): Promise<void> => {
  const userApi = useApi(UserApi);
  return await userApi.userDeleteDelete({ id: id });
};
