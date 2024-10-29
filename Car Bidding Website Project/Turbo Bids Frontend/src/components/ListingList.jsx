import React from "react";
import ListingItem from "./ListingItem";
import "../css/ListingPage.css"; // Importing for side effects

function ListingList(props) {
    return (
        <ul className="list listing-list">
            {props.listings.map((listing) => (
                <ListingItem key={listing.id} listingItem={listing} onListingSelect={props.onListingSelect} />
            ))}
        </ul>
    );
}

export default ListingList;
