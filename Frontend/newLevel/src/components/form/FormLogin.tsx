import React, { useState } from 'react';
import Input from '../input/Input';
import Style from './FormLogin.module.css';
import axiosInstance from '../../axiosInstances';
import { ILoginUser } from '../../interfaces/ILoginUser';
import { MouseEventHandler } from 'react';
import { useLoading } from '../../context/LoadingContext';
import { useAlert } from '../../context/PopupContext';

const FormLogin: React.FC<{actionOpenModal: MouseEventHandler<HTMLAnchorElement> |undefined}> = ({actionOpenModal}) => {
  const [login, setLogin] = useState<string>('')
  const [password, setPassword] = useState<string>('')
  const { setIsLoading } = useLoading();
  const {showAlert} = useAlert()

  const loginRequest = async () => {
    try {
      if (login === '' || password === '') {
        showAlert('info', 'Preencha os campos')
      } else {
        setIsLoading(true);
      }  
      
      const loginData: ILoginUser = {
        login: login!,
        password: password!,
      };
  
      const resp = await axiosInstance.post('/Auth/Login', loginData);
  
     showAlert('success', resp.data.message);
    } catch (err: any) {
      if (err.code === 'ERR_NETWORK') {
        showAlert('error', err.message)
      } else {
        showAlert('error', err.response.data.message);
      }
      
    } finally {
      setIsLoading(false);
    }
  };
  
  

  return (
    <>
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
