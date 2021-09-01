import { BaseAPI, Configuration } from "../Apis";

type ApiConstructor<T extends BaseAPI> = new (config: Configuration) => T;

export function useApi<T extends BaseAPI>(api: ApiConstructor<T>) {
  console.log(process.env);
  return new api(
    new Configuration({
      basePath: process.env.REACT_APP_BASE_URL,
    })
  );
}
