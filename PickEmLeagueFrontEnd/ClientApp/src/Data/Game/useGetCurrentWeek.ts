import { GameApi } from "../../Apis/apis/GameApi";
import { useApi } from "../useApi";

export const useGetCurrentWeek = async (): Promise<number> => {
  const gameApi = useApi(GameApi);
  return await gameApi.gameCurrentWeekGet();
};
