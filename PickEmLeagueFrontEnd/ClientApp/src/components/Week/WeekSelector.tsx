import React from "react";
import { useContext } from "react";
import { WeekContext } from "../../Data/Contexts/WeekContext";
import { Badge, Button, Col, Label, Row } from "reactstrap";
import "../../../node_modules/bootstrap/dist/css/bootstrap.min.css";

export function WeekSelector() {
  const { week, setWeek } = useContext(WeekContext);

  const handleWeekChange = (change: number) => {
    let newWeek = week! + change;
    if (newWeek === 0) {
      newWeek = 18;
    }
    if (newWeek === 19) {
      newWeek = 1;
    }
    setWeek(newWeek);
  };

  return (
    <Row className="flex-nowrap justify-content-center">
      <Col className="col-2">
        <div className="d-grid gap-2">
          <Button
            color="primary"
            className="btn-block"
            onClick={() => handleWeekChange(-1)}
          >
            Previous
          </Button>
        </div>
      </Col>
      <Col className="col-4">
        <h2 className="text-center">Week {week}</h2>
      </Col>
      <Col className="col-2">
        <div className="d-grid gap-2">
          <Button
            color="primary"
            className="btn-block"
            onClick={() => handleWeekChange(1)}
          >
            Next
          </Button>
        </div>
      </Col>
    </Row>
  );
}
