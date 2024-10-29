import React, { useEffect, useState } from 'react';
import VehicleList from '../components/VehicleList';
import VehicleAPI from "../api/VehicleAPI";
import '../css/VehiclePage.css'; // Ensure CSS file is correctly imported

function VehiclePage() {
  const [vehicleItems, setVehicleItems] = useState([]);
  const [selectedVehicleId, setSelectedVehicleId] = useState(null);
  const [model, setModel] = useState('');
  const [makers, setMakers] = useState([]);
  const [types, setTypes] = useState([]);
  const [selectedMaker, setSelectedMaker] = useState('');
  const [selectedType, setSelectedType] = useState('');
  const [filterMaker, setFilterMaker] = useState('All Makers');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [makerList, typeList] = await Promise.all([
          VehicleAPI.getMakers(),
          VehicleAPI.getTypes()
        ]);
        setMakers(["All Makers", ...makerList]);
        setTypes(["Select Type", ...typeList]);
        setSelectedMaker("Select Maker");
        setSelectedType("Select Type");
        setFilterMaker("All Makers");
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
    refreshVehicleList(); // Fetch vehicles as well when component mounts
  }, []);

  const refreshVehicleList = async (maker = '') => {
    try {
        let vehicles;
        if (maker && maker !== 'All Makers') {
            vehicles = await VehicleAPI.filterByMaker(maker);
        } else {
            const response = await VehicleAPI.getVehicles();
            vehicles = response;
        }
        setVehicleItems(vehicles);
    } catch (error) {
        console.error("Error loading vehicles:", error);
    }
};

const handleAddVehicle = async () => {
  if (selectedMaker !== "Select Maker" && model && selectedType !== "Select Type") {
      try {
          const response = await VehicleAPI.addVehicle({ maker: selectedMaker, model, type: selectedType });
          setModel('');
          setSelectedMaker("Select Maker");
          setSelectedType("Select Type");
          await refreshVehicleList(filterMaker); // Refresh list with current filter
          alert('Vehicle added successfully!');
      } catch (error) {
          alert('Failed to add vehicle: ' + error.message);
      }
  } else {
      alert('Please select a maker, type, and fill out the model field.');
  }
};

  const handleVehicleSelect = (id) => {
    setSelectedVehicleId(id);
  };

  const handleDeleteVehicle = async () => {
    if (selectedVehicleId) {
      try {
        await VehicleAPI.deleteVehicle(selectedVehicleId);
        refreshVehicleList();
        setSelectedVehicleId(null); // Clear the selection after delete
        alert('Vehicle deleted successfully!');
      } catch (error) {
        alert('Failed to delete vehicle: ' + error.message);
      }
    } else {
      alert('Please select a vehicle to delete.');
    }
  };

  const handleFilterChange = async () => {
    await refreshVehicleList(filterMaker);
};

  return (
    <div className="container-vehicles">
      <div className="left-column">
        <VehicleList
          vehicleItems={vehicleItems}
          onVehicleSelect={handleVehicleSelect}
          selectedVehicleId={selectedVehicleId}
        />
        <select
          className="form-control mb-2"
          value={filterMaker}
          onChange={(e) => setFilterMaker(e.target.value)}
        >
          {makers.map((maker, index) => (
            <option key={index} value={maker}>{maker}</option>
          ))}
        </select>
        <button onClick={handleFilterChange} className="btn btn-primary btn-full-width">
          Filter
        </button>
      </div>
      <div className="right-column">
        <select
          className="form-control mb-2"
          value={selectedMaker}
          onChange={(e) => setSelectedMaker(e.target.value)}
        >
          {makers.map((maker, index) => (
            <option key={index} value={maker}>{maker}</option>
          ))}
        </select>
        <select
          className="form-control mb-2"
          value={selectedType}
          onChange={(e) => setSelectedType(e.target.value)}
        >
          {types.map((type, index) => (
            <option key={index} value={type}>{type}</option>
          ))}
        </select>
        <input
          type="text"
          placeholder="Model"
          className="form-control mb-2"
          value={model}
          onChange={(e) => setModel(e.target.value)}
        />
        <button onClick={handleAddVehicle} className="btn btn-primary btn-full-width">
          Add Vehicle
        </button>
        <button onClick={handleDeleteVehicle} className="btn btn-danger btn-full-width" style={{ marginTop: '1rem' }}>
          Delete Selected Vehicle
        </button>
      </div>
    </div>
  );
}

export default VehiclePage;
