import React, { useEffect, useState } from "react";
import { Col, Row } from "reactstrap";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";
import { User } from "../../Apis";
import { useGetWeekWinner } from "../../Data/ScoreSummary/useGetWeekWinner";
import { ProfilePicture } from "../Images/ProfilePicture";

export type WinnersCircleProps = {
  winner: User | undefined;
  message: string;
};

export function WinnersCircle(props: WinnersCircleProps) {
  return props.winner === undefined ? (
    <div></div>
  ) : (
    <Row
      className="flex-nowrap justify-content-center"
      style={{ backgroundColor: "gold" }}
    >
      <Col className="col-2">
        <ProfilePicture userId={props.winner?.id} />
      </Col>
      <Col className="col-4">
        <h2 className="text-center">{props.message}</h2>
        <h2 className="text-center">
          {props.winner.username ? props.winner.username : props.winner.name}
        </h2>
      </Col>
      <Col className="col-2">
        <ProfilePicture userId={props.winner?.id} />
      </Col>
    </Row>
  );
}
