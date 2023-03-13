import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, Observable, of, tap, throwError } from 'rxjs';
import { AppToastrService } from './appToastr.service';
import { CustomHttpErrorResponse } from 'src/models/CustomHttpErrorResponse';
import { HttpCodes } from 'src/models/HttpCodes';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  host = 'https://localhost:7154/';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(
    private httpClient: HttpClient,
    private appToastrService: AppToastrService
  ) {}

  post<T>(path: string, data: T): Observable<T> {
    return this.httpClient
      .post<T>(this.host + path, data, this.httpOptions)
      .pipe(
        tap(() => this.showSuccessMessage('Успешно')),
        catchError((err) => this.handleServerError(err))
      );
  }

  // get<T>(path: string): Observable<T> {
  //   return this.httpClient
  //     .get<T>(this.host + path)
  //     .pipe();
  // }

  private showSuccessMessage(message: string) {
    this.appToastrService.showSuccess(message);
  }

  private handleServerError(error: CustomHttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      this.appToastrService.showError(
        'An error occurred: ' + error.error.message
      );

      return of(null);
    }

    if (error.status === HttpCodes.Conflict) {
      this.appToastrService.showError(
        (error.error as CustomHttpErrorResponse).Message
      );

      return of(null);
    }

    if (error.status === HttpCodes.BadRequest) {
      this.appToastrService.showError(
        (error.error as CustomHttpErrorResponse).Message
      );

      return of(null);
    }

    return throwError(() => error);
  }
}
