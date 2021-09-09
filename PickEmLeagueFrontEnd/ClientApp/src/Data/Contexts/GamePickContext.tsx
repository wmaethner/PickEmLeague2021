import { useContext } from "react";
import { createContext } from "react";
import { GamePick, User } from "../../Apis";

export type GamePickContent = {
  gamePick: GamePick;
  setGamePick: (gamePick: GamePick) => void;
};

export const GamePickContext = createContext<GamePickContent>({
  gamePick: {},
  setGamePick: () => console.warn("no gamepick provider"),
});

export const useGamePickContext = () => useContext(GamePickContext);
