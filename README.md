
[![.NET](https://github.com/GuardianPlatform/Guardian/actions/workflows/dotnet.yml/badge.svg)](https://github.com/GuardianPlatform/Guardian/actions/workflows/dotnet.yml) [![CodeQL](https://github.com/GuardianPlatform/Guardian/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/GuardianPlatform/Guardian/actions/workflows/codeql-analysis.yml)

---

## Table of Contents
1. [Guardian](#guardian)
2. [Backend Build](#how-to-build-backend-project)
3. [Frontend Buld](#how-to-build-frontend-project)
4. [Project description (PL)](#project-description-pl)

## Guardian

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


---

# Project description (PL):<br>
Guardian – jest to projekt cyfrowej dystrybucji gier. Jego główną funkcjonalnością jest możliwość przeglądania, listowania po kategoriach oraz dodawanie przez twórców nowych gier. Celem tej aplikacji jest danie zwłaszcza twórcom niezależnym miejsca na reklamę swojej twórczości. 
Architektura projektu:
Projekt utworzony został zaprojektowany wykorzystując „Onion Architecture”. Oznacza to, że mamy podział na:
- Domain Entities
- Domain Service
- Infrastructure
- Presentation/API
- Microservices
- Workers
- Unit Tests
- Integration Tests

Domain Entities – warstwa ta odpowiada za modele biznesowe. Jest to „coreowa” biblioteka dla projektu, ponieważ odpowiada ona za obiekty oraz ich właściwości. W solucji można znaleźć projekt pod nazwą”Guardian.Domain”.

Domain Service – warstwa odpowiedzialna za logikę biznesową. Implementuje ona zachowania serwisów oraz funkcjonalności aplikacji dla modeli biznesowych. Ze względu na wykorzystanie wzorca CQRS implementuje ona również zapytania do bazy danych – użycie wzorca „repository” byłoby tutaj zbędne. Wykorzystywana w projekcie biblioteka Entity Framework Core jest już swojego rodzaju implementacją patternu „repository” oraz „unit of work” zgodnie z dokumentacją Microsoftu. W solucji można znaleźć projekt pod nazwą ”Guardian.Service”.

Infrastructure – warstwa odpowiedzialna za komunikacje z serwisami zewnętrzmi. Wszystkie projekty infrastruktury znajdują się w folderze „Guardian.Infrastructure”. W projekcie zaimplementowana jest komunikacja z następującymi serwisami:
- Communication pod nazwą „Guardian.Infrastructure.Communication”, implementującą komunikację z zewnętrznymi API, w tym przypadku mikroserwisem do logowania danych. 
-Database pod nazwą „Guardian.Infrastructure.Database”, implementującą obiekty biznesowe w formie encji, ich konfigurację, migracje oraz seedowanie.
- Email pod nazwą „Guardian.Infrastructure.Email”, implementująca wysyłkę wiadomości email poprzez SmtpClient’a
- EventHub – pod nazwą „Guardian.Infrastructure.EventHub”, implementująca połączenie z kolejką „Kafka”

Presentation/API – znajdującą się pod nazwą „Guardian”, odpowiada za sterowanie całym projektem wykorzystując pozostałe warstwy.  Istnieje również projekt frontendowy w folderze „guardian-frontend” odpowiedzialny za prezentacje danych graficznie.

Microservices:
-  Guardian.Logging.Api – Prosta implementacja logowania do pliku, w solucji znajdziemy w folderze „Guardian.Microservices”. Posiada ona kontrakt/interfejsy w formie biblioteki pod nazwą „Guardian.Logging.Contract”.

Workers: 
- Guardian.Worker.Email – Prosta implementacja workera procesująca polecenia wysłania maila z kolejki „Kafka”. Znajduje się ona w folderze „Guardian.Workers”.

Unit Tests:
- Guardian.Test.Unit – implementacja testów jednostkowych dla aplikacji Guardian.
- Guardian.Test.Integration – implementacja testów integralnych dla aplikacji Guardian. Wykorzystuje ona WebFactory oraz SQLite. 

