import { Injectable } from '@angular/core';
import { UserTokens } from 'src/models/UserTokens';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  saveTokens(key: string, value: UserTokens) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  getTokens(key: string): UserTokens {
    return JSON.parse(localStorage.getItem(key)) as UserTokens;
  }

  removeTokens(key: string) {
    localStorage.removeItem(key);
  }

  clearLocalStorage() {
    localStorage.clear();
  }

  saveRefreshTokens(key: string, value: UserTokens): void {
    this.removeTokens(key);
    this.saveTokens(key, value);
  }
}
