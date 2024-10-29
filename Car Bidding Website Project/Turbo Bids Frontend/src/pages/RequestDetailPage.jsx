import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import RequestAPI from '../api/RequestAPI';
import '../css/RequestPage.css'; // Importing for side effects

function RequestDetailPage() {
  const { requestId } = useParams(); // Ensure this gets the ID from the URL
  const [request, setRequest] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchRequest = async () => {
      try {
        const data = await RequestAPI.getRequestById(requestId);
        setRequest(data);
      } catch (err) {
        setError(err);
      }
    };

    fetchRequest();
  }, [requestId]);

  const handleAccept = async () => {
    try {
      await RequestAPI.acceptRequest(requestId);
      alert('Request accepted successfully!');
      navigate('/employeeRequests');
    } catch (error) {
      alert('Failed to accept request: ' + error.message);
    }
  };

  const handleDecline = async () => {
    try {
      await RequestAPI.declineRequest(requestId);
      alert('Request declined successfully!');
      navigate('/employeeRequests');
    } catch (error) {
      alert('Failed to decline request: ' + error.message);
    }
  };

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  if (!request) {
    return <div>Loading...</div>;
  }

  return (
    <div className="request-detail">
      <h2>Request Details</h2>
      <p>ID: {request.ID}</p>
      <p>Milage: {request.milage}</p>
      <p>Details: {request.details}</p>
      <p>Status: {request.status}</p>
      <p>Start: {new Date(request.start).toLocaleString()}</p>
      <p>End: {new Date(request.end).toLocaleString()}</p>
      <p>Max Price: {request.maxPrice}</p>
      <h3>Seller Information</h3>
      <p>Email: {request.sellerId.email}</p>
      <p>Last Name: {request.sellerId.lastName}</p>
      <p>First Name: {request.sellerId.firstName}</p>
      <h3>Vehicle Information</h3>
      <p>Maker: {request.vehicle.maker}</p>
      <p>Model: {request.vehicle.model}</p>
      <p>Type: {request.vehicle.type}</p>
      <button onClick={handleAccept} className="btn btn-primary">
        Accept
      </button>
      <button onClick={handleDecline} className="btn btn-danger" style={{ marginLeft: '1rem' }}>
        Decline
      </button>
    </div>
  );
}

export default RequestDetailPage;
