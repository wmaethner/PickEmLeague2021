import { ImageApi } from "../../Apis";
import { useApi } from "../useApi";

export const useGetUserImage = async (userId: number): Promise<string> => {
    const imageApi = useApi(ImageApi);
    let response = await imageApi.imageGet({ userId: userId });
    return JSON.parse(response);
};
