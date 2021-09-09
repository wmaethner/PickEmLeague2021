import React from "react";
import ReactDatePicker from "react-datepicker";

export function DateTimePicker(props) {
  return (
    <ReactDatePicker
      selected={props.date}
      selectsRange={false}
      showMonthDropdown
      showYearDropdown
      showTimeSelect
      timeFormat="HH:mm"
      timeIntervals={5}
      timeCaption="time"
      dateFormat="MMMM d, yyyy h:mm aa"
      onChange={(date) => props.handleDateChange(date)}
    ></ReactDatePicker>
  );
}
