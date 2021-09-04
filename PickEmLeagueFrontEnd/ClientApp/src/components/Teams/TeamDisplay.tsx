import React from "react";
import { useContext } from "react";
import { Label } from "reactstrap";
import { TeamContext } from "../../Data/Teams/TeamsContext";

type Props = {
    id: number | undefined
}

export const TeamDisplay: React.FC<Props> = ({ id }) => {
    const teamContext = useContext(TeamContext);

    return (
        <Label>{id ? teamContext.teams[id - 1].name : "NA"}</Label>
    );
}

