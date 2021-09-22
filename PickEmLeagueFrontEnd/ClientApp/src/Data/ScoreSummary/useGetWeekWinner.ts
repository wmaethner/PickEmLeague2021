import { User, UserSummary } from "../../Apis";
import { ScoreSummaryApi } from "../../Apis/apis/ScoreSummaryApi";
import { useApi } from "../useApi";

export const useGetWeekWinner = async (
  week: number | undefined
): Promise<User | undefined> => {
  const scoreSummaryApi = useApi(ScoreSummaryApi);
  return await (
    await scoreSummaryApi.scoreSummaryGetWeekWinnerGet({ week: week })
  ).winner;
};
