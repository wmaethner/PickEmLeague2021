import { GamePick, GamePickApi } from "../../Apis";
import { GameApi } from "../../Apis/apis/GameApi";
import { Game } from "../../Apis/models/Game";
import { useApi } from "../useApi";

export const useUpdateGamePicks = async (
  picks: GamePick[]
): Promise<boolean> => {
  const gamePickApi = useApi(GamePickApi);
  return await gamePickApi.gamePickUpdateGamePicksPost({
    gamePick: picks,
  });
};
