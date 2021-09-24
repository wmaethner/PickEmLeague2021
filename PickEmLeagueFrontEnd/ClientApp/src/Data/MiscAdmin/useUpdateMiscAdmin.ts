import { GamePick, GamePickApi } from "../../Apis";
import { GameApi } from "../../Apis/apis/GameApi";
import { MiscAdminApi } from "../../Apis/apis/MiscAdminApi";
import { Game } from "../../Apis/models/Game";
import { MiscAdmin } from "../../Apis/models/MiscAdmin";
import { useApi } from "../useApi";

export const useUpdateMiscAdmin = async (miscAdmin: MiscAdmin): Promise<void> => {
  const miscAdminApi = useApi(MiscAdminApi);
  return await miscAdminApi.miscAdminUpdateMiscAdminPut({ miscAdmin: miscAdmin });
};
