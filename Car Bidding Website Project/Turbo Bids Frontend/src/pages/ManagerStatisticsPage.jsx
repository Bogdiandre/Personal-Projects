import React, { useState, useEffect } from 'react';
import VehicleAPI from '../api/VehicleAPI';
import ListingAPI from '../api/ListingAPI';
import '../css/ManagerStatistics.css';

function ManagerStatisticsPage() {
  const [makers, setMakers] = useState([]);
  const [models, setModels] = useState([]);
  const [selectedMaker, setSelectedMaker] = useState('');
  const [selectedModel, setSelectedModel] = useState('');
  const [averagePrice, setAveragePrice] = useState('');

  useEffect(() => {
    const fetchMakers = async () => {
      try {
        const makersList = await VehicleAPI.getMakers();
        setMakers(makersList);
      } catch (error) {
        console.error("Error fetching makers:", error);
      }
    };

    fetchMakers();
  }, []);

  useEffect(() => {
    const fetchModels = async () => {
      if (selectedMaker) {
        try {
          const modelsList = await VehicleAPI.getModelsByMaker(selectedMaker);
          setModels(modelsList);
        } catch (error) {
          console.error("Error fetching models:", error);
        }
      }
    };

    fetchModels();
  }, [selectedMaker]);

  const handleGetAveragePrice = async () => {
    try {
      const priceData = await ListingAPI.getAveragePriceForVehicle(selectedMaker, selectedModel);
      setAveragePrice(priceData);
    } catch (error) {
      console.error("Error fetching average price:", error);
      alert('Failed to fetch average price: ' + error.message);
    }
  };

  return (
    <div className="manager-statistics-container">
      <h2>Manager Statistics</h2>
      <select
        value={selectedMaker}
        onChange={(e) => setSelectedMaker(e.target.value)}
        className="form-control mb-2"
      >
        <option value="">Select Maker</option>
        {makers.map((maker, index) => (
          <option key={index} value={maker}>{maker}</option>
        ))}
      </select>
      <select
        value={selectedModel}
        onChange={(e) => setSelectedModel(e.target.value)}
        className="form-control mb-2"
        disabled={!selectedMaker}
      >
        <option value="">Select Model</option>
        {models.map((model, index) => (
          <option key={index} value={model}>{model}</option>
        ))}
      </select>
      <button onClick={handleGetAveragePrice} className="btn btn-primary btn-full-width">
        AVERAGE PRICE
      </button>
      <input
        type="text"
        placeholder="Average Price"
        className="form-control mb-2"
        value={averagePrice}
        readOnly
      />
    </div>
  );
}

export default ManagerStatisticsPage;
