import React from "react";
import { useContext } from "react";
import { Image } from "react-bootstrap";
import { Col, Container, Label, Row } from "reactstrap";
import { TeamContext } from "../../Data/Contexts/TeamsContext";
import Cardinals from "../../Assets/TeamLogos/Cardinals.jpg";

type Props = {
  id: number;
};

export const TeamDisplay: React.FC<Props> = ({ id }) => {
  const teamContext = useContext(TeamContext);

  function teamName(): string {
    return teamContext.teams[id - 1].name?.toString()!;
  }

  const imageSrc = () => {
    return process.env.PUBLIC_URL + "/TeamLogos/" + teamName().replace(/\s/g, "") + ".jpg";
  }

  return (
    <Container className="img-wrapper">
      <Row className="align-items-center">
        <Col className="col-8 align-middle"><span className="block align-middle">{teamName()}</span></Col>
        <Col className="col-4"><Image src={imageSrc()} width={50} height={50} /></Col>
      </Row>
    </Container>
  );
};
