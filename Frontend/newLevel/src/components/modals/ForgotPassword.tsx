import React, { useState } from 'react';
import Input from '../input/Input';
import Style from './ForgotPassword.module.css';

interface ForgotPasswordProps {
  modalOpen: boolean;
  onClose: () => void;
}

const ForgotPassword: React.FC<ForgotPasswordProps> = ({ modalOpen, onClose }) => {
  const [email, setEmail] = useState<string>('');

  const forgotPassword = () => {
    // Implemente a lógica para redefinir a senha aqui
  };

  return (
    <>
      {modalOpen && (
        <div className={`${Style.modal} ${Style.open}`}>
          <div className={Style.modal_content}>
            <span className={Style.close_btn} onClick={onClose}>
              &times;
            </span>
            <h2>Recuperação de Senha</h2>
            <p>Insira seu e-mail para receber as instruções de recuperação:</p>
            <div id="forgotPasswordMessage"></div>
            <Input
              type="text"
              name="resetPasswordEmail"
              placeholder="Seu e-mail"
              value={email}
              handleOnChange={(e: React.ChangeEvent<HTMLInputElement>) => setEmail(e.target.value)}
            />
            <button className={Style.ghost} onClick={forgotPassword}>
              Enviar
            </button>
          </div>
        </div>
      )}
    </>
  );
};

export default ForgotPassword;
