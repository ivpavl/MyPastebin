import React, { useState, useEffect } from 'react';
import '../styles/Home.css';
import tokenHandler from '../types/handlers';



function UserStatus() {
  const [userName, setUserName] = useState<string>('');
  const tokenHandle = new tokenHandler();
  useEffect(() => {
    async function getUserName(): Promise<void> {
        try {
            await fetch('user', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                ...tokenHandle.getAuthorization(),
            }
            })
            .then((response) => response.json())
            .then((response) => {
              console.log(response)
              setUserName(response.userName);
              });;
        } catch (error) {
            console.error(error);
        }
      }
      if(tokenHandle.isTokenSet())
      {
          getUserName();
      }
}, [])

  return (
    <div>
      {
        tokenHandle.isTokenSet() ? <p>Right now you are logged as {userName}</p> : <p>You are not logged in.</p>
      }
    </div>
  );
}

export default UserStatus;
