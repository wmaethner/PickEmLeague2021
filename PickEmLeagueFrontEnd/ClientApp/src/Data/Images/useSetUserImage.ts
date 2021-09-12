import { ImageApi } from "../../Apis";
import { useApi } from "../useApi";

export const SetUserImage = async (userId: number, file: File): Promise<void> => {
  const imageApi = useApi(ImageApi);
  await imageApi.imagePost({ userId: userId, formFile: file });
};
