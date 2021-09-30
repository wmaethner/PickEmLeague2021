import React, { useContext, useEffect, useState, PureComponent } from "react";
import { Col, Container, Row, Button, Collapse } from "reactstrap";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { Game, GamePick, GameResult, User } from "../../Apis";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { useGetGamesByWeek } from "../../Data/Game/useGetGamesByWeek";
import { useGetGamePicksByWeek } from "../../Data/GamePicks/useGetGamePicksByWeek";
import { TeamDisplay } from "../Teams/TeamDisplay";
import { WeekSelector } from "../Week/WeekSelector";
import { Bar, BarChart, CartesianGrid, Legend, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts';
import { useGetAllUsers } from "../../Data/User/userGetUserAll";

type AveragePointsData = {
    name: string;
    averagePoints: number;
}

export function StatsPage() {
    const { week } = useContext(WeekContext);
    const [users, setUsers] = useState<User[]>([]);
    const [gamePicks, setGamePicks] = useState<GamePick[]>([]);
    const [games, setGames] = useState<Game[]>([]);

    useEffect(() => {
        async function GetGamePicks() {
            setUsers(await useGetAllUsers());
        }
        GetGamePicks();
    }, []);

    function getData(): AveragePointsData[] {
        let data: Array<AveragePointsData> = [];

        users.map(user => {
            data.push({
                name: user.name!,
                averagePoints: Math.floor(Math.random() * 100)
            })
        });

        return data;
    }

    const data = [
        {
            name: 'Page A',
            uv: 4000,
            pv: 2400,
            amt: 2400,
        },
        {
            name: 'Page B',
            uv: 3000,
            pv: 1398,
            amt: 2210,
        },
        {
            name: 'Page C',
            uv: 2000,
            pv: 9800,
            amt: 2290,
        },
        {
            name: 'Page D',
            uv: 2780,
            pv: 3908,
            amt: 2000,
        },
        {
            name: 'Page E',
            uv: 1890,
            pv: 4800,
            amt: 2181,
        },
        {
            name: 'Page F',
            uv: 2390,
            pv: 3800,
            amt: 2500,
        },
        {
            name: 'Page G',
            uv: 3490,
            pv: 4300,
            amt: 2100,
        },
    ];


    return (
        <div>
            <br />
            <Container fluid>
                {/* <ResponsiveContainer width="100%" height="100%"> */}
                    <BarChart
                        width={800}
                        height={500}
                        data={getData()}
                        margin={{
                            top: 5,
                            right: 30,
                            left: 20,
                            bottom: 5,
                        }}
                    >
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="name" />
                        <YAxis />
                        <Tooltip />
                        <Legend />
                        <Bar dataKey="averagePoints" fill="#8884d8" />
                        {/* <Bar dataKey="pv" fill="#8884d8" />
          <Bar dataKey="uv" fill="#82ca9d" /> */}
                    </BarChart>
                {/* </ResponsiveContainer> */}
            </Container>
        </div>
    );
}