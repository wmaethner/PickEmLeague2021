import { GamePickApi } from "../../Apis/apis/GamePickApi";
import { GamePick } from "../../Apis/models/GamePick";
import { useApi } from "../useApi";

export const useGetGamePicksByUserAndWeek = async (
  userId: number,
  week: number
): Promise<GamePick[]> => {
  const gamePickApi = useApi(GamePickApi);
  return await gamePickApi.gamePickGetGamePicksByUserAndWeekGet({
    userId: userId,
    week: week,
  });
};
