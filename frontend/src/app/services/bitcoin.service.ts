import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { CreateBitcoinTransactionRequest } from '../models/payment.models';

export interface BitcoinBalance {
  address: string;
  balance: number;
  currency: string;
}

export interface BitcoinTransaction {
  txId: string;
  confirmations: number;
  amount: number;
  timestamp: Date;
}

export interface SendBitcoinResponse {
  success: boolean;
  transactionHash?: string;
  message: string;
}

@Injectable({
  providedIn: 'root'
})
export class BitcoinService {
  private apiUrl = `${environment.apiUrl}/api/bitcoin`;

  constructor(private http: HttpClient) { }

  /**
   * Get wallet balance
   */
  getBalance(address: string): Observable<BitcoinBalance> {
    return this.http.get<BitcoinBalance>(`${this.apiUrl}/balance/${address}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Send Bitcoin transaction
   */
  sendTransaction(request: CreateBitcoinTransactionRequest): Observable<SendBitcoinResponse> {
    return this.http.post<SendBitcoinResponse>(`${this.apiUrl}/send`, request)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Get transaction details
   */
  getTransaction(txId: string): Observable<BitcoinTransaction> {
    return this.http.get<BitcoinTransaction>(`${this.apiUrl}/transaction/${txId}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Validate Bitcoin address
   */
  validateAddress(address: string): Observable<{ valid: boolean; type?: string }> {
    return this.http.get<{ valid: boolean; type?: string }>(
      `${this.apiUrl}/validate-address/${address}`
    ).pipe(
      catchError(this.handleError)
    );
  }

  /**
   * Error handler
   */
  private handleError(error: any): Observable<never> {
    console.error('An error occurred:', error);
    return throwError(() => new Error(error.message || 'Server error'));
  }
}
