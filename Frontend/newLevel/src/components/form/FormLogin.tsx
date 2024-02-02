import React, { useState } from 'react';
import Input from '../input/Input';
import Style from './FormLogin.module.css';
import { ILoginUser } from '../../interfaces/ILoginUser';
import { MouseEventHandler } from 'react';
import { useLoading } from '../../context/LoadingContext';
import { useAlert } from '../../context/PopupContext';
import { ApiClient } from '../../api/axiosInstanceApi';
import { IUserManagerResponse } from '../../interfaces/IUserManagerResponse';

const FormLogin: React.FC<{ actionOpenModal: MouseEventHandler<HTMLAnchorElement> | undefined }> = ({ actionOpenModal }) => {
  const [login, setLogin] = useState<string>('')
  const [password, setPassword] = useState<string>('')
  const { setIsLoading } = useLoading();
  const { showAlert } = useAlert()
  const axios = new ApiClient('https://localhost:7213/api')

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
      const resp = await axios.post('/Auth/Login', loginData) as IUserManagerResponse;
      const token = resp.token;
      window.localStorage.setItem("Authorization" , token)
      if (!resp.isSkipedFirstPage) {
        window.location.href = 'http://localhost:5173/PostLogin'
      } else {
        showAlert('success', resp.message);
      }

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
