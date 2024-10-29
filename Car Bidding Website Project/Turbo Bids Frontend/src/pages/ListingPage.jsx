import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import ListingAPI from '../api/ListingAPI'; // Ensure this path is correct based on your project structure
import ListingCard from '../components/ListingCard';
import '../css/ListingPage.css'; // Importing for side effects

function ListingPage() {
    const [listings, setListings] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchListings = async () => {
            try {
                const data = await ListingAPI.getAllOpenListings();
                setListings(data);
            } catch (error) {
                setError(error);
            }
        };

        fetchListings();
    }, []);

    if (error) {
        return <div>Error: {error.message}</div>;
    }

    return (
        <div className="container-listings">
            <div className="listing-list">
                {listings.length === 0 ? (
                    <p>No listings found</p> 
                ) : (
                    listings.map((listing) => (
                        <ListingCard key={listing.id} listingItem={listing} />
                    ))
                )}
            </div>
        </div>
    );
}

export default ListingPage;
