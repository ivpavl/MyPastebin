import React, { useRef, useState } from 'react';
import '../styles/Home.css';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';



function LoggedOutNav() {
  return (
    <>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to="/register">Register</NavLink>
      </NavItem>
    </>
  );
}

export default LoggedOutNav;
