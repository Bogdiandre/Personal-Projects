import {jwtDecode} from "jwt-decode";

const TokenManager = {
  getAccessToken: () => sessionStorage.getItem("accessToken"),
  getClaims: () => {
    const token = sessionStorage.getItem("accessToken");
    if (!token) return undefined;
    return jwtDecode(token);
  },
  setAccessToken: (token) => {
    sessionStorage.setItem("accessToken", token);
    const claims = jwtDecode(token);
    sessionStorage.setItem("claims", JSON.stringify(claims));
    return claims;
  },
  clear: () => {
    sessionStorage.removeItem("accessToken");
    sessionStorage.removeItem("claims");
  },
  getUserId: () => {
    const claims = JSON.parse(sessionStorage.getItem("claims"));
    return claims ? claims.userId : null;
  }
};

export default TokenManager;
