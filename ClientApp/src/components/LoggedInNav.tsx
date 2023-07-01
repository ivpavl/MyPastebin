import React, { useRef, useState } from 'react';
import '../styles/Home.css';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';



function LoggedInNav() {
  return (
    <>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/cabinet">User cabinet</NavLink>
      </NavItem> 
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/logout">Logout</NavLink>
      </NavItem>
    </>
  );
}

export default LoggedInNav;
