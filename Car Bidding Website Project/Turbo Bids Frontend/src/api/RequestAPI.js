import axios from 'axios';
import TokenManager from './TokenManager';

const BASE_URL = "http://localhost:8090/requests";

const RequestAPI = {
  getRequests: async () => {
    const token = TokenManager.getAccessToken();
    const response = await axios.get(BASE_URL, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    return response.data;
  },
  getRequestById: async (requestId) => {
    const token = TokenManager.getAccessToken();
    const response = await axios.get(`${BASE_URL}/${requestId}`, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    return response.data;
  },
  deleteRequest: async (requestId) => {
    const token = TokenManager.getAccessToken();
    await axios.delete(`${BASE_URL}/${requestId}`, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
  },
  createRequest: async (request) => {
    const token = TokenManager.getAccessToken();
    const response = await axios.post(BASE_URL, request, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    return response.data;
  },
  updateRequest: async (requestId, request) => {
    const token = TokenManager.getAccessToken();
    await axios.put(`${BASE_URL}/${requestId}`, request, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
  },
  acceptRequest: async (requestId) => {
    const token = TokenManager.getAccessToken();
    const response = await axios.post(`${BASE_URL}/accept/${requestId}`, {}, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    return response.data;
  },
  declineRequest: async (requestId) => {
    const token = TokenManager.getAccessToken();
    await axios.put(`${BASE_URL}/decline/${requestId}`, {}, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
  },
  getPendingRequests: async () => {
    const token = TokenManager.getAccessToken();
    const response = await axios.get(`${BASE_URL}/status/pending`, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    return response.data.requests; // Ensure this is an array
  },
  getRequestsByUserId: async (userId) => {
    const token = TokenManager.getAccessToken();
    try {
      const response = await axios.get(`${BASE_URL}/user/${userId}`, {
        headers: {
          'Authorization': `Bearer ${token}`
        }
      });
      return response.data.requests;
    } catch (error) {
      console.error(`Error fetching requests for user with ID ${userId}:`, error.response.status, error.response.data);
      throw error;
    }
  },
  webPost: async (request) => {
    const token = TokenManager.getAccessToken();
    const response = await axios.post(`${BASE_URL}/webPost`, request, {
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });
    return response.data;
  }
};

export default RequestAPI;
