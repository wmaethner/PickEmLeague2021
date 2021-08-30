import { BaseAPI, Configuration } from "../Apis";

type ApiConstructor<T extends BaseAPI> = new (config: Configuration) => T;

export function useApi<T extends BaseAPI>(api: ApiConstructor<T>) {
  return new api(
    new Configuration({
      basePath: "https://localhost:5001",
    })
  );
}
