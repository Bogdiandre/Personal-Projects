import axios from "axios";
import TokenManager from "./TokenManager";

const BASE_URL = "http://localhost:8090";

const ListingAPI = {
    getAllListings: async () => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/listings`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data.listings;
        } catch (error) {
            console.error("Error fetching listings:", error);
            throw error;
        }
    },

    getListingById: async (listingId) => {
        try {
            const response = await axios.get(`${BASE_URL}/listings/${listingId}`);
            return response.data;
        } catch (error) {
            console.error(`Error fetching listing with ID ${listingId}:`, error);
            throw error;
        }
    },

    deleteListing: async (listingId) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.delete(`${BASE_URL}/listings/${listingId}`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data; // Or simply return to indicate success
        } catch (error) {
            console.error(`Error deleting listing with ID ${listingId}:`, error);
            throw error;
        }
    },

    buyOutListing: async (listingId, buyerId) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.post(`${BASE_URL}/listings/buyout/${listingId}/buyer/${buyerId}`, {}, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error(`Error buying out listing with ID ${listingId} for buyer ID ${buyerId}:`, error);
            throw error;
        }
    },

    getBidsForListing: async (listingId) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.get(`${BASE_URL}/listings/${listingId}/bids`, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data.bids;
        } catch (error) {
            console.error(`Error fetching bids for listing with ID ${listingId}:`, error);
            throw error;
        }
    },

    addBid: async (listingId, bid) => {
        const token = TokenManager.getAccessToken();
        try {
            const response = await axios.post(`${BASE_URL}/listings/${listingId}/addBid`, bid, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            return response.data;
        } catch (error) {
            console.error(`Error adding bid to listing with ID ${listingId}:`, error);
            throw error;
        }
    },

    getHighestBidForListing: async (listingId) => {
        try {
            const response = await axios.get(`${BASE_URL}/listings/${listingId}/highestBid`);
            return response.data;
        } catch (error) {
            console.error(`Error fetching highest bid for listing with ID ${listingId}:`, error);
            throw error;
        }
    },

    getAllOpenListings: async () => {
        try {
            const response = await axios.get(`${BASE_URL}/listings/open`);
            return response.data.listings;
        } catch (error) {
            console.error("Error fetching open listings:", error);
            throw error;
        }
    },

    getAveragePriceForVehicle: async (maker, model) => {
        const token = TokenManager.getAccessToken();
        try {
          const response = await axios.get(`${BASE_URL}/listings/averagePriceForVehicle`, {
            headers: {
              Authorization: `Bearer ${token}`
            },
            params: {
              maker: maker,
              model: model
            }
          });
          return response.data; // Ensure this is returning the correct data
        } catch (error) {
          console.error(`Error fetching average price for vehicle ${maker} ${model}:`, error);
          throw error;
        }
      }
};

export default ListingAPI;
