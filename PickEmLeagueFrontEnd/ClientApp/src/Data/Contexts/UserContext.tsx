import { useContext } from "react";
import { createContext } from "react";
import { User } from "../../Apis";

export type UserContent = {
  user: User | undefined;
  loggedIn: boolean;
  setUser: (user: User) => void;
};

export const UserContext = createContext<UserContent>({
  user: undefined,
  loggedIn: false,
  setUser: () => console.warn("no user provider"),
});

export const useUserContext = () => useContext(UserContext);
