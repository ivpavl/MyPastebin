import { Counter } from "./components/Counter";
import Post from "./components/Post";
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
];

export default AppRoutes;
