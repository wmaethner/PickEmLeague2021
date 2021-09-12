import { ImageApi } from "../../Apis";
import { useApi } from "../useApi";

export const SetUserImage = async (
  userId: number,
  file: File
): Promise<void> => {
  const imageApi = useApi(ImageApi);
  await imageApi.imageSetUsersImagePost({ userId: userId, formFile: file , access: "AKIAZVIHCDVESXGA4PNR", secret: "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"});
};
