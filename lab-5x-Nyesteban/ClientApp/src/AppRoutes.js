import { Counter } from "./components/Counter";
//import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import CompaniesCrud from "./components/CompaniesCrud";
import GamesCrud from "./components/GamesCrud";
import AppsCrud from "./components/AppsCrud";
import DevelopmentDetailsCrud from "./components/DevelopmentDetailsCrud";
import Register from "./components/Register";
import Login from "./components/Login";
import Profile from "./components/Profile";
import Admin from "./components/Admin";

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
    path: '/companies',
    element: <CompaniesCrud />
  },
  {
    path: '/games',
    element: <GamesCrud />
  },
  {
    path: '/apps',
    element: <AppsCrud />
  },
  {
    path: '/developmentdetails',
    element: <DevelopmentDetailsCrud />
  },
  {
    path: '/register',
    element: <Register />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/profile',
    element: <Profile />
  },
  {
    path: '/admin',
    element: <Admin />
  }
];

export default AppRoutes;
