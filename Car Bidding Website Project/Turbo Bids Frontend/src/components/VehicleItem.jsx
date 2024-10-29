import React from "react";
import styles from "./VehicleItem.module.css";

function VehicleItem(props) {
    const { vehicleItem } = props;

    return (
        <li className={styles.item}>
            <span>Maker: {vehicleItem.maker}</span>
            <span>Model: {vehicleItem.model}</span>
            <span>Type: {vehicleItem.type}</span> {/* Add this line to show type */}
        </li>
    );
}

export default VehicleItem;
