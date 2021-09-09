import { GameApi } from "../../Apis/apis/GameApi";
import { useApi } from "../useApi";

export const useDeleteGame = async (id: number): Promise<void> => {
  const gameApi = useApi(GameApi);
  return await gameApi.gameDeleteGameDelete({ id: id });
};
