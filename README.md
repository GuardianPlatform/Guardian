# Guardian

[![.NET](https://github.com/GuardianPlatform/Guardian/actions/workflows/dotnet.yml/badge.svg)](https://github.com/GuardianPlatform/Guardian/actions/workflows/dotnet.yml) [![CodeQL](https://github.com/GuardianPlatform/Guardian/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/GuardianPlatform/Guardian/actions/workflows/codeql-analysis.yml)

Guardian is a fanmade video game digital distribution service inspired by Valve platform Steam. This project is just a prototype for studying purposes.

## How to build backend project:

- Requires .NET 5 + Docker
- Pull and run kafka:

```
(this is a small version of the official kafka)
docker pull bashj79/kafka-kraft
docker run -p 9092:9092 -d bashj79/kafka-kraft
```

- Run .NET projects, preferably using multiple startup project:
  ![image](https://user-images.githubusercontent.com/20387650/178614809-8404bf1b-81ea-43e2-97da-ede6996104cb.png)

#### docker-compose exist, but is still not ready and the connection with DB or API may not work

---

## How to build frontend project:

- Requires node/npm
- Move to guardian-frontend folder
- Open terminal/console
- Run "npm start"

![image](https://user-images.githubusercontent.com/20387650/178614015-0680dd93-6ada-47a2-b1e1-1849310a132c.png)
![image](https://user-images.githubusercontent.com/20387650/178614005-551247c2-bb14-4bb4-8291-a94e048d6bff.png)
