import React, { useEffect, useState } from 'react';
import RequestList from '../components/RequestList';
import RequestAPI from '../api/RequestAPI';
import '../css/RequestPage.css'; // Importing for side effects
import { useNavigate } from 'react-router-dom';

function RequestPage() {
  const [requests, setRequests] = useState([]);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchRequests = async () => {
      try {
        const data = await RequestAPI.getRequests();
        setRequests(data);
      } catch (err) {
        setError(err);
      }
    };

    fetchRequests();
  }, []);

  const handleRequestSelect = (id) => {
    navigate(`/requests/${id}`);
  };

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  return (
    <div className="container-requests">
      <div className="request-list">
        <RequestList requests={requests} onRequestSelect={handleRequestSelect} />
      </div>
    </div>
  );
}

export default RequestPage;
