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
- [ ] - Run the API and database with docker; 

## Before Run Project Without Docker

- Make sure you have .Net 8 installed in your machine;

## Run Project Without Docker

1. Clone project to your machine;
2. Enter inside folder of project in your terminal;
3. Inside the folder of the project, enter in the folder **TodoListAPI** on terminal;
4. Run command `donet build` to build the project;
5. Run command `donet run` to run the project: 

Your terminal returns a message like's: `Now listening on: http://localhost:5241`

Now you can enter the project swagger on url: `http://localhost:5241/swagger`

## Run Project Automatized Tests

1. Clone project to your machine;
2. Enter inside folder of project in your terminal;
3. Inside the folder of the project, enter in the folder **TodoListAPI** on terminal;
4. Run command `donet test` to build the project;