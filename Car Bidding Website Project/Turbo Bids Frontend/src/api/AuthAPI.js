import axios from "axios";
import TokenManager from "./TokenManager";

const BASE_URL = "http://localhost:8090";

const AuthAPI = {
  login: async (email, password) => {
    try {
      const response = await axios.post(`${BASE_URL}/auth/login`, { email, password });
      const accessToken = response.data.accessToken;
      TokenManager.setAccessToken(accessToken);
      return accessToken;
    } catch (error) {
      console.error("Error logging in:", error);
      throw error;
    }
  },
  employeeLogin: async (email, password) => {
    try {
      const response = await axios.post(`${BASE_URL}/auth/login/worker-login`, { email, password });
      const accessToken = response.data.accessToken;
      TokenManager.setAccessToken(accessToken);
      return accessToken;
    } catch (error) {
      console.error("Error logging in:", error);
      throw error;
    }
  },
  signup: async (firstName, lastName, email, password) => {
    console.log('AuthAPI - First Name:', firstName);  // Add logging here
    console.log('AuthAPI - Last Name:', lastName);    // Add logging here
    console.log('AuthAPI - Email:', email);           // Add logging here
    console.log('AuthAPI - Password:', password);     // Add logging here
    const response = await axios.post('http://localhost:8080/auth/register/client', {
      firstName,
      lastName,
      email,
      password
    });
    return response.data;
  }
};

export default AuthAPI;
