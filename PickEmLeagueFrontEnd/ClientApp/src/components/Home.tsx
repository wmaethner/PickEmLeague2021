import React, {  } from "react";
import { Container } from "reactstrap";
import { WeekSelector } from "./Week/WeekSelector";
import { Tabs, Tab } from "react-bootstrap";
import { WeekSummary } from "./Home/WeekSumary";
import { SeasonSummary } from "./Home/SeasonSummary";

export function Home() {

  return (
    <Container className="data-table">
      <WeekSelector />
      <Tabs defaultActiveKey="week">
        <Tab eventKey="week" title="Week">
          <WeekSummary />
        </Tab>
        <Tab eventKey="season" title="Season">
          <SeasonSummary />
        </Tab>
      </Tabs>
    </Container >
  );
}
