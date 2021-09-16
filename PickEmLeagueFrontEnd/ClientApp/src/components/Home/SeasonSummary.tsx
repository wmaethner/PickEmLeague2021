import React, { useContext, useEffect, useState } from "react";
import { useUserContext } from "../../Data/Contexts/UserContext";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { Table } from "reactstrap";
import { Game, GameResult, User } from "../../Apis";
import { useGetAllGames } from "../../Data/Game/userGetGameAll";
import { userDisplay } from "../Home";

export type UserSeasonSummary = {
    user: User;
    displayName: string;
    score: number;
    correctPicks: number;
}

export type SeasonSummaryProps = {
    seasonSummaries: UserSeasonSummary[];
}

export function SeasonSummary(props: SeasonSummaryProps) {
    const { user, loggedIn } = useUserContext();
    const [games, setGames] = useState<Game[]>([]);
    const { week } = useContext(WeekContext);

    useEffect(() => {
        async function GetGames() {
            setGames(await useGetAllGames());
        }
        GetGames();
    }, []);

    function displayCorrectPicks(correctPicks: number | undefined) {
        let count = correctPicks ? correctPicks : 0;

        return (
            <div>
                <label>
                    {count}/{getGamesPlayed()}
                </label>
            </div>
        );
    }

    function getGamesPlayed() {
        return games.filter((game) => game.gameResult !== GameResult.NotPlayed)
            .length;
    }

    return (
        <div>
            <Table>
                <thead>
                    <tr>
                        <th>User</th>
                        <th>
                            <label id="pick-status" data-tip data-for="pick-status">
                                Pick Status
                            </label>
                        </th>
                        <th>Season Score</th>
                    </tr>
                </thead>
                <tbody>
                    {props.seasonSummaries?.sort((a, b) => (a.score < b.score ? 1 : -1))
                        .map((summary, index) => (
                            <tr key={summary.user?.id}>
                                <td>{userDisplay(summary.user!, index)}</td>
                                <td>
                                    {displayCorrectPicks(summary.correctPicks)}
                                </td>
                                <td>{summary.score}</td>
                            </tr>
                        ))}
                </tbody>
            </Table>
        </div>
    )
}