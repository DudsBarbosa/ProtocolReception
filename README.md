# ProtocolReception
This is a simple project that simulates the reception of a protocol document.
The project has two console applications: one that sends a message in a string format and another that receives the message, both use docker containers to run, `ProtocolReception.ConsolePublisher` and `ProtocolReception.ConsoleConsumer`.

## How it works
The project uses a message broker to send and receive messages, in this case, RabbitMQ. 
The `ProtocolReception.ConsolePublisher` sends a message to the broker and the `ProtocolReception.ConsoleConsumer` receives the message and saves it to the database.
If the message is not in the correct format, the consumer will not save it to the database and will save in a database log table instead.

## How to run
To run the project, you must have Docker, Visual Studio 2022 and SQL Server installed on your machine.
If you have all the prerequisites, you can run the project by following these steps:
1. Clone the repository
1. Open the solution file in Visual Studio 2022
1. Run the project
1. The project will be available at `https://localhost:7075`
1. You can use the Swagger interface to test the API endpoints at `https://localhost:7075/swagger/index.html`
1. Token authentication is enabled, so you must get a token to use the API endpoints that require authentication. You can get a token by calling the https://localhost:7075/api/Auth
 endpoint with the following body:
```json
{
  "username": "admin",
  "password": "admin"
}
```
1. The token will be returned in the response body, you must use this token using Postman or another tool to call the API endpoints that require authentication by adding the token in the Authorization header with the Bearer token type.

## Tests
Improvement is needed in the tests, but you can run Integration Tests the tests by following these steps:
1. Open the Test Explorer in Visual Studio 2022
1. Run the tests
1. The tests will run and you can see the results in the Test Explorer
1. The tests will run against the database, so you must have the database running 

## Technologies
- .NET 7
- Entity Framework Core
- RabbitMQ
- Docker
- SQL Server
- Swagger

## Database scripts
The database scripts are in the `ProtocolReception.Infrastructure` project, you can find the scripts in the `Repositories\Scripts` folder.