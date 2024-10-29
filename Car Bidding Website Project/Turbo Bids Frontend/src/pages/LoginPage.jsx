import React from 'react';
import FlipCardLogin from '../components/FlipCardLogin';
import AuthAPI from '../api/AuthAPI';
import TokenManager from '../api/TokenManager';
import { useNavigate, useLocation } from 'react-router-dom';
import '../css/Login.css';

const LoginPage = ({ onLogin }) => {
  const navigate = useNavigate();
  const location = useLocation();
  const handleLogin = async (email, password) => {
    try {
      const token = await AuthAPI.login(email, password);
      const claims = TokenManager.setAccessToken(token);
      onLogin(claims.role);
      if(location.state && location.state.from){
        navigate(location.state.from)
      }
      else{
        navigate('/');
      }
      
    } catch (err) {
      alert("Login failed!");
    }
  };

  const handleSignup = async (firstName, lastName, email, password) => {
    try {
      const claims = await AuthAPI.signup(firstName, lastName, email, password);
      TokenManager.setClaims(claims);
      onLogin(claims.role);
      navigate('/');
    } catch (err) {
      alert("Sign-up failed!");
    }
  };

  return (
    <div className="login-page">
      <FlipCardLogin onLogin={handleLogin} onSignup={handleSignup} />
    </div>
  );
};

export default LoginPage;
