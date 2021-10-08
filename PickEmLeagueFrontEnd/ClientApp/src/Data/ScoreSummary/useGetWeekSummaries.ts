import { UserSummary, WeekSummary } from "../../Apis";
import { ScoreSummaryApi } from "../../Apis/apis/ScoreSummaryApi";
import { useApi } from "../useApi";

export const useGetWeekSummaries = async (
  week: number | undefined
): Promise<Array<WeekSummary>> => {
  const scoreSummaryApi = useApi(ScoreSummaryApi);
  return await scoreSummaryApi.scoreSummaryGetWeekSummariesGet({ week: week });
};
