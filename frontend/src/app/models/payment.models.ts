// Enums
export enum PaymentStatus {
  Pending = 0,
  Processing = 1,
  Completed = 2,
  Failed = 3,
  Cancelled = 4
}

export enum PaymentType {
  BankTransfer = 0,
  BitcoinTransfer = 1,
  Hybrid = 2
}

export enum TransactionType {
  ISO20022 = 0,
  Bitcoin = 1
}

export enum TransactionStatus {
  Pending = 0,
  Broadcasting = 1,
  Confirmed = 2,
  Failed = 3
}

export enum AccountType {
  BankAccount = 0,
  BitcoinWallet = 1
}

// Models
export interface Money {
  amount: number;
  currency: string;
}

export interface Account {
  id: string;
  type: AccountType;
  accountHolder: string;
  iban?: string;
  bic?: string;
  bankName?: string;
  walletAddress?: string;
  createdAt: Date;
  isActive: boolean;
}

export interface Transaction {
  id: string;
  paymentId: string;
  transactionHash: string;
  type: TransactionType;
  amount: Money;
  status: TransactionStatus;
  createdAt: Date;
  confirmedAt?: Date;
  fromAddress?: string;
  toAddress?: string;
  confirmations?: number;
  instructionId?: string;
  endToEndId?: string;
}

export interface Payment {
  id: string;
  paymentReference: string;
  type: PaymentType;
  amount: Money;
  status: PaymentStatus;
  debtorAccount: Account;
  creditorAccount: Account;
  createdAt: Date;
  processedAt?: Date;
  description?: string;
  transactions: Transaction[];
}

// DTOs for API requests
export interface CreatePaymentRequest {
  paymentReference: string;
  type: PaymentType;
  amount: number;
  currency: string;
  debtorAccountId: string;
  creditorAccountId: string;
  description?: string;
}

export interface CreateBitcoinTransactionRequest {
  paymentId: string;
  amount: number;
  fromAddress: string;
  toAddress: string;
}

export interface PaymentResponse {
  success: boolean;
  message: string;
  data?: Payment;
  errors?: string[];
}
