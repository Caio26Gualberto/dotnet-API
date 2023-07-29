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
	// Obtenha os valores dos campos de entrada
	const email = document.getElementById('email').value;
	const name = document.getElementById('name').value;
	const login = document.getElementById('login').value;
	const password = document.getElementById('password').value;
  
	// Construa o objeto de dados para enviar na solicitação fetch
	const userData = {
	  email: email,
	  name: name,
	  login: login,
	  password: password,
	  birthPlace: ''
	};
  
	// Realize a solicitação fetch para o endpoint de registro
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
      const messageElement = document.getElementById('message');
      if (data.success) {
        messageElement.textContent = 'Registro bem-sucedido!'; // Mensagem de sucesso
      } else {
        messageElement.textContent = data.message; // Mensagem de erro
      }
    } else {
      messageElement.textContent =('Resposta do servidor inválida:', data);
    }
  })
  }