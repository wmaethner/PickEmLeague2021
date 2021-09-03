import { GameApi } from "../../Apis/apis/GameApi";
import { Game } from "../../Apis/models/Game";
import { useApi } from "../useApi";

export const useEditGame = async (game: Game): Promise<void> => {
  const gameApi = useApi(GameApi);
  return await gameApi.gameUpdatePut({ game });
};
