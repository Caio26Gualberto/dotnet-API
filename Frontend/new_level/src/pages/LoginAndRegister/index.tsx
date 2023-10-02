import './index.css'


const LoginAndRegister = () => {
	return (
		<>
		<h2 className="welcome">Bem-vindo a página do metal!</h2>
	<div className="container" id="container">
		<div className="form-container sign-up-container">
			<form action="#">
				<h1>Registre-se</h1>
				<span>use seu email para o registro</span>
				<input type="text" id="name" placeholder="Nome" />
				<input type="text" id="login" placeholder="Login" />
				<input type="password" id="password" placeholder="Senha" />
				<input type="email" id="email" placeholder="Email" />
				{/* <button className="hover-button" onClick="registerUser()">Registrar</button> */}
				<div id="messageRegister"></div>
				<span className="span-obs">*Você poderá logar tanto por email ou login</span>
				<span className="span-obs">*Nome será utilizado dentro da plataforma para os outros usuários te reconhecerem</span>
			</form>
		</div>
		<div className="form-container sign-in-container">
			<form action="#">
				<h1>Entrar</h1>
				<span>use sua conta</span>
				<input type="text" id="enterLogin" placeholder="Login ou email" />
				<input type="password" id="passwordLogin" placeholder="Senha" />
				{/* <a href="#" id="forgotPasswordLink" onClick="openModal()">Esqueceu sua senha?</a> */}
				{/* <button className="hover-button" onClick="login()">Entrar</button> */}
				<div id="messageLogin"></div>				
			</form>

		</div>
		<div className="overlay-container">
			<div className="overlay">
				<div className="overlay-panel overlay-left">
					<h1>Bem-vindo de volta!</h1>
					<p>Mantenha-se conectado</p>
					<button className="ghost" id="signIn">Entrar</button>
				</div>
				<div className="overlay-panel overlay-right">
					<h1>Olá, metal!</h1>
					<p>Entre com suas informações e embarque no mundo do rock</p>
					<button className="ghost" id="signUp">Registrar-se</button>
				</div>
			</div>
		</div>
		<div id="modal" className="modal">
			<div className="modal-content">
				{/* <span className="close-btn" onClick="closeModal()">&times;</span> */}
				<h2>Recuperação de Senha</h2>
				<p>Insira seu e-mail para receber as instruções de recuperação:</p>
				<div id="forgotPasswordMessage"></div>
				<input type="email" id="resetPasswordEmail" placeholder="Seu e-mail"></input>
				{/* <button className="ghost" onClick="forgotPassword()">Enviar</button> */}
			</div>
		</div>
	</div>
		</>
	)
}

export default LoginAndRegister