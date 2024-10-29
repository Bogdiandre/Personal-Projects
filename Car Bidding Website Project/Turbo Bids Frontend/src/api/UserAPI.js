import axios from "axios";
import TokenManager from "./TokenManager";

const BASE_URL = "http://localhost:8090";

const UserAPI = {
    getUsers: async () => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/users`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data.users;
        } catch (error) {
            console.error("Error fetching users:", error);
            throw error;
        }
    },

    addUser: async (user) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.post(`${BASE_URL}/users`, {
                lastName: user.lastName,
                firstName: user.firstName,
                email: user.email,
                password: user.password,
                role: user.role
            }, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error adding user:", error);
            throw error;
        }
    },

    getRoles: async () => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/users/getAllRoles`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error fetching roles:", error);
            throw error;
        }
    },

    deleteUser: async (userId) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.delete(`${BASE_URL}/users/${userId}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;  // Or simply return to indicate success
        } catch (error) {
            console.error(`Error deleting user with ID ${userId}:`, error);
            throw error;
        }
    },

    registerClient: async (client) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.post(`${BASE_URL}/auth/register/client`, {
                lastName: client.lastName,
                firstName: client.firstName,
                email: client.email,
                password: client.password
            }, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error("Error registering client:", error);
            throw error;
        }
    }
};

export default UserAPI;
