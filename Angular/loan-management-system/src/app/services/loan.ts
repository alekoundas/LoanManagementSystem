import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Loan } from '../models/loan';

@Injectable()
export class LoanService {
  // private apiUrl = API_URL.loans;
  private apiUrl = "https://localhost:44343/api/loans";


  constructor(private http: HttpClient) {}

  getLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.apiUrl);
  }

  getLoansByCustomerId(customerId: number): Observable<Loan[]> {
    return this.http.get<Loan[]>(`${this.apiUrl}/customer/${customerId}`);
  }

  getLoanById(id: number): Observable<Loan> {
    return this.http.get<Loan>(`${this.apiUrl}/${id}`);
  }
}
