import React from "react";
import UserItem from "./UserItem";
import VehicleItem from "./VehicleItem";
import styles from "./RequestItem.module.css";

function RequestItem(props) {
    const { requestItem } = props;

    const handleClick = () => {
        props.onRequestSelect(requestItem.ID);
    };

    return (
        <li className={styles.item} onClick={handleClick}>
            <h3>Request Details</h3>
            <span>ID: {requestItem.id}</span>
            <span>Milage: {requestItem.milage}</span>
            <span>Details: {requestItem.details}</span>
            <span>Status: {requestItem.status}</span>
            <span>Start: {new Date(requestItem.start).toLocaleString()}</span>
            <span>End: {new Date(requestItem.end).toLocaleString()}</span>
            <span>Max Price: {requestItem.maxPrice}</span>
            <h3>Seller Information</h3>
            <UserItem userItem={requestItem.sellerId} />
            <h3>Vehicle Information</h3>
            <VehicleItem vehicleItem={requestItem.vehicle} />
        </li>
    );
}

export default RequestItem;

