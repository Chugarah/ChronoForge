# Projektstatus

Detta projekt är fortfarande under utveckling. Anledningen till att jag skriver på svenska är att projektet behöver ytterligare arbete och kompletteringar. Dessa styrs huvudsakligen av vad läraren Hans specificerar. Mina egna kompletteringar är betydligt mer omfattande.

## Status

### 1. React TypeScript (Front End)
- Laddar in projekt (Klart)
- Detaljerad sida för projekt (Under utveckling)
- Redigering av formulär (Ej klart)
- Skapa projekt och dess entiteter (Ej klart)

### 2. API (Back End)
- Scalar för utveckling inlagd
- API-endpoints:
  - **Customers**
    - POST
    - GET
  - **PaymentType**
    - GET
  - **Project**
    - POST
    - PUT
    - GET
    - DELETE
    - GET (Alla projekt)
  - **ServiceContracts**
    - POST
    - GET
    - GET (Alla)
  - **Status**
    - POST
    - PUT
    - GET
    - DELETE (Uppdaterar även projektet om statusen inte längre finns)
  - **User**
    - PUT
    - GET
    - DELETE

Större delen av API:et är klart och mycket går att redigera via API:et. Anledningen till att jag inte blev helt färdig är problem med Entity Framework Core, specifikt hantering av många-till-många-relationer. Jag försökte skapa en API-endpoint `/api/Association` för att koppla projekt till servicekontrakt, men EF Core hanterar det inte automatiskt som jag förväntade mig. Jag har haft utmaningar med DTO:er, domänmodeller och entiteter, speciellt vid mappning mellan dessa. En-mot-många och en-mot-en-relationer fungerar enklare.

Planen är nu att invänta feedback från Hans och komplettera vid behov, alternativt få godkänt. Oavsett kommer jag att fortsätta arbeta på detta för att lösa problemen med många-till-många-relationer.

## Godkänd-nivå (G)
- En sida som listar alla befintliga projekt. **(Klart)**
- En sida för att skapa ett nytt projekt. **(React: Ej klart, API: Klart)**
- En sida för att redigera/uppdatera ett befintligt projekt. **(React: Ej klart, API: Klart)**
- Använda **Entity Framework Core - Code First**. **(Implementerat med basrepository och domän till entitetsmappning)**
- Använda en av de presenterade databaserna. **(Använder Microsoft SQL Server, nätverksbaserad)**
- Använda minst två olika entiteter/tabeller. **(Använder ca 5 entiteter, alla med full CRUD)**
- Använda Services för att hantera projekt och kunder. **(Implementerat i Core-lagret)**
- Använda Repositories för att hantera alla entiteter/tabeller. **(Implementerat i Infrastruktur-lagret med basrepository)**
- Använda Dependency Injection. **(Implementerat via Composition Root, DI-containrar i alla lager utom API)**
- Tillämpa **Single Responsibility Principle (S i SOLID)**. **(Målet är en ren arkitektur med DDD och Factory-mönster)**

## Väl godkänd-nivå (VG)
- Generisk basklass för repositories.
- Flera entiteter exponerade via API:et.
- Migration-fil för att bygga databasen (Code First).
- Services som hanterar CRUD där det är lämpligt.
- Tillämpning av **SOLID-principer**. **(Delvis, behöver optimering och rensning)**
- Använda **Factories**. **(Implementerat men behöver optimering)**
- Använda **Task och async/await** för asynkrona operationer. **(Implementerat med try-catch vid databasanrop)**
- Implementera **Transaction Management** för att hantera flera beroende operationer i en transaktion. **(En basklass `UnitOfWork` hanterar transaktioner)**
