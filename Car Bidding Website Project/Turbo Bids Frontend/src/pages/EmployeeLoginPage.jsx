import React from 'react';
import EmployeeLoginCard from '../components/EmployeeLoginCard';
import AuthAPI from '../api/AuthAPI';
import TokenManager from '../api/TokenManager';
import { useNavigate } from 'react-router-dom';
import '../css/EmployeeLoginPage.css';

const EmployeeLoginPage = ({ onLogin }) => {
  const navigate = useNavigate();

  const handleLogin = async (email, password) => {
    try {
      const token = await AuthAPI.employeeLogin(email, password);
      const claims = TokenManager.setAccessToken(token);
      onLogin(claims.role);
      navigate('/');
    } catch (err) {
      alert("Login failed!");
    }
  };

  return (
    <div className="employee-login-page">
      <EmployeeLoginCard onLogin={handleLogin} />
    </div>
  );
};

export default EmployeeLoginPage;
