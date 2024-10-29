import React, { useEffect, useState } from 'react';
import RequestCardItemPersonal from './RequestCardItemPersonal';
import RequestAPI from '../api/RequestAPI';
import './RequestCardPersonalList.css'; // Correct import path

function RequestCardPersonalList({ accountId }) {
  const [requests, setRequests] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchRequests = async () => {
      try {
        const data = await RequestAPI.getRequestsByUserId(accountId);
        setRequests(data);
      } catch (err) {
        setError(err);
      }
    };

    fetchRequests();
  }, [accountId]);

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <div className="request-list-container">
      <div className="request-list">
        {requests.length === 0 ? (
          <p>No Requests</p> 
        ) : (
          requests.map(request => (
            <RequestCardItemPersonal key={request.id} request={request} />
          ))
        )}
      </div>
    </div>
  );
}

export default RequestCardPersonalList;
