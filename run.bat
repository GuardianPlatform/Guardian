docker pull bashj79/kafka-kraft
docker run -p 9092:9092 -d bashj79/kafka-kraft
dotnet build ./Guardian.Backend/Guardian.Backend.sln
dotnet ef database update --context ApplicationDbContext --project ./Guardian.Backend/Guardian/Guardian.csproj
dotnet ef database update --context IdentityContext --project ./Guardian.Backend/Guardian/Guardian.csproj
start dotnet run --project ./Guardian.Backend/Guardian/Guardian.csproj
start dotnet run --project ./Guardian.Backend/Guardian.Microservices/Guardian.Logging.Api/Guardian.Logging.Api.csproj
start dotnet run --project ./Guardian.Backend/Guardian.Workers/Guardian.Worker.Email/Guardian.Worker.Email.csproj
cd ./guardian-frontend 
call npm install
start npm start