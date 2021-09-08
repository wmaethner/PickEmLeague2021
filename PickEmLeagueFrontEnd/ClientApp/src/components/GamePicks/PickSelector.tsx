import React, { useEffect, useState } from "react";
import { ToggleButton, ToggleButtonGroup } from "react-bootstrap";
import {
    ButtonToggle,
    Button,
    Row,
    Col,
    Label,
    Card,
    CardBody,
    ButtonGroup,
} from "reactstrap";
import { Game, GamePick, GameResult } from "../../Apis";
import { useUpdateGamePicks } from "../../Data/GamePicks/useUpdateGamePicks";
import { TeamDisplay } from "../Teams/TeamDisplay";

type Props = {
    gamePick: GamePick;
    onPickChanged: (result: GameResult) => void;
};

export const PickSelector: React.FC<Props> = ({ gamePick, onPickChanged }) => {
    const [pick, setPick] = useState<GamePick>(gamePick);

    const UpdatePick = async (result: GameResult) => {
        setPick({ ...pick, pick: result });
    }

    useEffect(() => {
        async function UpdateGamePick() {
            useUpdateGamePicks([pick]);
        }
        UpdateGamePick();
    }, [pick]);


    const getClassName = (
        actualPick: GameResult | undefined,
        expectedPick: GameResult
    ) => {
        let result = "btn btn-block hover-overlay ";
        result += (actualPick === expectedPick) ? "btn-primary" : "btn-default";
        return result;
    };

    return (
        <Card>
            <CardBody>
                <Row>
                    <Col className="col-2">
                        <Label>{pick.wager}</Label>
                    </Col>
                    <Col className="col-5">
                        <Button className={getClassName(pick.pick, GameResult.HomeWin)} 
                            onClick={() => UpdatePick(GameResult.HomeWin)}>
                            <TeamDisplay id={pick.game?.homeTeamId}></TeamDisplay>
                        </Button>
                    </Col>
                    <Col className="col-5">
                        <Button className={getClassName(pick.pick, GameResult.AwayWin)} 
                            onClick={() => UpdatePick(GameResult.AwayWin)}>
                            <TeamDisplay id={pick.game?.awayTeamId}></TeamDisplay>
                        </Button>
                    </Col>
                </Row>
            </CardBody>
        </Card>
    );
};
