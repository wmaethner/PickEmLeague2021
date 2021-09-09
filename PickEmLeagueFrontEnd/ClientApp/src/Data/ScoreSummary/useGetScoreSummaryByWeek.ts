import { GamePickApi } from "../../Apis/apis/GamePickApi";
import { ScoreSummaryApi } from "../../Apis/apis/ScoreSummaryApi";
import { GamePick } from "../../Apis/models/GamePick";
import { ScoreSummaryResponse } from "../../Apis/models/ScoreSummaryResponse";
import { UsersWeeklyScoreSummary } from "../../Apis/models/UsersWeeklyScoreSummary";
import { useApi } from "../useApi";

export const useGetScoreSummaryByWeek = async (
  week: number
): Promise<Array<UsersWeeklyScoreSummary>> => {
  const scoreSummaryApi = useApi(ScoreSummaryApi);
  return await scoreSummaryApi.scoreSummaryGetScoreSummaryGet({ week: week });
};
