import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';
import { UserAccount } from 'src/models/UserAccount';
import { UserTokens } from 'src/models/UserTokens';
import { VerifyUserEmail } from 'src/models/VerifyUserEmail';
import { HttpService } from './http.service';
import { LocalStorageService } from './localStorage.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  isUserLogged = new BehaviorSubject(null);

  constructor(
    private httpService: HttpService,
    private localStorage: LocalStorageService,
    private router: Router
  ) {}

  logoutUser(refreshUserToken: string) {
    this.isUserLogged.next(false);
    this.logout(refreshUserToken);
  }

  loginUser() {
    this.isUserLogged.next(true);
  }

  getUserTokens(path: string, userData: UserAccount): Observable<UserTokens> {
    return this.httpService.post(path, userData, false);
  }

  registerUser(path: string, userData: UserAccount): Observable<UserAccount> {
    return this.httpService.post(path, userData);
  }

  verifyEmail(
    path: string,
    token: VerifyUserEmail
  ): Observable<VerifyUserEmail> {
    return this.httpService.post(path, token);
  }

  refreshToken(refreshUserToken: string): Observable<UserTokens> {
    return this.httpService.post(
      'refresh',
      JSON.stringify({ refreshToken: refreshUserToken }),
      false
    );
  }

  logout(refreshUserToken: string) {
    this.httpService
      .post('logout', { "refreshToken": refreshUserToken }, false)
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      )
      .subscribe((responce: any) => {
        if (responce === null) {
          this.localStorage.clearLocalStorage();
          this.router.navigateByUrl('');
        }
      });
  }
}
