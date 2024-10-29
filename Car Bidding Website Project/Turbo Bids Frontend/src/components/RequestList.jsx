import React from "react";
import RequestItem from "./RequestItem";
import "../css/RequestPage.css"; // Importing for side effects

function RequestList(props) {
    return (
        <ul className="list">
            {props.requests.map((request) => (
                <RequestItem key={request.ID} requestItem={request} onRequestSelect={props.onRequestSelect} />
            ))}
        </ul>
    );
}

export default RequestList;
