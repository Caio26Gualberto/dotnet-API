import React, { useState } from 'react';
import Input from '../input/Input';
import Style from './FormLogin.module.css';
import axiosInstance from '../../axiosInstances';
import { ILoginUser } from '../../interfaces/ILoginUser';
import { MouseEventHandler } from 'react';
import ForgotPassword from '../modals/ForgotPasswordModal';
import PopupMessages from '../popupMessages/PopupMessages';

const FormLogin: React.FC<{actionOpenModal: MouseEventHandler<HTMLAnchorElement> |undefined}> = ({actionOpenModal}) => {
  const [login, setLogin] = useState<string>('')
  const [password, setPassword] = useState<string>('')
  const [messagePopup, setMessagePopup] = useState<string>('')
  const [typeOfMessage, setTypeOfMessage] = useState<string>('')
  const [showPopup, setShowPopup] = useState<boolean>(false)


  const showMessage = () => {
    setShowPopup(true);

    // Feche a mensagem após 3 segundos
    setTimeout(() => {
      setShowPopup(false);
    }, 3000);
  };

  const loginRequest = () => {
    if (login === '' || password === '') {
      setMessagePopup('Preencha os campos')
      setTypeOfMessage('warning')
      showMessage()
    }

    const loginData: ILoginUser = {
      login: login!,
      password: password!,
    };

    axiosInstance.post('/Auth/Login', loginData)
    .then((response) => {  
      setMessagePopup(response.data.message)
      setTypeOfMessage('success')
      showMessage()
    }).catch(err => {
      setMessagePopup(err.response.data.message)
      setTypeOfMessage('error')
      showMessage()
    });
  };

  return (
    <>
      {showPopup && <PopupMessages messageType={typeOfMessage} text={messagePopup}/>}
      <form action="#">
        <h1>Entrar</h1>
        <span>Use sua conta</span>
        <Input type="text" name="enterLogin" placeholder="Login ou email" handleOnChange={(e: any) => setLogin(e.target.value)} />
        <Input type="password" id="passwordLogin" placeholder="Senha" handleOnChange={(e: any) => setPassword(e.target.value)} />
        <a id="forgotPasswordLink" onClick={actionOpenModal}>
          Esqueceu sua senha?
        </a>
        <button className={Style.hover_button} onClick={loginRequest}>
          Entrar
        </button>
      </form>
    </>
  );
};

export default FormLogin;
