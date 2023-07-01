import React from 'react';
import Post from "./pages/Post";
import Login from "./pages/Login";
import Home from "./pages/Home";
import Register from './pages/Register';
import Logout from './pages/Logout';
import UserCabinet from './pages/UserCabinet';

interface Route{
  index?: boolean,
  key: number,
  element: Function,
  path: string,
}

const AppRoutes: Route[] = [
  {
    index: true,
    path: '/',
    key: 1,
    element: <Home />,
  },
  {
    path: '/post',
    key: 2,
    element: <Post />
  },
  {
    path: '/login',
    key: 3,
    element: <Login />
  },
  {
    path: '/register',
    key: 4,
    element: <Register />
  },
  {
    path: '/logout',
    key: 5,
    element: <Logout />
  },
  {
    path: '/cabinet',
    key: 6,
    element: <UserCabinet />
  },
];

export default AppRoutes;
