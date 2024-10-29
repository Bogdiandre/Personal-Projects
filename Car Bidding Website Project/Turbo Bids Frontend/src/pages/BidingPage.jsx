import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import ListingAPI from '../api/ListingAPI';
import TokenManager from '../api/TokenManager';
import WebSocketClient from '../api/WebSocketClient';
import '../css/BidingPage.css';
import bigCarImg from '../images/mazda-6_2010_.png';
import smallCarImg from '../images/mazda-6-backImg.png';
import interiorCarImg from '../images/mazda-6-interior.png';

function BidingPage() {
    const { listingId } = useParams();
    const [listing, setListing] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchListing = async () => {
            try {
                const data = await ListingAPI.getListingById(listingId);
                setListing(data);
            } catch (err) {
                setError(err);
            }
        };

        fetchListing();
    }, [listingId]);

    useEffect(() => {
        const recipient = TokenManager.getUserId();
        if (recipient) {
            console.log('Attempting to connect to WebSocket for recipient:', recipient);
            WebSocketClient.connect(recipient, (newNotification) => {
                console.log('New Notification received:', newNotification);
            });
        }

        return () => {
            console.log('Disconnecting from WebSocket');
            WebSocketClient.disconnect();
        };
    }, []);

    if (error) {
        return <div>Error: {error.message}</div>;
    }

    if (!listing) {
        return <div>Loading...</div>;
    }

    return (
        <div className="biding-page">
            <ImagesSection />
            <div className="details-bidding-section">
                <VehicleDetailSide vehicle={listing.request.vehicle} request={listing.request} />
                <BiddingSide listingId={listingId} request={listing.request} />
            </div>
        </div>
    );
}

function ImagesSection() {
    return (
        <div className="images-section">
            <img src={bigCarImg} alt="Big Car" className="image" />
            <div className="small-images">
                <img src={smallCarImg} alt="Small Car" className="small-image" />
                <img src={interiorCarImg} alt="Interior Car" className="small-image" />
            </div>
        </div>
    );
}

function VehicleDetailSide({ vehicle, request }) {
    return (
        <div className="vehicle-detail-side">
            <h2>Vehicle Details</h2>
            <p>Maker: {vehicle.maker}</p>
            <p>Model: {vehicle.model}</p>
            <p>Type: {vehicle.type}</p>
            <p>Milage: {request.milage} km</p>
            <p>Details: {request.details}</p>
        </div>
    );
}

function BiddingSide({ listingId, request }) {
    const [timeLeft, setTimeLeft] = useState(calculateTimeLeft());
    const [highestBid, setHighestBid] = useState(null);
    const [bid, setBid] = useState('');
    const [bidError, setBidError] = useState('');
    const navigate = useNavigate();
    const isLoggedIn = TokenManager.getAccessToken() !== null;

    useEffect(() => {
        const timer = setInterval(() => {
            setTimeLeft(calculateTimeLeft());
        }, 1000);

        return () => clearInterval(timer);
    }, []);

    useEffect(() => {
        const fetchHighestBid = async () => {
            try {
                const response = await ListingAPI.getHighestBidForListing(listingId);
                setHighestBid(response);
            } catch (error) {
                console.error("Error fetching highest bid:", error);
            }
        };

        fetchHighestBid();
    }, [listingId]);

    function calculateTimeLeft() {
        const endTime = new Date(request.end);
        const now = new Date();
        const difference = endTime - now;

        let timeLeft = {};

        if (difference > 0) {
            timeLeft = {
                hours: Math.floor(difference / (1000 * 60 * 60)),
                minutes: Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60)),
                seconds: Math.floor((difference % (1000 * 60)) / 1000)
            };
        }

        return timeLeft;
    }

    const handleBidChange = (e) => {
        const value = e.target.value;
        if (!/^\d*\.?\d*$/.test(value) || value > 10000000) {
            setBidError('Bid must be a number between 0 and 10 million.');
        } else {
            setBidError('');
        }
        setBid(value);
    };

    const handlePlaceBid = async () => {
        if (!isLoggedIn) {
            navigate('/auth', {state: {from: window.location.pathname}});
            return;
        }

        if (!bid) {
            setBidError('Please enter a bid amount.');
            return;
        }

        if (!bidError && bid) {
            const bidObject = {
                accountId: TokenManager.getUserId(),
                amount: bid
            };
            try {
                await ListingAPI.addBid(listingId, bidObject);
                console.log('Bid placed successfully:', bidObject);
                alert('Bid placed successfully!');
                setBid(''); // Clear the bid input after a successful bid
            } catch (error) {
                console.error('Failed to place bid:', error);
                alert('Failed to place bid: ' + error.message);
            }
        }
    };

    const handleBuyOut = async () => {
        if (!isLoggedIn) {
            navigate('/auth');
            return;
        }

        try {
            await ListingAPI.buyOutListing(listingId, TokenManager.getUserId());
            alert('Listing bought out successfully!');
        } catch (error) {
            console.error('Failed to buy out listing:', error);
            alert('Failed to buy out listing: ' + error.message);
        }
    };

    return (
        <div className="bidding-side">
            <h2>Bidding</h2>
            <p>Time Left: {timeLeft.hours || '0'}h {timeLeft.minutes || '0'}m {timeLeft.seconds || '0'}s</p>
            {highestBid && (
                <p>
                    Highest Bid: ${highestBid.amount} ({highestBid.firstName} {highestBid.lastName})
                </p>
            )}
            <div className="bid-input-container">
                <div className="wave-group">
                    <input 
                        required 
                        type="text" 
                        className="input" 
                        value={bid}
                        onChange={handleBidChange}
                    />
                    <span className="bar"></span>
                    <label className="label">
                        <span className="label-char" style={{ '--index': 0 }}>B</span>
                        <span className="label-char" style={{ '--index': 1 }}>i</span>
                        <span className="label-char" style={{ '--index': 2 }}>d</span>
                    </label>
                </div>
                <button className="cssbuttons-io-button" onClick={handlePlaceBid}>
                    Place Bid
                    <div className="icon">
                        <svg height="24" width="24" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path d="M0 0h24v24H0z" fill="none"></path>
                            <path d="M16.172 11l-5.364-5.364 1.414-1.414L20 12l-7.778 7.778-1.414-1.414L16.172 13H4v-2z" fill="currentColor"></path>
                        </svg>
                    </div>
                </button>
            </div>
            {bidError && <p className="error">{bidError}</p>}
            <div className="flip-button">
                <button className="button button-front" onClick={handleBuyOut}>
                    <span className="text-front">Buy Out!</span>
                    <span className="text-back">${request.maxPrice}</span>
                </button>
            </div>
        </div>
    );
}

export default BidingPage;
