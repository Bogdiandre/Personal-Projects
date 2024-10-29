import React from "react";
import styles from "./UserItem.module.css";

function UserItem(props) {
    return (
        <li className={styles.item}>
            
            <span> Email: {props.userItem.email}</span>
            <span> LastName: {props.userItem.lastName}</span>
            <span> FirstName: {props.userItem.firstName}</span>
            <span> Role: {props.userItem.role}</span>
             {/* Add this line to show type */}
        </li>
    );
}

export default UserItem;