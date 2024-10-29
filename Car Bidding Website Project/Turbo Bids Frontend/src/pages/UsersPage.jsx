import React, { useEffect, useState } from 'react';
import UserList from '../components/UserList';
import UserAPI from '../api/UserAPI';
import '../css/UserPage.css'; // Ensure CSS styles are linked correctly

function UserPage() {
  const [users, setUsers] = useState([]);
  const [selectedUserId, setSelectedUserId] = useState(null);
  const [roles, setRoles] = useState([]);
  const [newUser, setNewUser] = useState({ lastName: '', firstName: '', email: '', password: '', role: '' });

  useEffect(() => {
    const fetchData = async () => {
      try {
        const roleList = await UserAPI.getRoles();
        setRoles(["Select Role", ...roleList]);
        setNewUser({...newUser, role: "Select Role"});
        refreshUserList();
      } catch (error) {
        console.error("Error fetching initial data:", error);
      }
    };

    fetchData();
  }, []);

  const refreshUserList = async () => {
    try {
      const usersData = await UserAPI.getUsers();
      setUsers(usersData);
    } catch (error) {
      console.error("Error loading users:", error);
    }
  };

  const handleAddUser = async () => {
    if (
      newUser.role !== "Select Role" &&
      newUser.lastName.trim() &&
      newUser.firstName.trim() &&
      newUser.email.trim() &&
      newUser.password.trim()
    ) {
      try {
        await UserAPI.addUser(newUser);
        await refreshUserList();
        setNewUser({ lastName: '', firstName: '', email: '', password: '', role: 'Select Role' });
        alert('User added successfully!');
      } catch (error) {
        alert('Failed to add user: ' + error.message);
      }
    } else {
      alert('Please fill all fields and select a role.');
    }
  };

  const handleDeleteUser = async () => {
    if (selectedUserId) {
      try {
        await UserAPI.deleteUser(selectedUserId);
        await refreshUserList();
        setSelectedUserId(null); // Clear the selection after delete
        alert('User deleted successfully!');
      } catch (error) {
        alert('Failed to delete user: ' + error.message);
      }
    } else {
      alert('Please select a user to delete.');
    }
  };

  const handleUserSelect = (id) => {
    setSelectedUserId(id);
  };

  return (
    <div className="container-vehicles">
      <div className="left-column">
        <UserList
          userItems={users}
          onUserSelect={handleUserSelect}
          selectedUserId={selectedUserId}
        />
      </div>
      <div className="right-column">
        <select
          className="form-control mb-2"
          value={newUser.role}
          onChange={(e) => setNewUser({...newUser, role: e.target.value})}
        >
          {roles.map((role, index) => (
            <option key={index} value={role}>{role}</option>
          ))}
        </select>
        <input
          type="text"
          placeholder="Last Name"
          className="form-control mb-2"
          value={newUser.lastName}
          onChange={(e) => setNewUser({...newUser, lastName: e.target.value})}
        />
        <input
          type="text"
          placeholder="First Name"
          className="form-control mb-2"
          value={newUser.firstName}
          onChange={(e) => setNewUser({...newUser, firstName: e.target.value})}
        />
        <input
          type="email"
          placeholder="Email"
          className="form-control mb-2"
          value={newUser.email}
          onChange={(e) => setNewUser({...newUser, email: e.target.value})}
        />
        <input
          type="password"
          placeholder="Password"
          className="form-control mb-2"
          value={newUser.password}
          onChange={(e) => setNewUser({...newUser, password: e.target.value})}
        />
        <button onClick={handleAddUser} className="btn btn-primary btn-full-width">
          Add User
        </button>
        <button onClick={handleDeleteUser} className="btn btn-danger btn-full-width" style={{ marginTop: '1rem' }}>
          Delete Selected User
        </button>
      </div>
    </div>
  );
}

export default UserPage;
