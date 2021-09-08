import React from "react";
import { useState } from "react";
import { createContext } from "react";

export type WeekContent = {
  week: number | undefined;
  setWeek: (week: number) => void;
};

export const WeekContext = createContext<WeekContent>({
  week: 1,
  setWeek: () => {},
});

export const WeekProvider: React.FC = ({ children }) => {
  const [week, setCurrentWeek] = useState<number>(1);

  const setWeek = (week: number) => {
    setCurrentWeek(week);
  };

  return (
    <WeekContext.Provider value={{ week, setWeek }}>
      {children}
    </WeekContext.Provider>
  );
};
