import React, { useEffect, useState } from 'react';
import RequestCardItem from '../components/RequestCardItem';
import RequestAPI from '../api/RequestAPI';
import '../css/RequestsEmployeePage.css'; 

function RequestsEmployeePage() {
  const [requests, setRequests] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchRequests = async () => {
      try {
        const data = await RequestAPI.getPendingRequests();
        setRequests(data);
      } catch (err) {
        setError(err);
      }
    };

    fetchRequests();
  }, []);

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <div className="container-requests">
      <div className="request-list">
        {requests.length === 0 ? (
          <p>No Requests</p> 
        ) : (
          requests.map(request => (
            <RequestCardItem key={request.ID} request={request} />
          ))
        )}
      </div>
    </div>
  );
}

export default RequestsEmployeePage;
