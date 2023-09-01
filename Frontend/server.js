const http = require('http');
const fs = require('fs');
const path = require('path');

const port = 5500; // Porta que você deseja usar

const server = http.createServer((req, res) => {
  const requestedUrl = req.url === '/' ? '/index.html' : req.url;
  const filePath = path.join(__dirname, requestedUrl);
  debugger
  // Verifica se o arquivo existe
  fs.access(filePath, fs.constants.F_OK, (err) => {
    if (err) {
      res.statusCode = 404;
      res.end('Arquivo não encontrado');
      return;
    }

    // Lê o arquivo e envia como resposta
    fs.readFile(filePath, (err, data) => {
      if (err) {
        res.statusCode = 500;
        res.end('Erro interno do servidor');
        return;
      }

      res.statusCode = 200;

      // Define o Content-Type com base na extensão do arquivo
      const ext = path.extname(filePath);
      const contentType = {
        '.html': 'text/html',
        '.css': 'text/css',
        '.js': 'text/javascript'
      }[ext] || 'application/octet-stream';

      res.setHeader('Content-Type', contentType);
      res.end(data);
    });
  });
});

server.listen(port, () => {
  console.log(`Servidor rodando em http://localhost:${port}`);
});