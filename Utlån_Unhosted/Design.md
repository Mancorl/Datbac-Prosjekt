# 2.1 Systemdiagram – Unhosted arkitektur

Systemet er designet som et **unhosted, offline-first utlånssystem for brettspill**.  
Alle komponenter kjører lokalt på samme enhet (f.eks. PC eller nettbrett), uten sentral server eller backend.

Systemet er delt inn i **avgrensede kontekster (DDD-inspirert)** som kommuniserer via **in-prosess kall og lokale domene-events**.

---

## Arkitekturprinsipper

- Ingen ekstern backend eller hosting
- Alle contexts er **lokale moduler**
- Lokal persistens (f.eks. SQLite, IndexedDB eller filbasert lagring)
- Event-basert samhandling internt i applikasjonen
- UI er koblet til domenet via application services
- Login håndteres lokalt (rollebasert eller enkel brukeridentifikasjon)

---

## Kontekstbeskrivelser

### UserContext
- Håndterer aktiv bruker og rolle (Admin / Lender)
- Login setter gjeldende brukerkontekst
- Ingen ekstern autentisering

### AdminContext
- Administrative handlinger:
  - Oppdatere brettspill
  - Godkjenne utlånere (Lender)
- Sender kommandoer til andre contexts

### BoardGameContext
- Eier brettspill-aggregeringer
- Holder status på spill (Tilgjengelig / Utlånt / Defekt)
- Publiserer `BoardGameChanged`-events

### OrderContext
- Ansvarlig for lånebestillinger
- Inneholder `Order`-aggregate
- Eksponerer `IOrderingService` som lokal application service

### FulfillmentContext
- Utfører selve utlånet
- Kobler Order, Lender og BoardGame
- Publiserer events ved utlån og retur

### LenderContext
- Representerer brukere som kan låne spill
- Kobles til Fulfillment ved aktivt utlån

---

## Application Services

`IOrderingService` er **ikke et backend-API**, men en lokal application service som:
- brukes av UI
- orkestrerer domenelogikk
- oppretter og validerer Order-aggregater

---

## Hendelser (Domain Events)

- `BoardGameChanged`
- `BoardGameBorrowed`
- `BoardGameRetrieved`

Alle events:
- publiseres lokalt
- håndteres via intern event-bus
- brukes til å oppdatere UI og andre contexts

---

## Systemdiagram (Unhosted)

```mermaid
flowchart TB
 subgraph OrderContext["OrderContext (Local Module)"]
        Order["Order Aggregate"]
        IOrderingService["IOrderingService (Application Service)"]
  end

  subgraph UserContext["UserContext"]
        Login["Login (Local)"]
        User["Active User / Role"]
        Login --> User
  end

  subgraph AdminContext["AdminContext"]
        Admin["Admin Actions"]
  end

  subgraph BoardGameContext["BoardGameContext"]
        BoardGame["BoardGame Aggregate"]
  end

  subgraph LenderContext["LenderContext"]
        Lender["Lender"]
  end

  subgraph FulfillmentContext["FulfillmentContext"]
        Fulfillment["Fulfillment"]
        BoardGameLent["BoardGameLent"]
  end

  %% Flows
  User --> IOrderingService
  IOrderingService --> Order

  Admin -. updateBoardGameItem .-> BoardGame
  Admin -. acceptLender .-> Lender

  BoardGame -. BoardGameChanged .-> User

  Order -. BorrowBoardGame .-> Fulfillment
  Lender -. SetLender .-> Fulfillment

  Fulfillment -. BoardGameBorrowed .-> BoardGameLent
  BoardGameLent -. BoardGameRetrieved .-> Fulfillment