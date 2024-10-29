import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import ListingAPI from '../api/ListingAPI'; // Ensure this path is correct based on your project structure
import '../css/ListingPage.css'; // Importing for side effects
import UserItem from '../components/UserItem';
import VehicleItem from '../components/VehicleItem';

function ListingDetailPage() {
  const { listingId } = useParams(); // Ensure this gets the ID from the URL
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

  if (error) {
    return <div>Error: {error.message}</div>;
  }

  if (!listing) {
    return <div>Loading...</div>;
  }

  return (
    <div className="listing-detail">
      <h2>Listing Details</h2>
      <p>ID: {listing.id}</p>
      <p>Status: {listing.status}</p>
      {listing.buyerId && (
        <>
          <h3>Buyer Information</h3>
          <UserItem userItem={listing.buyerId} />
        </>
      )}
      <h3>Request Details</h3>
      <p>ID: {listing.request.id}</p>
      <p>Milage: {listing.request.milage}</p>
      <p>Details: {listing.request.details}</p>
      <p>Status: {listing.request.status}</p>
      <p>Start: {new Date(listing.request.start).toLocaleString()}</p>
      <p>End: {new Date(listing.request.end).toLocaleString()}</p>
      <p>Max Price: {listing.request.maxPrice}</p>
      <h3>Seller Information</h3>
      <UserItem userItem={listing.request.sellerId} />
      <h3>Vehicle Information</h3>
      <VehicleItem vehicleItem={listing.request.vehicle} />
    </div>
  );
}

export default ListingDetailPage;
