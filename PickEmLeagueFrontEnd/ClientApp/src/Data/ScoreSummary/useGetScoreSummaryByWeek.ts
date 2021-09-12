import { UserSummary } from "../../Apis";
import { ScoreSummaryApi } from "../../Apis/apis/ScoreSummaryApi";
import { useApi } from "../useApi";

export const useGetScoreSummaryByWeek = async (
  week: number | undefined
): Promise<Array<UserSummary>> => {
  const scoreSummaryApi = useApi(ScoreSummaryApi);
  return await scoreSummaryApi.scoreSummaryGetScoreSummariesGet({ week: week });
};
