import { Link, Outlet } from "react-router-dom";

const Layout = () => (
    <div>
        <header>
            <nav>
                <ul>
                    <li><Link to="/">Home</Link></li>
                    <li><Link to="/products">Products</Link></li>
                    <li><Link to="/warehouses">WareHouses</Link></li>
                </ul>
            </nav>
        </header>
        <main>
            <Outlet /> 
        </main>
    </div>
);

export default Layout;
