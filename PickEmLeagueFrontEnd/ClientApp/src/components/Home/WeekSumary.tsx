import React, { useContext, useEffect, useState } from "react";
import { useUserContext } from "../../Data/Contexts/UserContext";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { Table } from "reactstrap";
import { useGetScoreSummaryByWeek } from "../../Data/ScoreSummary/useGetScoreSummaryByWeek";
import { Game, GameResult, User, UserSummary, WeekPickStatus } from "../../Apis";
import { BsCircleFill } from "react-icons/bs";
import ReactTooltip from "react-tooltip";
import { useGetGamesByWeek } from "../../Data/Game/useGetGamesByWeek";
import { ProfilePicture } from "../Images/ProfilePicture";

function useGetGames(week: number | undefined) {
    const [games, setGames] = useState<Game[]>([]);

    useEffect(() => {
        if (week === undefined) {
            return;
        }
        async function GetGames() {
            setGames(await useGetGamesByWeek(week!));
        }
        GetGames();
    }, [week]);

    return games;
}

function useGetScoreSummaries(week: number | undefined) {
    const [scoreSummary, setScoreSummary] = useState<Array<UserSummary>>([]);

    useEffect(() => {
        async function GetSummaries() {
            let response = await useGetScoreSummaryByWeek(week);
            setScoreSummary(response);
        }
        GetSummaries();
    }, [week]);

    return scoreSummary;
}

export function WeekSummary() {
    const { user, loggedIn } = useUserContext();
    const { week } = useContext(WeekContext);
    const games = useGetGames(week);
    const scoreSummary = useGetScoreSummaries(week);

    function userDisplay(user: User, index: number) {
        return (
            <div className="row align-items-center">
                <div className="col">{usersNameDisplay(user, index)}</div>
                <div className="col">
                    <ProfilePicture userId={user.id} />
                </div>
            </div>
        );
    }

    function usersNameDisplay(user: User, index: number) {
        if (user.username) {
            return (
                <div>
                    <label
                        id={"user-label-" + index}
                        data-tip
                        data-for={user.id?.toString()}
                    >
                        {user.username}
                    </label>
                    <ReactTooltip id={user.id?.toString()} type="info">
                        <span>{user.name}</span>
                    </ReactTooltip>
                </div>
            );
        }
        return (
            <div>
                <label>{user.name}</label>
            </div>
        );
    }

    function displayPickStatus(status: WeekPickStatus | undefined) {
        switch (status) {
            case WeekPickStatus.NotPicked:
                return (
                    <div>
                        <BsCircleFill color="red" size="1.5em" className="status-circle" />
                    </div>
                );
            case WeekPickStatus.PartiallyPicked:
                return (
                    <div>
                        <BsCircleFill
                            color="yellow"
                            size="1.5em"
                            className="status-circle"
                        />
                    </div>
                );
            case WeekPickStatus.FullyPicked:
                return (
                    <div>
                        <BsCircleFill
                            color="green"
                            size="1.5em"
                            className="status-circle"
                        />
                    </div>
                );
        }
        return "";
    }

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
                        <th>Week Score</th>
                        <th>Correct Picks</th>
                    </tr>
                </thead>
                <tbody>
                    {scoreSummary?.map((userScore, index) => (
                        <tr key={userScore.user?.id}>
                            <td>{userDisplay(userScore.user!, index)}</td>
                            <td>
                                {displayPickStatus(userScore.weekSummary?.weekPickStatus)}
                            </td>
                            <td>{userScore.weekSummary?.weekScore}</td>
                            <td>
                                {displayCorrectPicks(userScore.weekSummary?.correctPicks)}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
            <ReactTooltip id="pick-status" type="info">
                <p>Green = Fully picked</p>
                <p>Yellow = Some picked</p>
                <p>Red = None picked</p>
            </ReactTooltip>
        </div>

    )


}