import React from "react";
import UserItem from "./UserItem";
import VehicleItem from "./VehicleItem";
import RequestItem from "./RequestItem"; // Import the RequestItem component
import styles from "./ListingItem.module.css";

function ListingItem(props) {
    const { listingItem } = props;

    const handleClick = () => {
        props.onListingSelect(listingItem.id);
    };

    return (
        <li className={styles.item} onClick={handleClick}>
            <h3>Listing Details</h3>
            <span>ID: {listingItem.id}</span>
            <span>Status: {listingItem.status}</span>
            {listingItem.buyerId && (
                <>
                    <h3>Buyer Information</h3>
                    <UserItem userItem={listingItem.buyerId} />
                </>
            )}
            <h3>Request Details</h3>
            <RequestItem requestItem={listingItem.request} />
        </li>
    );
}

export default ListingItem;

