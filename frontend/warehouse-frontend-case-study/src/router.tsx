import { createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout";
import Home from "./pages/home";
import Products from "./pages/products";
import WareHouses from "./pages/warehouses";

const Router = createBrowserRouter([
    {
      path: '/',
      element: <Layout />,
      children: [
        { path: '/', element: <Home /> },
        { path: '/products', element: <Products /> },
        { path: '/warehouses', element: <WareHouses /> },
      ],
    },
  ]);

export default Router;