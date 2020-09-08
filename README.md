#Implementation notes

##database
database structure in Database.sql

##data type for money
using decimal in .net and sql server for money precision

##application layers
1. domain layer - core business object and their DTO (request and response object) for MVVM pattern, Automapper is used to map between them, also interfaces for Repository pattern. This layer can be reused if choosing different database layer eg. Dapper
2. model validation using FluentValidation - it will return 400 error with useful error messages
3. database layer - concrete database layer and its repository implementation
4. if there is external service call - i will use HttpClient with Polly for circuit breaker pattern

##application error handling
using a middleware to intercept internal exceptions so not too much information will be seen by user but writing to application log. 

##health check
it's common to deploy application in cloud with k8s and the platform need to poll application with an interval to make sure it's responding. Created health check endpoint for this purpose. It simply return a value but some improvements can be made for checking database as well

##application security
basic authentication is implemented but valid user (user/password) is hard coded at the moment. but it can be connected to user repository easily. for any endpoints need authentication, it needs to add Authorization header with a based 64 encrypted string for "Basic user:password" 

##swagger
swagger doc is implemented - http://localhost:5000/swagger/index.html 
can use basic authentication

##cache
memory cache is implemented

##application logging
just used internal logger to log to console

##versioning
use v1/resource
v2 can be implemented with a controller in v2 folder

##unit tests
xunit, moq

##future improvements
400 model validation message can be customized for better structure
unit tests on validators
HATEOAS
level 3 rest api (https://en.wikipedia.org/wiki/HATEOAS) - a returned object is with a field Links with its associated valid action. Eg. GET /product/1 will return a product with its link to update or delete this resourse