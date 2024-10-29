import axios from 'axios';

const API_URL = 'http://localhost:8090';

const getNotificationsForUser = async (recipient) => {
  const response = await axios.get(`${API_URL}/notifications/${recipient}`);
  return response.data;
};

export default {
  getNotificationsForUser,
};
