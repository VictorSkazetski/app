import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserAccount } from 'src/models/UserAccount';
import { VerifyUserEmail } from 'src/models/VerifyUserEmail';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  constructor(protected httpService: HttpService) {}

  checkAccount(url: string, userData: UserAccount): Observable<UserAccount> {
    return this.httpService.post(url, userData);
  }

  registration(path: string, userData: UserAccount): Observable<UserAccount> {
    return this.httpService.post(path, userData);
  }

  verifyEmail(
    path: string,
    token: VerifyUserEmail
  ): Observable<VerifyUserEmail> {
    return this.httpService.post(path, token);
  }
}
