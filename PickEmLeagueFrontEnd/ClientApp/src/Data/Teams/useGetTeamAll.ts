import { TeamApi } from "../../Apis/apis/TeamApi";
import { Team } from "../../Apis/models/Team";
import { useApi } from "../useApi";

export const useGetTeamAll = async (): Promise<Team[]> => {
  const teamApi = useApi(TeamApi);
  return await teamApi.teamGet();
};
