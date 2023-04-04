import { Counter } from "./components/Counter";
//import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import CompaniesCrud from "./components/CompaniesCrud";

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
  }
];

export default AppRoutes;
