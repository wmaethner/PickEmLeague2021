import { route } from "static-route-paths";

export const routes = route({
  root: route(),
  users: route("users"),
});
