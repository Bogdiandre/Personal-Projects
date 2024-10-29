import axios from "axios";
import TokenManager from "./TokenManager";

const BASE_URL = "http://localhost:8090";

const VehicleAPI = {
    getVehicles: async () => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/vehicles`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data.vehicles;
        } catch (error) {
            console.error("Error fetching vehicles:", error);
            throw error;
        }
    },

    getMakers: async () => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/vehicles/makers`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching makers:", error);
            throw error;
        }
    },

    getTypes: async () => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/vehicles/vehicleTypes`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching vehicle types:", error);
            throw error;
        }
    },

    addVehicle: async (vehicle) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.post(`${BASE_URL}/vehicles`, {
                maker: vehicle.maker,
                model: vehicle.model,
                type: vehicle.type
            }, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error adding vehicle:", error);
            throw error;
        }
    },

    deleteVehicle: async (vehicleId) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.delete(`${BASE_URL}/vehicles/${vehicleId}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;  // Or simply return to indicate success
        } catch (error) {
            console.error(`Error deleting vehicle with ID ${vehicleId}:`, error);
            throw error;
        }
    },

    filterByMaker: async (maker) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/vehicles/filterByMaker`, {
                params: { maker },
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data.vehicles;
        } catch (error) {
            console.error("Error fetching vehicles:", error);
            throw error;
        }
    },

    getModelsByMaker: async (maker) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/vehicles/modelsByMaker`, {
                params: { maker },
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error(`Error fetching models for maker ${maker}:`, error);
            throw error;
        }
    }
};

export default VehicleAPI;
