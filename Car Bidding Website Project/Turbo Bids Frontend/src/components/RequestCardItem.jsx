import React from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import styles from './RequestCardItem.module.css';

function RequestCardItem({ request }) {


  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/requests/${request.id}`); 
  };

  return (
    <div className={styles.card} onClick={handleClick}>
      <div className={styles['card-details']}>
        <p className={styles['text-title']}>{request.vehicle.maker} {request.vehicle.model}</p>
        <p className={styles['text-body']}>{request.details}</p>
      </div>
      <button className={styles['card-button']} onClick={handleClick}>More info</button>
    </div>
  );
}

export default RequestCardItem;
