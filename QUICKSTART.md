# ⚡ Quick Start Guide

Get up and running in 5 minutes!

## Prerequisites

✅ .NET 8 SDK
✅ Node.js 18+
✅ Git

## 1. Clone & Navigate

```bash
git clone https://github.com/YOUR_USERNAME/HybridPaymentGateway.git
cd HybridPaymentGateway
```

## 2. Start Backend (Terminal 1)

```bash
cd backend
dotnet restore
dotnet run --project src/HybridPaymentGateway.WebApi
```

🟢 Backend running at: `https://localhost:5001`
📄 Swagger UI: `https://localhost:5001/swagger`

## 3. Start Frontend (Terminal 2)

```bash
cd frontend
npm install
npm start
```

🟢 Frontend running at: `http://localhost:4200`

## 4. Open Browser

Navigate to: `http://localhost:4200`

You should see the beautiful dashboard! 🎉

## 🔧 First Time Setup

### Configure Backend

Create `backend/src/HybridPaymentGateway.WebApi/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=hybridpayment.db"
  }
}
```

### Configure Frontend

Edit `frontend/src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001'
};
```

## ✅ Verify Everything Works

1. Backend: Open `https://localhost:5001/swagger`
2. Frontend: Open `http://localhost:4200`
3. Check browser console for any errors

## 🐛 Common Issues

**Port already in use?**
```bash
# Backend - change port
dotnet run --project src/HybridPaymentGateway.WebApi --urls "https://localhost:5002"

# Frontend - change port
ng serve --port 4300
```

**CORS errors?**
- Make sure backend is running
- Check CORS configuration in `Program.cs`

**SSL certificate errors?**
```bash
dotnet dev-certs https --trust
```

## 📚 Next Steps

- Read [SETUP.md](./docs/SETUP.md) for detailed setup
- Check [API Documentation](./docs/API_DOCS.md)
- Explore the code!

---

Happy coding! 🚀
