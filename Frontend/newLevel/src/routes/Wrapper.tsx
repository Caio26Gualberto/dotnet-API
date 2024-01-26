// Wrapper.tsx
import React from 'react';
import Navbar from '../components/navbar/Navbar';
import Footer from '../components/footer/Footer';

interface WrapperProps {
  children: React.ReactNode;
  isLoginPage?: boolean;
}

const Wrapper: React.FC<WrapperProps> = ({ children, isLoginPage = false }) => {
  if (!isLoginPage) {
    return (
      <>
        <Navbar />
        {children}
        <Footer />
      </>
    );
  }

  return <>{children}</>;
};

export default Wrapper;
