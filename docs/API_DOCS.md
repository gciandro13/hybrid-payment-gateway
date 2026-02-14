# 📖 API Documentation

## Base URL

Development: `https://localhost:5001/api`

Production: `https://your-domain.com/api`

## Authentication

⚠️ **Currently not implemented**. Add JWT authentication before production use.

## Endpoints

### Payments

#### Get All Payments

```http
GET /api/payments
```

**Response:**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "paymentReference": "PAY-001",
    "type": 0,
    "amount": {
      "amount": 1000.00,
      "currency": "EUR"
    },
    "status": 0,
    "createdAt": "2024-02-14T10:30:00Z"
  }
]
```

#### Get Payment by ID

```http
GET /api/payments/{id}
```

**Parameters:**
- `id` (UUID) - Payment ID

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "paymentReference": "PAY-001",
  "type": 0,
  "amount": {
    "amount": 1000.00,
    "currency": "EUR"
  },
  "status": 0,
  "debtorAccount": {
    "id": "...",
    "accountHolder": "John Doe",
    "iban": "IT60X0542811101000000123456"
  },
  "creditorAccount": {
    "id": "...",
    "accountHolder": "Jane Smith",
    "iban": "DE89370400440532013000"
  },
  "transactions": [],
  "createdAt": "2024-02-14T10:30:00Z"
}
```

#### Create Payment

```http
POST /api/payments
```

**Request Body:**
```json
{
  "paymentReference": "PAY-002",
  "type": 0,
  "amount": 1500.00,
  "currency": "EUR",
  "debtorAccountId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "creditorAccountId": "4gb96g75-6828-5673-c4gd-3d074g77bgb7",
  "description": "Invoice payment"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Payment created successfully",
  "data": {
    "id": "...",
    "paymentReference": "PAY-002",
    ...
  }
}
```

#### Process Payment

```http
POST /api/payments/{id}/process
```

**Parameters:**
- `id` (UUID) - Payment ID

**Response:**
```json
{
  "success": true,
  "message": "Payment processing started"
}
```

#### Get Payments by Status

```http
GET /api/payments/status/{status}
```

**Parameters:**
- `status` (int) - 0: Pending, 1: Processing, 2: Completed, 3: Failed, 4: Cancelled

#### Parse ISO 20022 File

```http
POST /api/payments/parse-iso20022
Content-Type: multipart/form-data
```

**Request:**
```
file: (binary)
```

**Response:**
```json
{
  "success": true,
  "payments": [
    {
      "instructionId": "INSTR-001",
      "endToEndId": "E2E-001",
      "amount": 1000.00,
      "currency": "EUR",
      "debtorName": "John Doe",
      "creditorName": "Jane Smith",
      "creditorIBAN": "DE89370400440532013000"
    }
  ]
}
```

---

### Bitcoin

#### Get Balance

```http
GET /api/bitcoin/balance/{address}
```

**Parameters:**
- `address` (string) - Bitcoin address

**Response:**
```json
{
  "address": "bc1qxy2kgdygjrsqtzq2n0yrf2493p83kkfjhx0wlh",
  "balance": 0.05,
  "currency": "BTC"
}
```

#### Send Bitcoin

```http
POST /api/bitcoin/send
```

**Request Body:**
```json
{
  "paymentId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "amount": 0.001,
  "fromAddress": "bc1qxy2kgdygjrsqtzq2n0yrf2493p83kkfjhx0wlh",
  "toAddress": "bc1q9s8kq9q8y3z4x5c6v7n8m9k0j1h2g3f4d5s6a7"
}
```

**Response:**
```json
{
  "success": true,
  "transactionHash": "a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6",
  "message": "Transaction broadcasted successfully"
}
```

#### Get Transaction Details

```http
GET /api/bitcoin/transaction/{txId}
```

**Parameters:**
- `txId` (string) - Transaction hash

**Response:**
```json
{
  "txId": "a1b2c3d4e5f6g7h8i9j0k1l2m3n4o5p6q7r8s9t0u1v2w3x4y5z6",
  "confirmations": 6,
  "amount": 0.001,
  "timestamp": "2024-02-14T10:30:00Z"
}
```

#### Validate Address

```http
GET /api/bitcoin/validate-address/{address}
```

**Parameters:**
- `address` (string) - Bitcoin address to validate

**Response:**
```json
{
  "valid": true,
  "type": "bech32"
}
```

---

### Accounts

#### Get All Accounts

```http
GET /api/accounts
```

#### Get Account by ID

```http
GET /api/accounts/{id}
```

#### Create Bank Account

```http
POST /api/accounts/bank
```

**Request Body:**
```json
{
  "accountHolder": "John Doe",
  "iban": "IT60X0542811101000000123456",
  "bic": "BOFAIT3XXXX",
  "bankName": "Bank of Italy"
}
```

#### Create Bitcoin Wallet

```http
POST /api/accounts/bitcoin
```

**Request Body:**
```json
{
  "accountHolder": "John Doe",
  "walletAddress": "bc1qxy2kgdygjrsqtzq2n0yrf2493p83kkfjhx0wlh"
}
```

---

## Enums

### PaymentType
- `0` - BankTransfer
- `1` - BitcoinTransfer
- `2` - Hybrid

### PaymentStatus
- `0` - Pending
- `1` - Processing
- `2` - Completed
- `3` - Failed
- `4` - Cancelled

### TransactionStatus
- `0` - Pending
- `1` - Broadcasting
- `2` - Confirmed
- `3` - Failed

### AccountType
- `0` - BankAccount
- `1` - BitcoinWallet

---

## Error Responses

```json
{
  "success": false,
  "message": "Error message",
  "errors": [
    "Detailed error 1",
    "Detailed error 2"
  ]
}
```

### Common Status Codes
- `200` - Success
- `201` - Created
- `400` - Bad Request
- `404` - Not Found
- `500` - Internal Server Error

---

## Rate Limiting

⚠️ Not currently implemented. Add before production use.

---

## Examples with cURL

### Create Payment
```bash
curl -X POST https://localhost:5001/api/payments \
  -H "Content-Type: application/json" \
  -d '{
    "paymentReference": "PAY-001",
    "type": 0,
    "amount": 1000,
    "currency": "EUR",
    "debtorAccountId": "account-id-1",
    "creditorAccountId": "account-id-2"
  }'
```

### Get Bitcoin Balance
```bash
curl https://localhost:5001/api/bitcoin/balance/bc1qxy2kgdygjrsqtzq2n0yrf2493p83kkfjhx0wlh
```

---

For more examples, check the **Swagger UI** at: `https://localhost:5001/swagger`
