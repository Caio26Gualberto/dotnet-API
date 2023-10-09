import React, { useState , useEffect, useRef} from 'react';
import axiosInstance from '../../axiosInstances'
import Input from '../input/Input';
import Style from './ForgotPasswordModal.module.css';

interface ForgotPasswordProps {
  modalOpen: boolean;
  onClose: () => void;
}

const ForgotPassword: React.FC<ForgotPasswordProps> = ({ modalOpen, onClose }) => {
  const [email, setEmail] = useState<string>('');

  const forgotPassword = () => {
    axiosInstance.get(`/User/ForgottenPassword?email=${encodeURIComponent(email)}`)
    .then(resp => {
      console.log(resp)
    })
  };

  // Adicione um evento de tecla ESC para fechar a modal
  const handleEscapeKey = (event: KeyboardEvent) => {
    if (event.key === "Escape" && modalOpen) {
      onClose();
    }
  };

  useEffect(() => {
    document.addEventListener("keydown", handleEscapeKey);

    return () => {
      document.removeEventListener("keydown", handleEscapeKey);
    };
  }, [modalOpen, onClose]);


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
