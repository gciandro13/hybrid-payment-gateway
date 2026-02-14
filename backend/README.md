# HybridPaymentGateway

A .NET 8 Web API implementing Clean Architecture for processing ISO 20022 payment files and Bitcoin transactions.

## 🎯 Project Overview

HybridPaymentGateway è un gateway di pagamento ibrido che gestisce sia pagamenti tradizionali tramite standard ISO 20022 che transazioni Bitcoin. Il progetto implementa Clean Architecture per garantire separazione delle responsabilità, testabilità e manutenibilità.

## 🏗️ Architecture Layers

### 1. **Domain Layer** (Core Business Logic)
Contiene le entità di dominio, value objects e la logica di business fondamentale per ISO 20022 e Bitcoin.

**Responsabilità:**
- Entità core (Payment, Transaction, Account)
- Value Objects (BitcoinAddress, IBAN, Amount)
- Business rules e validazioni di dominio
- Interfacce dei repository (contratti)

**Nessuna dipendenza esterna**

### 2. **Application Layer** (Use Cases)
Contiene i casi d'uso dell'applicazione come `ProcessPayment`, `ParseIso20022File`, `SendBitcoinTransaction`.

**Responsabilità:**
- Use Cases per ISO 20022 e Bitcoin
- DTOs (Data Transfer Objects)
- Service interfaces
- Validatori e mapping
- Orchestrazione della business logic

**Dipendenze:** Domain

### 3. **Infrastructure Layer** (External Integration)
Implementa l'integrazione con database, nodi Bitcoin e parser ISO 20022.

**Responsabilità:**
- Implementazione repository
- DbContext e configurazioni Entity Framework
- Parser e validator ISO 20022
- Client per nodi Bitcoin (Bitcoin Core RPC)
- Servizi esterni e infrastruttura

**Dipendenze:** Application, Domain

### 4. **WebApi Layer** (REST API)
Espone i controller REST per l'interazione con il sistema.

**Responsabilità:**
- REST API Controllers
- Request/Response models
- Middleware (error handling, logging)
- Dependency Injection setup
- Swagger/OpenAPI documentation

**Dipendenze:** Infrastructure, Application, Domain

## 📁 Project Structure

```
HybridPaymentGateway/
├── src/
│   ├── HybridPaymentGateway.Domain/          
│   │   ├── Entities/                          # Payment, Transaction, Account
│   │   ├── ValueObjects/                      # BitcoinAddress, IBAN, Amount
│   │   ├── Enums/                            # PaymentStatus, TransactionType
│   │   ├── Exceptions/                       # Domain exceptions
│   │   └── Interfaces/                       # IPaymentRepository, ITransactionRepository
│   │
│   ├── HybridPaymentGateway.Application/     
│   │   ├── UseCases/
│   │   │   ├── ISO20022/                     # ParseIso20022File, ValidatePayment
│   │   │   └── Bitcoin/                      # ProcessBitcoinPayment, GetBalance
│   │   ├── DTOs/                             # PaymentRequest, TransactionResponse
│   │   ├── Interfaces/                       # IPaymentService, IBitcoinService
│   │   ├── Services/                         # Application services
│   │   ├── Validators/                       # FluentValidation validators
│   │   └── Mappings/                         # AutoMapper profiles
│   │
│   ├── HybridPaymentGateway.Infrastructure/  
│   │   ├── Persistence/                      # ApplicationDbContext
│   │   │   └── Configurations/               # EF Core entity configurations
│   │   ├── Repositories/                     # PaymentRepository, TransactionRepository
│   │   ├── ExternalServices/
│   │   │   ├── ISO20022/                     # ISO 20022 parser implementation
│   │   │   └── Bitcoin/                      # Bitcoin node client (RPC)
│   │   └── Identity/                         # JWT authentication
│   │
│   └── HybridPaymentGateway.WebApi/          
│       ├── Controllers/                       # PaymentsController, BitcoinController
│       ├── Middleware/                        # ExceptionHandlingMiddleware
│       └── Filters/                          # ValidationFilter
│
└── tests/
    ├── HybridPaymentGateway.Domain.Tests/
    ├── HybridPaymentGateway.Application.Tests/
    ├── HybridPaymentGateway.Infrastructure.Tests/
    └── HybridPaymentGateway.WebApi.Tests/
```

## 🚀 Key Features

### ISO 20022 Processing
- **Parsing**: Lettura e parsing di file XML ISO 20022 (pain.001, pacs.008, camt.053)
- **Validation**: Validazione messaggi secondo lo standard
- **Processing**: Elaborazione pagamenti SEPA e internazionali

### Bitcoin Integration
- **Transactions**: Invio e ricezione di transazioni Bitcoin
- **Balance Checking**: Verifica saldo wallet
- **Address Management**: Gestione indirizzi Bitcoin
- **Node Integration**: Connessione a nodi Bitcoin (Bitcoin Core)

### Additional Features
- **Clean Architecture**: Separazione netta delle responsabilità
- **RESTful API**: Endpoints ben documentati con Swagger
- **Validation**: FluentValidation per input validation
- **Error Handling**: Gestione centralizzata degli errori
- **Unit Testing**: Coverage completo con xUnit, Moq, FluentAssertions
- **Logging**: Structured logging con Serilog

## 🛠️ Tech Stack

- **.NET 8**: Framework principale
- **ASP.NET Core Web API**: REST API
- **Entity Framework Core**: ORM per database
- **FluentValidation**: Input validation
- **AutoMapper**: Object mapping
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework
- **Moq**: Mocking framework
- **FluentAssertions**: Assertion library
- **Serilog**: Logging (da configurare)

## 📋 Prerequisites

- .NET 8 SDK
- SQL Server / PostgreSQL (per persistence)
- Bitcoin Core node (opzionale, per testing locale)

## 🚀 Getting Started

```bash
# Clone the repository
git clone https://github.com/your-username/HybridPaymentGateway.git

# Navigate to the solution directory
cd HybridPaymentGateway

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests
dotnet test

# Run the API
dotnet run --project src/HybridPaymentGateway.WebApi

# Navigate to Swagger UI
# https://localhost:5001/swagger
```

## 🔧 Configuration

Configurare `appsettings.json` nel progetto WebApi:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HybridPaymentGateway;..."
  },
  "BitcoinNode": {
    "RpcUrl": "http://localhost:8332",
    "RpcUser": "your-rpc-user",
    "RpcPassword": "your-rpc-password"
  }
}
```

## 📚 API Endpoints

### Payments (ISO 20022)
- `POST /api/payments/parse` - Parse ISO 20022 file
- `POST /api/payments/process` - Process payment
- `GET /api/payments/{id}` - Get payment details

### Bitcoin
- `POST /api/bitcoin/send` - Send Bitcoin transaction
- `GET /api/bitcoin/balance/{address}` - Get wallet balance
- `GET /api/bitcoin/transaction/{txId}` - Get transaction details

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test /p:CollectCoverage=true /p:CoverageReportsFormat=opencover

# Run specific test project
dotnet test tests/HybridPaymentGateway.Domain.Tests
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

MIT

## 👨‍💻 Author

Your Name - Senior .NET Developer
