import { Counter } from "./components/Counter";
import Post from "./components/Post.tsx";
import Login from "./components/Login.tsx";
import Home from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/post',
    element: <Post />
  },
  {
    path: '/login',
    element: <Login />
  },
];

export default AppRoutes;
