# GreenNewJobs

## Requisitos
- Docker
- Docker Compose
- Git

## Como Rodar a Aplicação

### Clone o repositório

Abra o terminal ou prompt de comando e digite os seguintes comandos:

<code>git clone [[https://github.com/seu-usuario/GreenNewJobs.git](https://github.com/brunoxti/Desafio-BackEnd---GreenNewJobs.git)](https://github.com/brunoxti/Desafio-BackEnd---GreenNewJobs.git)</code>

<code>cd GreenNewJobs</code>

### Suba os contêineres Docker

No terminal ou prompt de comando, estando dentro do diretório GreenNewJobs, execute o seguinte comando:

  <code>docker-compose up --build</code>

  Pronto! A aplicação deve estar rodando e acessível via navegador no endereço [http://localhost:8080/swagger](http://localhost:8080/swagger).


### Se quiser testar pelo swagger

Faça o login como admin no endpoint Auth e colete o token para usar nos outros endpoints de admin
![image](https://github.com/brunoxti/Desafio-BackEnd---GreenNewJobs/assets/8594131/759962c9-5cd3-4dfe-a7f4-20a40c29332d)

### Se quiser testar pelo postman

Use a colection <code>GreenNewJobs.postman_collection</code> na pasta raiz do projeto.
faça login e use os endpoints.

![image](https://github.com/brunoxti/Desafio-BackEnd---GreenNewJobs/assets/8594131/e2c790b1-fea5-4f97-9423-e33778fc5438)





