import React from 'react';
import VehicleItem from './VehicleItem';
import styles from './VehicleItem.module.css';

function VehicleList({ vehicleItems, onVehicleSelect, selectedVehicleId }) {
  if (!vehicleItems) {
    // Handles the scenario where vehicleItems are not yet loaded or undefined
    return <div>Loading vehicles...</div>;
  }

  return (
    <ul className={styles.vehicleList}>
      {vehicleItems.map(vehicle => (
        <li 
          key={vehicle.id} 
          onClick={() => onVehicleSelect(vehicle.id)}
          style={{
            backgroundColor: vehicle.id === selectedVehicleId ? '#eaeaea' : 'transparent',
            cursor: 'pointer'
          }}
        >
          <VehicleItem vehicleItem={vehicle} />
        </li>
      ))}
    </ul>
  );
}

export default VehicleList;
