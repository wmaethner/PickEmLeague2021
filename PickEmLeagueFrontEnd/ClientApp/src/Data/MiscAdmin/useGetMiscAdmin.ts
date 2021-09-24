import { MiscAdminApi } from "../../Apis/apis/MiscAdminApi";
import { MiscAdmin } from "../../Apis/models/MiscAdmin";
import { useApi } from "../useApi";

export const useGetMiscAdmin = async (): Promise<MiscAdmin> => {
  const miscAdminApi = useApi(MiscAdminApi);
  return await miscAdminApi.miscAdminGetMiscAdminGet();
};
