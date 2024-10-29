import React from 'react';
import './RequestCardItemPersonal.module.css'; // Correct import path

function RequestCardItemPersonal({ request }) {
  return (
    <div className="card">
      <div className="card-details">
        <p className="text-title">{request.vehicle.maker} {request.vehicle.model}</p>
        <p className="text-body">{request.status}</p>
        <p className="text-body">{request.details}</p>
      </div>
      <button className="card-button">More info</button>
    </div>
  );
}

export default RequestCardItemPersonal;
