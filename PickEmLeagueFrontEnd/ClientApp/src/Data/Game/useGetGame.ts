import { User, UserApi } from "../../Apis";
import { GameApi } from "../../Apis/apis/GameApi";
import { Game } from "../../Apis/models/Game";
import { useApi } from "../useApi";

export const useGetGame = async (id: number): Promise<Game> => {
  const gameApi = useApi(GameApi);
  return await gameApi.gameGetGet({ id: id });
};
