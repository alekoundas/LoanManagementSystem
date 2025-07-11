import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { LoanApplication } from '../models/loan-application';

@Injectable()
export class LoanApplicationService {
  private apiUrl = API_URL.loanApplications;

  constructor(private http: HttpClient) {}

  getApplications(filters: any): Observable<LoanApplication[]> {
    return this.http
      .get<LoanApplication[]>(this.apiUrl, { params: filters })
      .pipe(retry(3), catchError(this.handleError));
  }

  submitApplication(application: LoanApplication): Observable<LoanApplication> {
    return this.http
      .post<LoanApplication>(this.apiUrl, application)
      .pipe(retry(3), catchError(this.handleError));
  }

  deleteApplication(id: number): Observable<void> {
    return this.http
      .delete<void>(`${this.apiUrl}/${id}`)
      .pipe(retry(3), catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'An error occurred';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Client-side error: ${error.error.message}`;
    } else {
      errorMessage = `Server-side error: ${error.status} - ${error.message}`;
    }
    console.error(errorMessage);
    return throwError(() => new Error(errorMessage));
  }
}
