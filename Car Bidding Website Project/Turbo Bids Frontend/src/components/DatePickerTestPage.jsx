import React, { useState } from 'react';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import '../css/DatePickerTestPage.css';

function DatePickerTestPage() {
  const [startDate, setStartDate] = useState(new Date());

  return (
    <div className="date-picker-test-page">
      <h2>Select a Date</h2>
      <DatePicker
        selected={startDate}
        onChange={(date) => setStartDate(date)}
        showTimeSelect
        dateFormat="Pp"
        inline
        className="custom-date-picker"
      />
    </div>
  );
}

export default DatePickerTestPage;
