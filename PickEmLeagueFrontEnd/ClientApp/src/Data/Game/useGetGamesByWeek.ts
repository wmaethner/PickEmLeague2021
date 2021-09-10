import { GameApi } from "../../Apis/apis/GameApi";
import { Game } from "../../Apis/models/Game";
import { useApi } from "../useApi";

export const useGetGamesByWeek = async (week: number): Promise<Game[]> => {
  const gameApi = useApi(GameApi);
  return await gameApi.gameGetGamesForWeekGet({ week: week });
};
