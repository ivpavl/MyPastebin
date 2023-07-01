import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';
import tokenHandler from '../types/handlers';

const Layout = (props) =>
{
  const tokenHandle = new tokenHandler();

  return (
    <div>
      <NavMenu isLogged={tokenHandle.isTokenSet()} />
      <Container tag="main">
        {props.children}
      </Container>
    </div>
  );
}

export default Layout;
