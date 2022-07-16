
[![.NET](https://github.com/GuardianPlatform/Guardian/actions/workflows/dotnet.yml/badge.svg)](https://github.com/GuardianPlatform/Guardian/actions/workflows/dotnet.yml) [![CodeQL](https://github.com/GuardianPlatform/Guardian/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/GuardianPlatform/Guardian/actions/workflows/codeql-analysis.yml)

---

## Table of Contents
1. [Guardian](#guardian)
2. [How to run project](#how-to-run-project)
4. [Project description (PL)](#project-description-pl)

## Guardian

Guardian is a fanmade video game digital distribution service inspired by Valve platform Steam. This project is just a prototype for studying purposes.

## How to run project:

- Requires .NET 5 + Docker + npm + Local MSSQL
- Start docker
- Start run.bat
- Check if kafka container is running. Sometimes it stops due to internal problem

# Project description (PL):<br>
Guardian – jest to projekt cyfrowej dystrybucji gier. Jego główną funkcjonalnością jest możliwość przeglądania, listowania po kategoriach oraz dodawanie przez twórców nowych gier. Celem tej aplikacji jest danie zwłaszcza twórcom niezależnym miejsca na reklamę swojej twórczości. 
### Architektura projektu:
Projekt utworzony został zaprojektowany wykorzystując „Onion Architecture”. Oznacza to, że mamy podział na:
- Domain Entities
- Domain Service
- Infrastructure
- Presentation/API
- Microservices
- Workers
- Unit Tests
- Integration Tests

![image](https://user-images.githubusercontent.com/20387650/179355166-10e79910-221d-4317-9d30-371ae82a7979.png)

**Domain Entities** – warstwa ta odpowiada za modele biznesowe. Jest to „coreowa” biblioteka dla projektu, ponieważ odpowiada ona za obiekty oraz ich właściwości. W solucji można znaleźć projekt pod nazwą”Guardian.Domain”.

**Domain Service** – warstwa odpowiedzialna za logikę biznesową. Implementuje ona zachowania serwisów oraz funkcjonalności aplikacji dla modeli biznesowych. Ze względu na wykorzystanie wzorca CQRS implementuje ona również zapytania do bazy danych – użycie wzorca „repository” byłoby tutaj zbędne. Wykorzystywana w projekcie biblioteka Entity Framework Core jest już swojego rodzaju implementacją patternu „repository” oraz „unit of work” zgodnie z dokumentacją Microsoftu. W solucji można znaleźć projekt pod nazwą ”Guardian.Service”.

**Infrastructure** – warstwa odpowiedzialna za komunikacje z serwisami zewnętrzmi. Wszystkie projekty infrastruktury znajdują się w folderze „Guardian.Infrastructure”. W projekcie zaimplementowana jest komunikacja z następującymi serwisami:
- Communication pod nazwą „Guardian.Infrastructure.Communication”, implementującą komunikację z zewnętrznymi API, w tym przypadku mikroserwisem do logowania danych. 
-Database pod nazwą „Guardian.Infrastructure.Database”, implementującą obiekty biznesowe w formie encji, ich konfigurację, migracje oraz seedowanie.
- Email pod nazwą „Guardian.Infrastructure.Email”, implementująca wysyłkę wiadomości email poprzez SmtpClient’a
- EventHub – pod nazwą „Guardian.Infrastructure.EventHub”, implementująca połączenie z kolejką „Kafka”

**Presentation/API** – znajdującą się pod nazwą „Guardian”, odpowiada za sterowanie całym projektem wykorzystując pozostałe warstwy.  Istnieje również projekt frontendowy w folderze „guardian-frontend” odpowiedzialny za prezentacje danych graficznie.

**Microservices**:
-  Guardian.Logging.Api – Prosta implementacja logowania do pliku, w solucji znajdziemy w folderze „Guardian.Microservices”. Posiada ona kontrakt/interfejsy w formie biblioteki pod nazwą „Guardian.Logging.Contract”.

**Workers**: 
- Guardian.Worker.Email – Prosta implementacja workera procesująca polecenia wysłania maila z kolejki „Kafka”. Znajduje się ona w folderze „Guardian.Workers”.

**Tests**:
- Guardian.Test.Unit – implementacja testów jednostkowych dla aplikacji Guardian.
- Guardian.Test.Integration – implementacja testów integralnych dla aplikacji Guardian. Wykorzystuje ona WebFactory oraz SQLite. 

