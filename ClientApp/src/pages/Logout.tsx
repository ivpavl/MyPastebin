import React from 'react';
import tokenHandler from '../types/handlers';



function Logout() {
  const tokenHandle = new tokenHandler();
  tokenHandle.removeToken();
  window.location.href = '/';

  return (
      <h1>Logging out</h1>
  );
}

export default Logout;
