# 🚀 HybridPaymentGateway

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-17-red)](https://angular.io/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> A modern full-stack payment gateway supporting both ISO 20022 and Bitcoin transactions

## 📋 Project Overview

HybridPaymentGateway is a complete payment processing solution with:
- **Backend**: .NET 8 Web API with Clean Architecture
- **Frontend**: Angular 17 responsive UI
- **Features**: ISO 20022 payments + Bitcoin transactions

## 🏗️ Repository Structure

```
HybridPaymentGateway/
├── backend/          # .NET 8 Web API
│   ├── src/         # Source code (Domain, Application, Infrastructure, WebApi)
│   └── tests/       # Unit & integration tests
│
├── frontend/         # Angular 17 SPA
│   └── src/         # Angular components, services, models
│
├── docs/            # Documentation
│   ├── SETUP.md
│   └── API_DOCS.md
│
└── README.md        # This file
```

## ⚡ Quick Start

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- npm or yarn
- SQL Server (optional, can use SQLite for dev)

### 1️⃣ Backend Setup

```bash
# Navigate to backend
cd backend

# Restore packages
dotnet restore

# Run the API
dotnet run --project src/HybridPaymentGateway.WebApi

# API will be available at https://localhost:5001
```

### 2️⃣ Frontend Setup

```bash
# Navigate to frontend
cd frontend

# Install dependencies
npm install

# Start dev server
npm start

# UI will be available at http://localhost:4200
```

### 3️⃣ Full Development Environment

```bash
# Terminal 1 - Backend
cd backend
dotnet run --project src/HybridPaymentGateway.WebApi

# Terminal 2 - Frontend
cd frontend
npm start
```

## 🎯 Features

### Backend (.NET 8)
- ✅ Clean Architecture (Domain, Application, Infrastructure, API)
- ✅ ISO 20022 payment processing
- ✅ Bitcoin transaction support
- ✅ RESTful API with Swagger
- ✅ Entity Framework Core
- ✅ Repository Pattern
- ✅ CQRS ready
- ✅ Comprehensive unit tests

### Frontend (Angular 17)
- ✅ Modern responsive UI
- ✅ TypeScript models matching backend
- ✅ Payment management interface
- ✅ Bitcoin wallet integration
- ✅ Real-time transaction updates
- ✅ Beautiful gradient design
- ✅ Mobile-friendly

## 📚 Documentation

- [Backend Documentation](./backend/README.md)
- [Frontend Documentation](./frontend/README.md)
- [Setup Guide](./docs/SETUP.md)
- [API Documentation](./docs/API_DOCS.md)

## 🔧 Configuration

### Backend
Configure in `backend/src/HybridPaymentGateway.WebApi/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HybridPaymentGateway;..."
  },
  "BitcoinNode": {
    "RpcUrl": "http://localhost:8332",
    "RpcUser": "your-user",
    "RpcPassword": "your-password"
  }
}
```

### Frontend
Configure in `frontend/src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001'
};
```

## 🧪 Testing

### Backend Tests
```bash
cd backend
dotnet test
```

### Frontend Tests
```bash
cd frontend
npm test
```

## 📦 Build for Production

### Backend
```bash
cd backend
dotnet publish -c Release -o ./publish
```

### Frontend
```bash
cd frontend
npm run build
# Output in frontend/dist/
```

## 🚀 Deployment

### Backend
- Azure App Service
- Docker container
- IIS / Kestrel

### Frontend
- Azure Static Web Apps
- Netlify / Vercel
- Any static hosting

See [deployment guide](./docs/DEPLOYMENT.md) for details.

## 🏛️ Architecture

### Backend - Clean Architecture Layers

```
┌─────────────────────────────────────┐
│         WebApi Layer                │  Controllers, Middleware
├─────────────────────────────────────┤
│      Infrastructure Layer           │  EF Core, External APIs
├─────────────────────────────────────┤
│      Application Layer              │  Use Cases, DTOs
├─────────────────────────────────────┤
│         Domain Layer                │  Entities, Value Objects
└─────────────────────────────────────┘
```

### Key Design Patterns
- Repository Pattern
- Dependency Injection
- CQRS (ready)
- Value Objects
- Domain Events (ready)

## 🔐 Security Notes

⚠️ **IMPORTANT**: This is a demonstration project.

**Before production use:**
- [ ] Implement authentication (JWT/OAuth)
- [ ] Add authorization policies
- [ ] Secure all endpoints
- [ ] Use environment variables for secrets
- [ ] Implement rate limiting
- [ ] Add input validation
- [ ] Set up HTTPS only
- [ ] Enable CORS properly
- [ ] Implement logging & monitoring
- [ ] Security audit

## 🤝 Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) first.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Your Name**
- Senior .NET Developer
- GitHub: [@yourusername](https://github.com/yourusername)
- LinkedIn: [Your Profile](https://linkedin.com/in/yourprofile)

## 🙏 Acknowledgments

- Clean Architecture by Robert C. Martin
- ISO 20022 Standard
- Bitcoin Core developers
- Angular team

## 📞 Support

For questions or support:
- Open an [issue](https://github.com/yourusername/HybridPaymentGateway/issues)
- Discussions in [Discussions](https://github.com/yourusername/HybridPaymentGateway/discussions)

---

**⭐ If you find this project useful, please consider giving it a star!**
