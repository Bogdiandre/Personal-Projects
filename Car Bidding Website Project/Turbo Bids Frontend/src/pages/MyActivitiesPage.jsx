import React, { useEffect, useState } from 'react';
import RequestCardItemPersonal from '../components/RequestCardItemPersonal';
import RequestAPI from '../api/RequestAPI';
import CreateRequestComponent from '../components/CreateRequestComponent';
import TokenManager from '../api/TokenManager';
import '../css/MyActivitiesPage.css'; // Ensure CSS file is correctly imported

function MyActivitiesPage() {
    const [showCreateRequest, setShowCreateRequest] = useState(false);
    const [requests, setRequests] = useState([]);
    const [error, setError] = useState(null);
    const userId = TokenManager.getUserId();

    useEffect(() => {
        const fetchRequests = async () => {
            if (userId) {
                try {
                    const data = await RequestAPI.getRequestsByUserId(userId);
                    setRequests(data);
                } catch (err) {
                    console.error(`Error fetching requests: ${err.response?.status} ${err.response?.data}`);
                    setError(err);
                }
            }
        };

        fetchRequests();
    }, [userId]);

    const handleAddRequestClick = () => {
        setShowCreateRequest(true);
        setTimeout(() => {
            document.getElementById('create-request').scrollIntoView({ behavior: 'smooth' });
        }, 100); // Slight delay to ensure visibility
    };

    const handleCancelClick = () => {
        setShowCreateRequest(false);
    };

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
                        <RequestCardItemPersonal key={request.id} request={request} />
                    ))
                )}
            </div>
            {showCreateRequest ? (
                <div id="create-request">
                    <CreateRequestComponent onCancel={handleCancelClick} />
                </div>
            ) : (
                <button onClick={handleAddRequestClick} className="btn btn-primary">
                    Add Request
                </button>
            )}
        </div>
    );
}

export default MyActivitiesPage;
