import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, EMPTY, Observable, switchMap, throwError } from 'rxjs';
import { UserTokens } from 'src/models/UserTokens';
import { AccountService } from './account.service';
import { LocalStorageService } from './localStorage.service';

@Injectable({
  providedIn: 'root',
})
export class RefreshTokenInterceptor implements HttpInterceptor {
  constructor(
    private localStorage: LocalStorageService,
    private account: AccountService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (
      !request.url.includes('login') &&
      !request.url.includes('registration') &&
      !request.url.includes('verify') &&
      request.url.split('/')[3] !== 'bikes'
    ) {
      request = this.addAuthenticationToken(request);
    }

    return next.handle(request).pipe(
      catchError((error) => {
        console.log('HANDLE');
        if (
          error instanceof HttpErrorResponse &&
          error.status === 401 &&
          !request.url.includes('login') &&
          !request.url.includes('refresh')
        ) {
          return this.account
            .refreshToken(this.localStorage.getTokens('tokens').refreshToken)
            .pipe(
              switchMap((userTokens: UserTokens) => {
                this.localStorage.saveRefreshTokens('tokens', userTokens);

                return next.handle(this.addAuthenticationToken(request));
              }),
              catchError((error) => {
                this.logout();

                return throwError(() => error);
              })
            );
        }

        return throwError(() => error);
      })
    );
  }

  addAuthenticationToken(request: HttpRequest<any>): HttpRequest<any> {
    const accessToken = this.localStorage.getTokens('tokens').accessToken;
    if (!accessToken) {
      return request;
    }

    if (request.url.includes('logout')) {
      return request.clone({
        headers: request.headers.set(
          'Authorization',
          `Bearer ${this.localStorage.getTokens('tokens').accessToken}`
        ),
        body: JSON.stringify({
          refreshToken: this.localStorage.getTokens('tokens').refreshToken,
        }),
      });
    }

    return request.clone({
      headers: request.headers.set(
        'Authorization',
        `Bearer ${this.localStorage.getTokens('tokens').accessToken}`
      ),
    });
  }

  logout(): void {
    this.localStorage.clearLocalStorage();
    this.router.navigateByUrl('/login');
  }
}
