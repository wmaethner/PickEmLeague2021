import { GameApi } from "../../Apis/apis/GameApi";
import { Game } from "../../Apis/models/Game";
import { useApi } from "../useApi";

export const useCreateGame = async (week: number): Promise<Game> => {
  const gameApi = useApi(GameApi);
  return await gameApi.gameCreateGameForWeekPost({ week: week });
};
