# Hybrid Payment Gateway - Angular Frontend

Angular 17 frontend application for the HybridPaymentGateway .NET 8 API.

## 🎯 Features

- 📱 Responsive design with modern UI
- 🏦 Payment management interface
- ₿ Bitcoin transaction support
- 📊 Transaction history
- 🔄 Real-time status updates
- 🎨 Beautiful gradient design

## 📁 Project Structure

```
src/
├── app/
│   ├── components/          # Angular components (add your own)
│   ├── services/            # API services
│   │   ├── payment.service.ts
│   │   └── bitcoin.service.ts
│   ├── models/              # TypeScript models
│   │   └── payment.models.ts
│   ├── app.component.*      # Root component
│   ├── app.module.ts        # Main module
│   └── app-routing.module.ts
├── environments/            # Environment configurations
├── assets/                  # Static assets
└── styles.scss             # Global styles
```

## 🚀 Getting Started

### Prerequisites

- Node.js 18+ and npm
- Angular CLI 17

### Installation

```bash
# Install dependencies
npm install

# Install Angular CLI globally (if not already installed)
npm install -g @angular/cli@17
```

### Configuration

Update the API URL in `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001' // Your .NET API URL
};
```

### Development Server

```bash
# Start development server
npm start

# Or use Angular CLI
ng serve

# Navigate to http://localhost:4200
```

The application will automatically reload when you change any source files.

### Build

```bash
# Production build
npm run build

# Or
ng build --configuration production

# Output will be in dist/hybrid-payment-gateway-ui/
```

## 🔌 API Integration

### Services Available

#### PaymentService

```typescript
import { PaymentService } from './services/payment.service';

constructor(private paymentService: PaymentService) {}

// Get all payments
this.paymentService.getAllPayments().subscribe(payments => {
  console.log(payments);
});

// Create payment
const request: CreatePaymentRequest = {
  paymentReference: 'PAY-001',
  type: PaymentType.BankTransfer,
  amount: 1000,
  currency: 'EUR',
  debtorAccountId: 'account-id-1',
  creditorAccountId: 'account-id-2'
};

this.paymentService.createPayment(request).subscribe(response => {
  console.log(response);
});
```

#### BitcoinService

```typescript
import { BitcoinService } from './services/bitcoin.service';

constructor(private bitcoinService: BitcoinService) {}

// Get balance
this.bitcoinService.getBalance('bc1q...').subscribe(balance => {
  console.log(balance);
});

// Send transaction
const request: CreateBitcoinTransactionRequest = {
  paymentId: 'payment-id',
  amount: 0.001,
  fromAddress: 'bc1q...',
  toAddress: 'bc1q...'
};

this.bitcoinService.sendTransaction(request).subscribe(response => {
  console.log(response);
});
```

## 🎨 Adding New Components

### Example: Create a Payments List Component

```bash
# Generate component
ng generate component components/payments-list

# Or short form
ng g c components/payments-list
```

Then add to routing in `app-routing.module.ts`:

```typescript
import { PaymentsListComponent } from './components/payments-list/payments-list.component';

const routes: Routes = [
  { path: 'payments', component: PaymentsListComponent },
  // ... other routes
];
```

### Example Component Implementation

```typescript
// payments-list.component.ts
import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../../services/payment.service';
import { Payment } from '../../models/payment.models';

@Component({
  selector: 'app-payments-list',
  templateUrl: './payments-list.component.html',
  styleUrls: ['./payments-list.component.scss']
})
export class PaymentsListComponent implements OnInit {
  payments: Payment[] = [];
  loading = false;
  error: string | null = null;

  constructor(private paymentService: PaymentService) {}

  ngOnInit(): void {
    this.loadPayments();
  }

  loadPayments(): void {
    this.loading = true;
    this.paymentService.getAllPayments().subscribe({
      next: (data) => {
        this.payments = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = err.message;
        this.loading = false;
      }
    });
  }
}
```

## 🎨 Styling

The app uses SCSS with a modern gradient theme. Global styles are in `src/styles.scss`.

### Available CSS Classes

- `.card` - Card container
- `.btn`, `.btn-primary`, `.btn-secondary` - Buttons
- `.form-group` - Form field wrapper
- `.alert-success`, `.alert-error`, `.alert-info` - Alert messages
- `.badge-success`, `.badge-warning`, `.badge-danger` - Status badges
- `.table` - Data tables

## 📝 Models

All TypeScript models are in `src/app/models/payment.models.ts`:

- `Payment` - Payment entity
- `Transaction` - Transaction entity
- `Account` - Account entity
- `Money` - Money value object
- Enums: `PaymentStatus`, `PaymentType`, `TransactionType`, etc.

## 🔧 Configuration

### CORS Setup

Make sure your .NET API has CORS configured:

```csharp
// In Program.cs or Startup.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

app.UseCors("AllowAngularApp");
```

### Proxy Configuration (Optional)

Create `proxy.conf.json` in the root:

```json
{
  "/api": {
    "target": "https://localhost:5001",
    "secure": false,
    "changeOrigin": true
  }
}
```

Then update `angular.json`:

```json
"serve": {
  "options": {
    "proxyConfig": "proxy.conf.json"
  }
}
```

## 🧪 Testing

```bash
# Run unit tests
ng test

# Run e2e tests
ng e2e
```

## 📦 Deployment

### Build for Production

```bash
ng build --configuration production
```

### Deploy to Azure Static Web Apps

1. Create Azure Static Web App
2. Configure GitHub Actions
3. Push to main branch

### Deploy to Netlify/Vercel

```bash
# Build
ng build --configuration production

# Deploy dist/hybrid-payment-gateway-ui/
```

## 🤝 Integration with .NET Backend

1. Start your .NET API: `dotnet run --project src/HybridPaymentGateway.WebApi`
2. Update `environment.ts` with the API URL
3. Start Angular app: `npm start`
4. Navigate to `http://localhost:4200`

## 📚 Next Steps

1. ✅ Create payment components in `src/app/components/`
2. ✅ Implement forms for creating payments
3. ✅ Add authentication (JWT)
4. ✅ Add state management (NgRx/Akita) if needed
5. ✅ Add real-time updates (SignalR)
6. ✅ Implement error handling
7. ✅ Add loading states
8. ✅ Create dashboards and charts

## 📄 License

MIT
