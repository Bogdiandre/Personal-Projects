import React, { useState, useEffect } from "react";
import { Link } from 'react-router-dom';
import ListingAPI from '../api/ListingAPI';
import ImageAPI from '../api/ImageAPI';
import "./ListingCard.css"; // Importing the provided CSS

function ListingCard({ listingItem }) {
    const [timeLeft, setTimeLeft] = useState(calculateTimeLeft());
    const [highestBid, setHighestBid] = useState(null);
    const [imageUrl, setImageUrl] = useState(null);

    useEffect(() => {
        const timer = setInterval(() => {
            setTimeLeft(calculateTimeLeft());
        }, 1000);

        return () => clearInterval(timer);
    }, []);

    useEffect(() => {
        const fetchHighestBid = async () => {
            try {
                const response = await ListingAPI.getHighestBidForListing(listingItem.id);
                setHighestBid(response.amount);
            } catch (error) {
                console.error("Error fetching highest bid:", error);
            }
        };

        fetchHighestBid();
    }, [listingItem.id]);

    useEffect(() => {
        const fetchImage = async () => {
            try {
                const url = await ImageAPI.getImageByRequestId(listingItem.request.id);
                setImageUrl(url);
            } catch (error) {
                console.error('Failed to fetch image:', error);
            }
        };

        fetchImage();
    }, [listingItem.request.id]);

    function calculateTimeLeft() {
        const endTime = new Date(listingItem.request.end);
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

    return (
        <div className="listing-card">
            <Link to={`/listings/${listingItem.id}`} className="listing-card-link">
                <div className="card">
                    {imageUrl && <img src={imageUrl} alt="Car image" className="card__image" />}
                    <div className="card__content">
                        <p className="card__title">{listingItem.request.vehicle.maker} {listingItem.request.vehicle.model}</p>
                        <p className="card__description">
                            Highest Bid: ${highestBid || '0'}
                        </p>
                        <p className="card__description">
                            Time Left: {timeLeft.hours || '0'}h {timeLeft.minutes || '0'}m {timeLeft.seconds || '0'}s
                        </p>
                    </div>
                </div>
            </Link>
        </div>
    );
}

export default ListingCard;
