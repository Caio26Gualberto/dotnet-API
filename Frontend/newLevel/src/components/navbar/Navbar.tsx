import React from 'react';
import Style from './Navbar.module.css';
import { Link } from 'react-router-dom';


const Navbar: React.FC = () => {
  return (
    <nav className={Style.navbar}>
      <div className={Style.logo}>Seu Logo</div>
      <ul className={Style.nav_list}>
        <li><Link to="/postLogin">Home</Link></li>
        <li><Link to="/">Login</Link></li>
        <li><Link to="/postLogin">Caio</Link></li>
      </ul>
    </nav>
  );
};

export default Navbar;
