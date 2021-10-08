import { UserSummary, WeekSummary } from "../../Apis";
import { ScoreSummaryApi } from "../../Apis/apis/ScoreSummaryApi";
import { useApi } from "../useApi";

export const useGetSeasonSummaries = async (): Promise<Array<WeekSummary>> => {
  const scoreSummaryApi = useApi(ScoreSummaryApi);
  return await scoreSummaryApi.scoreSummaryGetSeasonSummariesGet();
};
