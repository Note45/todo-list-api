# TodoList API

TodoList API is an API developed in .Net with the aim of saving user to-dos in a database.

## Functional Requirements

- [X] - Create, update and delete a user;
- [X] - Login with user;
- [X] - Create, list, update and delete user's to-do;

## Technical Requirements

- [X] - Develop the API in .Net 8;
- [X] - Use Postgres as database;
- [X] - Create automatized tests;  
- [X] - Run the API and database with docker;

## Run Project With Docker

1. Clone project to your machine;
2. Enter inside folder of project in your terminal;
3. Change database credentials in the files `TodoListAPI/docker-compose.yml` and `TodoListAPI/appsettings.Sandbox.json` to your configs;
5. Run command `docker-compose up -d` to run the project;
6. Run command `dotnet ef database update` to run the database migrations;
7. Access the URL `http://localhost:8081/swagger` to viewer the project Swagger; 

## Before Run Project Without Docker

- Make sure you have .Net 8 installed in your machine;
- Make sure you have Postgres installed and configured with a database created;
- Make sure you add your database credentials in the file `TodoListAPI/appsettings.Development.json`;

## Run Project Without Docker

1. Clone project to your machine;
2. Enter inside folder of project in your terminal;
3. Inside the folder of the project, enter in the folder **TodoListAPI** on terminal;
4. Run command `donet build` to build the project;
5. Run command `donet run` to run the project.

Your terminal returns a message like's: `Now listening on: http://localhost:5241`

Now you can enter the project swagger on url: `http://localhost:5241/swagger`

## Run Project Automatized Tests

1. Clone project to your machine;
2. Enter inside folder of project in your terminal;
3. Inside the folder of the project, enter in the folder **TodoListAPI** on terminal;
4. Run command `donet test` to build the project;