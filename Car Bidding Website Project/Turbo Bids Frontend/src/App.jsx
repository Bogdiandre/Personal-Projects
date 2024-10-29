// src/App.jsx
import { Route, Routes, BrowserRouter as Router, Navigate } from "react-router-dom";
import { useState, useEffect } from 'react';
import VehiclePage from './pages/VehiclePage';
import UserPage from './pages/UsersPage';
import Header from './pages/Header';
import HomePage from './pages/HomePage';
import LoginPage from './pages/LoginPage';
import RequestPage from './pages/RequestPage';
import RequestDetailPage from './pages/RequestDetailPage';
import ListingPage from './pages/ListingPage';
import DatePickerTestPage from './components/DatePickerTestPage';
import RequestsEmployeePage from './pages/RequestsEmployeePage';
import BidingPage from './pages/BidingPage';
import MyActivitiesPage from './pages/MyActivitiesPage';
import EmployeeLoginPage from './pages/EmployeeLoginPage';
import ManagerStatisticsPage from './pages/ManagerStatisticsPage';
import ImageUploadPage from './pages/ImageUploadPage';
import TokenManager from './api/TokenManager';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

const PrivateRoute = ({ element: Element, isLoggedIn, role, allowedRoles, ...rest }) => {
  if (!isLoggedIn) {
    return <Navigate to="/auth" />;
  }
  if (allowedRoles && !allowedRoles.includes(role)) {
    return <Navigate to="/" />;
  }
  return <Element {...rest} />;
};

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [role, setRole] = useState('');

  useEffect(() => {
    const token = TokenManager.getAccessToken();
    if (token) {
      setIsLoggedIn(true);
      const claims = TokenManager.getClaims();
      if (claims && claims.role) {
        setRole(claims.role);
      }
    }
  }, []);

  const handleLogin = (userRole) => {
    setIsLoggedIn(true);
    setRole(userRole);
  };

  const handleLogout = () => {
    setIsLoggedIn(false);
    setRole('');
    TokenManager.clear();
  };

  return (
    <div className="App">
      <Router>
        <Header isLoggedIn={isLoggedIn} role={role} onLogout={handleLogout} />
        <Routes>
          <Route path="/" element={role === 'MANAGER' ? <Navigate to="/statistics" /> : <HomePage />} />
          <Route path="/listings" element={<ListingPage />} />
          <Route path="/about" element={<HomePage />} />
          <Route path="/auth" element={<LoginPage onLogin={handleLogin} />} />
          <Route path="/vehicles" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['EMPLOYEE', 'MANAGER']} element={VehiclePage} />} />
          <Route path="/users" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['MANAGER']} element={UserPage} />} />
          <Route path="/requests" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['EMPLOYEE']} element={RequestPage} />} />
          <Route path="/date-picker-test" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['EMPLOYEE']} element={DatePickerTestPage} />} />
          <Route path="/requests/:requestId" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['EMPLOYEE']} element={RequestDetailPage} />} />
          <Route path="/employeeRequests" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['EMPLOYEE']} element={RequestsEmployeePage} />} />
          {/* Removed PrivateRoute for BidingPage */}
          <Route path="/listings/:listingId" element={<BidingPage />} />
          <Route path="/myactivities" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['CLIENT']} element={MyActivitiesPage} />} />
          <Route path="/impossibletofindworkerlogin" element={<EmployeeLoginPage onLogin={() => handleLogin('EMPLOYEE')} />} />
          <Route path="/statistics" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['MANAGER']} element={ManagerStatisticsPage} />} />
          <Route path="/upload-image" element={<PrivateRoute isLoggedIn={isLoggedIn} role={role} allowedRoles={['CLIENT']} element={ImageUploadPage} />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;
