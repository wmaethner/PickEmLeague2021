import { GameApi } from "../../Apis/apis/GameApi";
import { Game } from "../../Apis/models/Game";
import { useApi } from "../useApi";

export const useGetAllGames = async (): Promise<Game[]> => {
  const gameApi = useApi(GameApi);
  return await gameApi.gameGetAllGet();
};
