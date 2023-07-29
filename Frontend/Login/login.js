const signUpButton = document.getElementById('signUp');
const signInButton = document.getElementById('signIn');
const container = document.getElementById('container');

signUpButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

signInButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});

function registerUser() {
	const email = document.getElementById('email').value;
	const name = document.getElementById('name').value;
	const login = document.getElementById('login').value;
	const password = document.getElementById('password').value;

	const userData = {
		email: email,
		name: name,
		login: login,
		password: password,
		birthPlace: ''
	};

	fetch('https://localhost:7213/api/User/Register', {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(userData)
	})
		.then(response => response.json())
		.then(data => {
			console.log();
			if (data.hasOwnProperty('success')) {
				const messageElement = document.getElementById('messageRegister');
				if (data.success) {
					messageElement.textContent = data.message; // Mensagem de sucesso
				} else {
					messageElement.textContent = data.message; // Mensagem de erro
				}
			} else {
				messageElement.textContent = data.message;
			}
		})
}

function login() {
	const login = document.getElementById('enterLogin').value;
	const password = document.getElementById('passwordLogin').value;

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
	.then(response => response.json())
		.then(data => {
			console.log();
			if (data.hasOwnProperty('success')) {
				const messageElement = document.getElementById('messageLogin');
				if (data.success) {
					messageElement.textContent = data.message; // Mensagem de sucesso
				} else {
					messageElement.textContent = data.message; // Mensagem de erro
					messageElement.setAttribute('class', 'errorMessage')
				}
			} else {
				messageElement.textContent = data.message;
			}
		})
}