import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import {
  Payment,
  CreatePaymentRequest,
  PaymentResponse,
  PaymentStatus
} from '../models/payment.models';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private apiUrl = `${environment.apiUrl}/api/payments`;

  constructor(private http: HttpClient) { }

  /**
   * Get all payments
   */
  getAllPayments(): Observable<Payment[]> {
    return this.http.get<Payment[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Get payment by ID
   */
  getPaymentById(id: string): Observable<Payment> {
    return this.http.get<Payment>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Get payments by status
   */
  getPaymentsByStatus(status: PaymentStatus): Observable<Payment[]> {
    return this.http.get<Payment[]>(`${this.apiUrl}/status/${status}`)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Create a new payment
   */
  createPayment(request: CreatePaymentRequest): Observable<PaymentResponse> {
    return this.http.post<PaymentResponse>(this.apiUrl, request)
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Process an existing payment
   */
  processPayment(paymentId: string): Observable<PaymentResponse> {
    return this.http.post<PaymentResponse>(`${this.apiUrl}/${paymentId}/process`, {})
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Cancel a payment
   */
  cancelPayment(paymentId: string): Observable<PaymentResponse> {
    return this.http.post<PaymentResponse>(`${this.apiUrl}/${paymentId}/cancel`, {})
      .pipe(
        catchError(this.handleError)
      );
  }

  /**
   * Parse ISO 20022 file
   */
  parseIso20022File(file: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post(`${this.apiUrl}/parse-iso20022`, formData)
      .pipe(
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
