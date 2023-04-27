import { Counter } from "./components/Counter";
//import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import CompaniesCrud from "./components/CompaniesCrud";
import GamesCrud from "./components/GamesCrud";
import AppsCrud from "./components/AppsCrud";
import DevelopmentDetailsCrud from "./components/DevelopmentDetailsCrud";

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
  }
];

export default AppRoutes;
