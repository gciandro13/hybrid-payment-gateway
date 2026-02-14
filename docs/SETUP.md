# 🛠️ Setup Guide - HybridPaymentGateway

Complete guide to set up the development environment for both backend and frontend.

## 📋 Prerequisites

### Required Software

1. **.NET 8 SDK**
   - Download: https://dotnet.microsoft.com/download/dotnet/8.0
   - Verify: `dotnet --version` (should be 8.0.x)

2. **Node.js 18+**
   - Download: https://nodejs.org/
   - Verify: `node --version` (should be 18.x or higher)
   - npm will be installed automatically

3. **Git**
   - Download: https://git-scm.com/
   - Verify: `git --version`

### Optional but Recommended

- **Visual Studio 2022** or **Visual Studio Code**
- **SQL Server** (or use SQLite for development)
- **Postman** or **Insomnia** for API testing
- **Bitcoin Core** (for Bitcoin integration testing)

## 🚀 Initial Setup

### 1. Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/HybridPaymentGateway.git
cd HybridPaymentGateway
```

### 2. Backend Setup (.NET)

```bash
# Navigate to backend
cd backend

# Restore NuGet packages
dotnet restore

# Build the solution
dotnet build

# Run tests to verify everything works
dotnet test
```

#### Configure User Secrets (Recommended for Development)

```bash
# Navigate to WebApi project
cd src/HybridPaymentGateway.WebApi

# Initialize user secrets
dotnet user-secrets init

# Add your secrets
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=HybridPaymentGateway;Trusted_Connection=true;"
dotnet user-secrets set "BitcoinNode:RpcPassword" "your-bitcoin-rpc-password"
dotnet user-secrets set "JwtSettings:SecretKey" "your-super-secret-key-min-32-chars"
```

#### Or Use appsettings.Development.json (Not Committed)

Create `src/HybridPaymentGateway.WebApi/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HybridPaymentGateway;Integrated Security=true;"
  },
  "BitcoinNode": {
    "RpcUrl": "http://localhost:8332",
    "RpcUser": "bitcoinrpc",
    "RpcPassword": "your-password-here"
  },
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyMinimum32Characters",
    "Issuer": "HybridPaymentGateway",
    "Audience": "HybridPaymentGateway",
    "ExpirationMinutes": 60
  }
}
```

**⚠️ Note**: This file is in `.gitignore` and won't be committed.

#### Database Setup

**Option A: SQL Server**

```bash
# Navigate to WebApi project
cd src/HybridPaymentGateway.WebApi

# Create migration (if not exists)
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

**Option B: SQLite (Simpler for Development)**

In `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=hybridpayment.db"
  }
}
```

Then run migrations as above.

#### Run the Backend

```bash
# From backend root
dotnet run --project src/HybridPaymentGateway.WebApi

# Or with watch (auto-restart on changes)
dotnet watch --project src/HybridPaymentGateway.WebApi
```

API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:5001/swagger`

### 3. Frontend Setup (Angular)

```bash
# Navigate to frontend (from repo root)
cd frontend

# Install dependencies
npm install

# Or use yarn
# yarn install
```

#### Configure Environment

Edit `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001'  // Your backend URL
};
```

#### Run the Frontend

```bash
# Start development server
npm start

# Or
ng serve

# Frontend will be available at http://localhost:4200
```

### 4. Verify Everything Works

1. **Backend Check**:
   - Open `https://localhost:5001/swagger`
   - You should see Swagger UI with API endpoints

2. **Frontend Check**:
   - Open `http://localhost:4200`
   - You should see the dashboard

3. **Integration Check**:
   - From frontend, try to call any API endpoint
   - Check browser console for CORS errors (if any, configure CORS in backend)

## 🔧 Common Issues & Solutions

### CORS Errors

If you get CORS errors when calling API from frontend:

In `Program.cs` (backend):

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// After app.UseRouting()
app.UseCors("AllowAngularApp");
```

### SSL Certificate Issues

If you get SSL errors:

```bash
# Trust the development certificate
dotnet dev-certs https --trust
```

### Port Already in Use

**Backend:**
```bash
# Change port in launchSettings.json or use:
dotnet run --urls "https://localhost:5002;http://localhost:5003"
```

**Frontend:**
```bash
# Use different port
ng serve --port 4300
```

### Database Connection Issues

**Test connection:**
```bash
# In SQL Server Management Studio or Azure Data Studio
# Connection string from appsettings.json
```

**Reset database:**
```bash
cd src/HybridPaymentGateway.WebApi
dotnet ef database drop
dotnet ef database update
```

## 📦 Building for Production

### Backend

```bash
cd backend
dotnet publish -c Release -o ./publish

# Output will be in backend/publish/
```

### Frontend

```bash
cd frontend
npm run build

# Output will be in frontend/dist/hybrid-payment-gateway-ui/
```

## 🧪 Running Tests

### Backend Tests

```bash
cd backend

# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true

# Run specific test project
dotnet test tests/HybridPaymentGateway.Domain.Tests
```

### Frontend Tests

```bash
cd frontend

# Run unit tests
npm test

# Run with coverage
npm test -- --code-coverage

# Run e2e tests (if configured)
npm run e2e
```

## 🐳 Docker Setup (Optional)

Create `docker-compose.yml` in root:

```yaml
version: '3.8'

services:
  backend:
    build: ./backend
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      
  frontend:
    build: ./frontend
    ports:
      - "4200:80"
    depends_on:
      - backend
      
  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
    ports:
      - "1433:1433"
```

Run with:
```bash
docker-compose up
```

## 📝 Next Steps

1. ✅ Read [API Documentation](./API_DOCS.md)
2. ✅ Explore the code structure
3. ✅ Create your first component
4. ✅ Implement authentication
5. ✅ Add your business logic

## 🆘 Need Help?

- Check [GitHub Issues](https://github.com/YOUR_USERNAME/HybridPaymentGateway/issues)
- Read the documentation in `/docs`
- Ask in [Discussions](https://github.com/YOUR_USERNAME/HybridPaymentGateway/discussions)

---

Happy Coding! 🚀
