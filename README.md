<p align="center">
  <img src="https://user-images.githubusercontent.com/20674439/158480674-3b8895e7-420e-4025-bd78-8058ba255476.png"  width="150" alt="Logo do projeto" />
</p>  
<p align="center">
   🚀 Este projeto é uma aplicação de estacionamento que oferece operações básicas, como criar, atualizar, buscar e excluir informações sobre estacionamentos.
</p>

 ## 🚀 Tecnologias Utilizadas
 - MediatR: Biblioteca de mediação para aplicativos .NET. Facilita a implementação do padrão Mediator para comunicação entre componentes.
 - MongoDB: Banco de dados NoSQL utilizado para armazenar os dados do estacionamento.
 - Docker: Plataforma de containerização que permite empacotar, distribuir e executar aplicativos em contêineres.
 - FluentValidation: Biblioteca para validação de entrada. Utilizada para validar os dados de entrada antes de processá-los.
 - Bogus: Biblioteca para geração de dados falsos. Usada em testes para criar dados fictícios.
 - FluentAssertions: Framework para escrever assertivas de maneira fluente e expressiva em testes unitários.
 - xUnit: Framework de teste unitário para a execução de testes na aplicação.
 - Moq: Framework de mocking para criar objetos fictícios em testes unitários.

## Pré-requisitos
 - Docker: Certifique-se de ter o Docker instalado na sua máquina.

## 🚀 Como Iniciar o Projeto
 - Clone o Repositório:
 git clone https://github.com/felipesbcabral/parking-challenge

## - Inicie os Contêineres Docker:
 docker-compose up -d

## - Acesse a Aplicação: 
 A aplicação estará disponível onde PORTA é a porta configurada no arquivo de composição do Docker.

## Estrutura do Projeto
   - ParkingChallenge: API da aplicação.
   - ParkingChallenge.Core: Lógica de negócios e entidades.
   - ParkingChallenge.UnitTests: Onde realizamos os testes da aplicação.

## Contribuições
 - Contribuições são bem-vindas! Se deseja contribuir com o projeto, abra uma issue ou envie um pull request.
