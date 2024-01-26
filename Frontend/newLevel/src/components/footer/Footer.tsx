import React from 'react';
import Style from './Footer.module.css';
import { FaGithub, FaInstagram } from 'react-icons/fa'

const Footer: React.FC = () => {
  return (
    <footer className={Style.footer}>
      <div className={Style.social_icons}>
        <a href="https://www.instagram.com/caio_gualbertoo/" target="_blank" rel="noopener noreferrer">
          <FaInstagram/>
        </a>
        <a href="https://github.com/Caio26Gualberto" target="_blank" rel="noopener noreferrer">
        <FaGithub/>
        </a>
      </div>
      <div className={Style.copyright}>New Level © 2024</div>
    </footer>
  );
};

export default Footer;
