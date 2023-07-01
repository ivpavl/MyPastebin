import React, {useEffect, useState} from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import '../styles/NavMenu.css';
import LoggedInNav from './LoggedInNav';
import LoggedOutNav from './LoggedOutNav';

interface INavMenu
{
  isLogged: boolean,
}

function NavMenu({isLogged}: INavMenu) {
  let collapsed: boolean = true;
  function toggleNavbar () {
    collapsed = !collapsed;
  }
  const [loggedIn, setLoggedIn] = useState<boolean>(isLogged);

  useEffect(() => {
    setLoggedIn(isLogged);
  }, [isLogged]);


  return (
    <header>
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
        <NavbarBrand tag={Link} to="/">MyPastebin</NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={collapsed} navbar>
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
            </NavItem>
            {
              loggedIn ?
              <LoggedInNav/>
              :
              <LoggedOutNav/>
              }
            
          </ul>
        </Collapse>
      </Navbar>
    </header>
  );
}

// export class NavMenu extends Component {

//   constructor (props) {
//     super(props);

//     this.toggleNavbar = this.toggleNavbar.bind(this);
//     this.state = {
//       collapsed: true
//     };
//   }



// }
export default NavMenu;