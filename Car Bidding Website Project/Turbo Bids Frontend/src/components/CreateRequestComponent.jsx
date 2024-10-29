import React, { useState, useEffect } from 'react';
import RequestAPI from '../api/RequestAPI';
import VehicleAPI from '../api/VehicleAPI';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import TokenManager from '../api/TokenManager';
import ImageAPI from '../api/ImageAPI';
import './CreateRequestComponent.css';

function CreateRequestComponent({ onCancel }) {
  const [makers, setMakers] = useState([]);
  const [models, setModels] = useState([]);
  const [selectedMaker, setSelectedMaker] = useState('');
  const [selectedModel, setSelectedModel] = useState('');
  const [milage, setMilage] = useState('');
  const [details, setDetails] = useState('');
  const [maxPrice, setMaxPrice] = useState('');
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date(new Date().getTime() + 48 * 60 * 60 * 1000));
  const [image, setImage] = useState(null); // New state for image upload
  const userId = TokenManager.getClaims().userId; // Get the logged-in user's ID

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

  const handleCreateRequest = async () => {
    if (!selectedMaker || !selectedModel || !milage || !details || !maxPrice || !startDate || !endDate) {
      alert('Please fill out all fields.');
      return;
    }

    const requestData = {
      sellerId: userId, // Use the logged-in user's ID
      maker: selectedMaker,
      model: selectedModel,
      milage: parseInt(milage),
      details,
      start: startDate.toISOString(),
      end: endDate.toISOString(),
      maxPrice: parseInt(maxPrice)
    };

    try {
      const createdRequest = await RequestAPI.webPost(requestData); // Corrected API call
      alert('Request created successfully!');

      // Now upload the image if it exists
      if (image) {
        await ImageAPI.uploadImage(image, createdRequest.requestId);
        alert('Image uploaded successfully!');
      }

      // Optionally, you can also reset the form fields here
      setSelectedMaker('');
      setSelectedModel('');
      setMilage('');
      setDetails('');
      setMaxPrice('');
      setStartDate(new Date());
      setEndDate(new Date(new Date().getTime() + 48 * 60 * 60 * 1000));
      setImage(null);
    } catch (error) {
      alert('Failed to create request or upload image: ' + error.message);
    }
  };

  const handleStartDateChange = (date) => {
    setStartDate(date);
    if (endDate <= date) {
      setEndDate(new Date(date.getTime() + 48 * 60 * 60 * 1000));
    }
  };

  const handleFileChange = (event) => {
    setImage(event.target.files[0]);
  };

  return (
    <div className="create-request-container">
      <h2>Vehicle Details</h2>
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
      <input
        type="number"
        placeholder="Milage"
        className="form-control mb-2"
        value={milage}
        onChange={(e) => setMilage(e.target.value)}
        min="0"
        max="10000000"
      />
      <input
        type="number"
        placeholder="Maximum Price"
        className="form-control mb-2"
        value={maxPrice}
        onChange={(e) => setMaxPrice(e.target.value)}
        min="0"
        max="10000000"
      />
      <textarea
        placeholder="Details"
        className="form-control mb-2 details-input"
        value={details}
        onChange={(e) => setDetails(e.target.value)}
      />
      <div className="date-picker-container">
        <h3>Start Date</h3>
        <DatePicker
          selected={startDate}
          onChange={handleStartDateChange}
          showTimeSelect
          dateFormat="Pp"
          inline
          className="custom-date-picker"
          minDate={new Date()}
        />
        <h3>End Date</h3>
        <DatePicker
          selected={endDate}
          onChange={(date) => setEndDate(date)}
          showTimeSelect
          dateFormat="Pp"
          inline
          className="custom-date-picker"
          minDate={new Date(startDate.getTime() + 48 * 60 * 60 * 1000)}
        />
      </div>

      {/* Image Upload Section */}
      <div className={`image-upload-container ${image ? 'image-selected' : ''}`}>
        <label htmlFor="file" className="custom-file-upload">
          <div className="icon">
            <svg viewBox="0 0 24 24" fill="" xmlns="http://www.w3.org/2000/svg">
              <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
              <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
              <g id="SVGRepo_iconCarrier">
                <path
                  fillRule="evenodd"
                  clipRule="evenodd"
                  d="M10 1C9.73478 1 9.48043 1.10536 9.29289 1.29289L3.29289 7.29289C3.10536 7.48043 3 7.73478 3 8V20C3 21.6569 4.34315 23 6 23H7C7.55228 23 8 22.5523 8 22C8 21.4477 7.55228 21 7 21H6C5.44772 21 5 20.5523 5 20V9H10C10.5523 9 11 8.55228 11 8V3H18C18.5523 3 19 3.44772 19 4V9C19 9.55228 19.4477 10 20 10C20.5523 10 21 9.55228 21 9V4C21 2.34315 19.6569 1 18 1H10ZM9 7H6.41421L9 4.41421V7ZM14 15.5C14 14.1193 15.1193 13 16.5 13C17.8807 13 19 14.1193 19 15.5V16V17H20C21.1046 17 22 17.8954 22 19C22 20.1046 21.1046 21 20 21H13C11.8954 21 11 20.1046 11 19C11 17.8954 11.8954 17 13 17H14V16V15.5ZM16.5 11C14.142 11 12.2076 12.8136 12.0156 15.122C10.2825 15.5606 9 17.1305 9 19C9 21.2091 10.7909 23 13 23H20C22.2091 23 24 21.2091 24 19C24 17.1305 22.7175 15.5606 20.9844 15.122C20.7924 12.8136 18.858 11 16.5 11Z"
                  fill=""
                ></path>
              </g>
            </svg>
          </div>
          <div className="text">
            <span>{image ? 'Image Selected' : 'Click to upload image'}</span>
          </div>
          <input id="file" type="file" onChange={handleFileChange} />
        </label>
      </div>

      <div className="button-container">
        <button onClick={handleCreateRequest} className="create-button">
          Create
        </button>
        <button onClick={onCancel} className="cancel-button">
          Cancel
        </button>
      </div>
    </div>
  );
}

export default CreateRequestComponent;
