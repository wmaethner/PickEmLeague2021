import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { createContext } from "react";
import { Team } from "../../Apis/models/Team";
import { useGetTeamAll } from "../Teams/useGetTeamAll";

export type TeamContent = {
  teams: Team[];
};

export const TeamContext = createContext<TeamContent>({
  teams: [],
});

export const TeamProvider: React.FC = ({ children }) => {
  const [teams, setTeams] = useState<Team[]>([]);

  useEffect(() => {
    async function GetTeams() {
      setTeams(await useGetTeamAll());
    }
    GetTeams();
  }, []);

  return (
    <TeamContext.Provider value={{ teams }}>{children}</TeamContext.Provider>
  );
};
