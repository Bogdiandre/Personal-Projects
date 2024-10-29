import React, { useState } from 'react';
import '../css/Login.css';

function FlipCardLogin({ onLogin, onSignup }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');

  const handleLoginSubmit = async (event) => {
    event.preventDefault();
    try {
      await onLogin(email, password);
      alert('Login successful!');
    } catch (error) {
      alert('Failed to login: ' + error.message);
    }
  };

  const handleSignupSubmit = async (event) => {
    event.preventDefault();
    // Log the values to verify they are not null or empty
    console.log('FlipCardLogin - First Name:', firstName);
    console.log('FlipCardLogin - Last Name:', lastName);
    console.log('FlipCardLogin - Email:', email);
    console.log('FlipCardLogin - Password:', password);

    try {
      await onSignup(firstName, lastName, email, password);
      alert('Client signed up successfully!');
      setFirstName('');
      setLastName('');
      setEmail('');
      setPassword('');
    } catch (error) {
      console.error('Error during signup:', error);
      alert('Failed to sign up client: ' + error.message);
    }
  };

  return (
    <div className="wrapper">
      <div className="card-switch">
        <label className="switch">
          <input type="checkbox" className="toggle" />
          <span className="slider"></span>
          <span className="card-side"></span>
          <div className="flip-card__inner">
            <div className="flip-card__front">
              <div className="title">Log in</div>
              <form className="flip-card__form" onSubmit={handleLoginSubmit}>
                <input
                  className="flip-card__input"
                  name="email"
                  placeholder="Email"
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
                <input
                  className="flip-card__input"
                  name="password"
                  placeholder="Password"
                  type="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
                <button className="flip-card__btn" type="submit">Let's go!</button>
              </form>
            </div>
            <div className="flip-card__back">
              <div className="title">Sign up</div>
              <form className="flip-card__form" onSubmit={handleSignupSubmit}>
                <input
                  className="flip-card__input"
                  name="firstName"
                  placeholder="First Name"
                  type="text"
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                />
                <input
                  className="flip-card__input"
                  name="lastName"
                  placeholder="Last Name"
                  type="text"
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                />
                <input
                  className="flip-card__input"
                  name="email"
                  placeholder="Email"
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                />
                <input
                  className="flip-card__input"
                  name="password"
                  placeholder="Password"
                  type="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                />
                <button className="flip-card__btn" type="submit">Confirm!</button>
              </form>
            </div>
          </div>
        </label>
      </div>
    </div>
  );
}

export default FlipCardLogin;
