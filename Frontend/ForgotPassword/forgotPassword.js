var id;
var email;
const messageElement = document.getElementById('newPasswordMessage');

function init() {
    var url = window.location.href;

    var params = new URLSearchParams(new URL(url).search);

    id = params.get('Id');
    email = params.get('Email');

    console.log("Id: ", id);
    console.log("Email: ", email);
}

function showPassword() {
    const inputPassword = document.getElementById('password')
    const inputNewPassword = document.getElementById('newPassword')

    if (inputPassword.type === 'password' && inputNewPassword.type === 'password') {
        inputPassword.type = 'text'
        inputNewPassword.type = 'text'
    } else {
        inputPassword.type = 'password'
        inputNewPassword.type = 'password'
    }
}

function updatePassword() {
    const userId = id;
    const userPassword = document.getElementById('password').value;
    const userNewPassword = document.getElementById('newPassword').value;
debugger;
    if (userPassword !== userNewPassword) {
        return errorMessage('As senhas diferem', messageElement);
    }
    const updatePasswordData = {
        id: userId,
        password: userNewPassword
    }
    fetch(`https://localhost:7213/api/User/UpdatePassword`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatePasswordData)
    })
        .then(response => {
            debugger;
            if (response.ok) {
                const link = document.createElement('a');
                const text = document.createTextNode('Retornar a página de login');
                link.setAttribute('href', 'http://localhost:5500/LoginAndRegister/loginAndRegister.html')
                link.appendChild(text);
                var linkRedirect = document.getElementById('linkRedirect')
                linkRedirect.appendChild(link)
                response.text().then(errorMessageText => sucessfulMessage(errorMessageText, messageElement)); // Trata a resposta como texto

            } else {
                return response.json().then(data => {
                    const errorMessager = data.errorMessage;
                    return errorMessage(errorMessager, messageElement)
                });
            }
        })
        .catch(error => {
            console.error('Erro:', error);
        });
}

init();