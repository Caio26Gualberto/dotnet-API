import { useState } from 'react';
import Input from '../input/Input';
import Style from './FormLogin.module.css';
import axios from 'axios';
import { ILoginUser } from '../../interfaces/ILoginUser';
import ForgotPassword from '../modals/ForgotPassword';

const FormLogin = () => {
  const [login, setLogin] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [modalOpen, setModalOpen] = useState<boolean>(false);

  const loginRequest = () => {
    if (login === '' || password === '') {
      alert('Preencha os campos');
    }

    const loginData: ILoginUser = {
      login: login!,
      password: password!,
    };

    axios.post('https://localhost:7213/api/Auth/Login', loginData).then(function (response) {});
  };

  const openModal = () => {
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
  };

  return (
    <>
      <form action="#">
        <h1>Entrar</h1>
        <span>use sua conta</span>
        <Input type="text" name="enterLogin" placeholder="Login ou email" handleOnChange={(e: any) => setLogin(e.target.value)} />
        <Input type="password" id="passwordLogin" placeholder="Senha" handleOnChange={(e: any) => setPassword(e.target.value)} />
        <a id="forgotPasswordLink" onClick={openModal}>
          Esqueceu sua senha?
        </a>
        <button className={Style.hover_button} onClick={loginRequest}>
          Entrar
        </button>
      </form>
      <ForgotPassword modalOpen={modalOpen} onClose={closeModal} />
    </>
  );
};

export default FormLogin;
