function errorMessage(data, messageElement) {
	console.log(data)
	messageElement.textContent = data;
	messageElement.setAttribute('class', 'errorMessage')
}

function sucessfulMessage(data, messageElement) {
	messageElement.textContent = data;
	messageElement.setAttribute('class', 'sucessfulMessage')
}