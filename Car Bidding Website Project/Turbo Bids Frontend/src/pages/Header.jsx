import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import '../css/Header.css';
import logoImg from '../images/TurboBids name logo.png';
import TokenManager from '../api/TokenManager';
import NotificationPopup from '../components/NotificationPopup';

const Header = ({ isLoggedIn, role, onLogout }) => {
  const navigate = useNavigate();

  const handleLogout = () => {
    TokenManager.clear();
    onLogout();
    navigate('/auth');
  };

  // Extract firstName and lastName from token claims
  const claims = TokenManager.getClaims();
  const firstName = claims ? claims.firstName : '';
  const lastName = claims ? claims.lastName : '';
  const recipient = `${firstName} ${lastName}`;

  return (
    <header className="header">
      <div className="left-section">
        <div className="logo-text">
          <img src={logoImg} alt="Turbo Bids Logo" />
        </div>
        <nav className="navbar">
          {isLoggedIn ? (
            role === 'EMPLOYEE' ? (
              <>
                <Link to="/" className="btn">Home</Link>
                <Link to="/employeeRequests" className="btn">Requests</Link>
                <Link to="/vehicles" className="btn">Vehicles</Link>
              </>
            ) : role === 'CLIENT' ? (
              <>
                <Link to="/" className="btn">Home</Link>
                <Link to="/listings" className="btn">Listings</Link>
                <Link to="/myactivities" className="btn">My Activities</Link>
                
              </>
            ) : role === 'MANAGER' ? (
              <>
                <Link to="/statistics" className="btn">Statistics</Link>
                <Link to="/users" className="btn">Users</Link>
              </>
            ) : null
          ) : (
            <>
              <Link to="/" className="btn">Home</Link>
              <Link to="/listings" className="btn">Listings</Link>
              <Link to="/about" className="btn">About</Link>
              <Link to="/auth" className="btn">Login</Link>
            </>
          )}
        </nav>
      </div>
      <div className="right-section">
        {isLoggedIn && role === 'CLIENT' && <NotificationPopup recipient={recipient} />}
        {isLoggedIn && (
          <button className="LogoutBtn" onClick={handleLogout}>
            <div className="logout-sign">
              <svg viewBox="0 0 512 512">
                <path d="M377.9 105.9L500.7 228.7c7.2 7.2 11.3 17.1 11.3 27.3s-4.1 20.1-11.3 27.3L377.9 406.1c-6.4 6.4-15 9.9-24 9.9c-18.7 0-33.9-15.2-33.9-33.9l0-62.1-128 0c-17.7 0-32-14.3-32-32l0-64c0-17.7 14.3-32 32-32l128 0 0-62.1c0-18.7 15.2-33.9 33.9-33.9c9 0 17.6 3.6 24 9.9zM160 96L96 96c-17.7 0-32 14.3-32 32l0 256c0 17.7 14.3 32 32 32l64 0c17.7 0 32 14.3 32 32s-14.3 32-32 32l-64 0c-53 0-96-43-96-96L0 128C0 75 43 32 96 32l64 0c17.7 0 32 14.3 32 32s-14.3 32-32 32z"></path>
              </svg>
            </div>
            <div className="logout-text">Logout</div>
          </button>
        )}
      </div>
    </header>
  );
};

export default Header;
