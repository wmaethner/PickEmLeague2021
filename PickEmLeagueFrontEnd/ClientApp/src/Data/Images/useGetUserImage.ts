import { ImageApi } from "../../Apis";
import { useApi } from "../useApi";

export const useGetUserImage = async (
  userId: number
): Promise<string | null> => {
  const imageApi = useApi(ImageApi);
  let response = await imageApi.imageGetUsersImageGet({ userId: userId, access: "AKIAZVIHCDVESXGA4PNR", secret: "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" });
  return response ? JSON.parse(response) : "";
};
