const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});

function openModal() {
	var modal = document.getElementById("modal");
	modal.style.display = "block";
}

// Função para fechar o popup modal
function closeModal() {
	var modal = document.getElementById("modal");
	modal.style.display = "none";
}

// Event listener para abrir o popup modal quando o link for clicado
document.getElementById("forgotPasswordLink").addEventListener("click", openModal);

// Event listener para fechar o popup modal quando o botão "X" for clicado
document.querySelector(".close-btn").addEventListener("click", closeModal);

// Event listener para fechar o popup modal quando o usuário clicar fora do modal
window.addEventListener("click", function (event) {
	var modal = document.getElementById("modal");
	if (event.target === modal) {
		closeModal();
	}
});
function registerUser() {
	const email = document.getElementById('email').value;
	const name = document.getElementById('name').value;
	const login = document.getElementById('login').value;
	const password = document.getElementById('password').value;
	const messageElement = document.getElementById('messageRegister');

	const userData = {
		email: email,
		name: name,
		login: login,
		password: password,
		birthPlace: ''
	};

	if (email === '' ||name === '' ||login === '' || password === '') {
		return errorMessage('Preencha todos os requisitos', messageElement)
	}

	fetch('https://localhost:7213/api/User/Register', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(userData)
	})
		.then(response => {
			if (response.ok) {
				return response.text().then(errorMessageText => sucessfulMessage(errorMessageText, messageElement)); // Trata a resposta como texto
			} else {
				return response.text().then(errorMessageText => errorMessage(errorMessageText, messageElement));
			}
		})
		.catch(error => {
			console.error('Erro:', error);
		});
}

function login() {
	const login = document.getElementById('enterLogin').value;
	const password = document.getElementById('passwordLogin').value;
	const messageElement = document.getElementById('messageLogin')

	if(login === '' || password === '') {
		return errorMessage('Preencha os campos', messageElement)
	}

	const loginData = {
		login: login,
		password: password
	}
	fetch('https://localhost:7213/api/User/Login', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(loginData)
	})
		.then(response => {
			if (response.ok) {
				return response.text().then(errorMessageText => sucessfulMessage(errorMessageText, messageElement)); // Trata a resposta como texto
			} else {
				return response.text().then(errorMessageText => errorMessage(errorMessageText, messageElement));
			}
		})
		.catch(error => {
			console.error('Erro:', error);
		});
}

function forgotPassword() {
	const email = document.getElementById('resetPasswordEmail').value;
	const messageElement = document.getElementById('forgotPasswordMessage');
	if (email === '') {
		return errorMessage('Preencha o campo', messageElement);
	}
	fetch(`https://localhost:7213/api/User/ForgottenPassword?email=${encodeURIComponent(email)}`, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
	})
		.then(response => {
			if (response.ok) {
				return response.text().then(errorMessageText => sucessfulMessage(errorMessageText, messageElement)); // Trata a resposta como texto
			} else {
				return response.text().then(errorMessageText => errorMessage(errorMessageText, messageElement));
			}
		})
		.catch(error => {
			console.error('Erro:', error);
		});
}
function errorMessage(data, messageElement) {
	messageElement.textContent = data;
	messageElement.setAttribute('class', 'errorMessage')
}

function sucessfulMessage(data, messageElement) {
	messageElement.textContent = data;
	messageElement.setAttribute('class', 'sucessfulMessage')
}