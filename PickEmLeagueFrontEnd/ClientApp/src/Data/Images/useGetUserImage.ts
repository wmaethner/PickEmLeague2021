import { ImageApi } from "../../Apis";
import { useApi } from "../useApi";

export const useGetUserImage = async (userId: number): Promise<string | null> => {
  const imageApi = useApi(ImageApi);
  let response = await imageApi.imageGet({ userId: userId });
  return response ? JSON.parse(response) : "";
};
