import React from 'react';
import UserTextBlockList from '../components/UserTextBlockList';
import UserStatus from '../components/UserStatus';


function UserCabinet() {
  return (
    <div>
      <h3>Welcome to your profile!</h3>
      <UserStatus/>
      <UserTextBlockList/>
    </div>
  );
}

export default UserCabinet;
