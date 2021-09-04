import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { createContext } from "react";
import { User } from "../../Apis";
import { Team } from "../../Apis/models/Team";

export type UserContent = {
    user: User | undefined,
    setUser: (user: User) => void
}

export const UserContext = createContext<UserContent>({
    user: undefined,
    setUser: () => {},
})

export const UserProvider: React.FC = ({ children }) => {
    const [user, setUserState] = useState<User>({ id: 1, name: "Test", email: "test@gmail.com", isAdmin: true});

    const setUser = (user: User) => {
        setUserState({ ...user });
    };

    return (
        <UserContext.Provider value={{ user, setUser }}>{children}</UserContext.Provider>
    );
};