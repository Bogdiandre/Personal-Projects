import axios from 'axios';

const BASE_URL = "http://localhost:8090/images";

const ImageAPI = {
  uploadImage: async (imageFile, requestId) => {
    const formData = new FormData();
    formData.append('image', imageFile);
    formData.append('requestId', requestId); 

    const response = await axios.post(`${BASE_URL}/upload`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    });
    return response.data;
  },

  getImageByRequestId: async (requestId) => {
    const response = await axios.get(`${BASE_URL}/request/${requestId}`, {
      responseType: 'blob'
    });

    const url = URL.createObjectURL(response.data);
    return url;
  }
};

export default ImageAPI;
