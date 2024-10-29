import React from 'react';
import UserItem from './UserItem';
import styles from './UserItem.module.css';

function UserList({userItems, onUserSelect, selectedUserId})
{
    return (
        <ul className={styles.userList}>
          {userItems.map(user => (
            <li 
              key={user.id} 
              onClick={() => onUserSelect(user.id)}
              style={{
                backgroundColor: user.id === selectedUserId ? '#eaeaea' : 'transparent',
                cursor: 'pointer'
              }}
            >
              <UserItem userItem={user} />
            </li>
          ))}
        </ul>
      );
}

export default UserList;