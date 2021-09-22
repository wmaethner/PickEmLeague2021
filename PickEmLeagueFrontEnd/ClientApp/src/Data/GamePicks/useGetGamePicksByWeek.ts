import { GamePickApi } from "../../Apis/apis/GamePickApi";
import { GamePick } from "../../Apis/models/GamePick";
import { useApi } from "../useApi";

export const useGetGamePicksByWeek = async (
  week: number
): Promise<GamePick[]> => {
  const gamePickApi = useApi(GamePickApi);
  return await gamePickApi.gamePickGetGamePickByWeekGet({
    week: week,
  });
};
