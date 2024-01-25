import { useState } from 'react'
import FormLogin from '../../components/form/FormLogin'
import FormRegister from '../../components/form/FormRegister'
import Style from './Login.module.css'
import ForgotPassword from '../../components/modals/ForgotPasswordModal'
import PopupMessages from '../../components/popupMessages/PopupMessages'


const Login = () => {

  const[showRegister, setShowRegister] = useState<boolean>(false)
  const[modalOpen, setModalOpen] = useState<boolean>(false)

  const swappignRegister = () => {
    setShowRegister(true)
  }

  const swappingSignin = () => {
    setShowRegister(false)
  }

  const openModal = () => {
    setModalOpen(true);
  };

  const closeModal = () => {
    setModalOpen(false);
  };

  return (
    <>
    <div className={Style.background_image}>
    <PopupMessages/>
    <h2 className={Style.welcome}>Bem-vindo a página do metal!</h2>
    <div className={`${Style.container} ${showRegister ? Style.right_panel_active : null}`}>
      <div className={`${Style.form_container} ${Style.sign_up_container}`}>
        <FormRegister/>
      </div>
      <div className={`${Style.form_container} ${Style.sign_in_container}`}>
        <FormLogin actionOpenModal={openModal}/>
      </div>
      <div className={Style.overlay_container}>
      <ForgotPassword modalOpen={modalOpen} onClose={closeModal} />
			<div className={Style.overlay}>
				<div className={`${Style.overlay_panel} ${Style.overlay_left}`}>
					<h1>Bem-vindo de volta!</h1>
					<p>Mantenha-se conectado</p>
					<button className={Style.ghost} id="signIn" onClick={swappingSignin}>Entrar</button>
				</div>
				<div className={`${Style.overlay_panel} ${Style.overlay_right}`}>
					<h1>Olá metal!</h1>
					<p>Entre com suas informações e embarque no mundo do rock</p>
					<button className={Style.ghost} id="signUp" onClick={swappignRegister}>Registrar-se</button>
				</div>
			</div>
		</div>
    </div>
    </div>
    </>
  )
}

export default Login