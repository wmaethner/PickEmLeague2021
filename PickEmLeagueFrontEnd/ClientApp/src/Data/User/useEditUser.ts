import { User, UserApi } from "../../Apis";
import { useApi } from "../useApi";

export const useEditUser = async (user: User): Promise<void> => {
    const userApi = useApi(UserApi);
    return await userApi.userUpdateUserPut({ user });
};
