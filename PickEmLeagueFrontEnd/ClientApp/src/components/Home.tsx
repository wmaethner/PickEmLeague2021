import React from "react";
import { Col, Container, Row } from "reactstrap";
import { Tab, Nav } from "react-bootstrap";
import { WeekSummaryPage } from "./Home/WeekSumary";
import { SeasonSummaryPage } from "./Home/SeasonSummary";
import { ProfilePicture } from "./Images/ProfilePicture";
import { User } from "../Apis";
import ReactTooltip from "react-tooltip";
import { WhoPickedWho } from "./Home/WhoPickedWho";

export function userDisplay(user: User, index: number) {
  return (
    <div className="row align-items-center">
      <div className="col">{usersNameDisplay(user, index)}</div>
      <div className="col">
        <ProfilePicture userId={user.id} />
      </div>
    </div>
  );
}

export function usersNameDisplay(user: User, index: number) {
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

export function Home() {
  return (
    <Container className="data-table">
      <Tab.Container id="left-tabs-example" defaultActiveKey="week">
        <Row>
          <Col sm={2}>
            <Nav variant="pills" className="flex-column">
              <Nav.Item>
                <Nav.Link eventKey="week">Week Summary</Nav.Link>
              </Nav.Item>
              <Nav.Item>
                <Nav.Link eventKey="season">Season Summary</Nav.Link>
              </Nav.Item>
              <Nav.Item>
                <Nav.Link eventKey="picks">Who Picked Who</Nav.Link>
              </Nav.Item>
            </Nav>
          </Col>
          <Col sm={10}>
            <Tab.Content>
              <Tab.Pane eventKey="week">
                <WeekSummaryPage />
              </Tab.Pane>
              <Tab.Pane eventKey="season">
                <SeasonSummaryPage />
              </Tab.Pane>
              <Tab.Pane eventKey="picks">
                <WhoPickedWho />
              </Tab.Pane>
            </Tab.Content>
          </Col>
        </Row>
      </Tab.Container>
    </Container>
  );
}
