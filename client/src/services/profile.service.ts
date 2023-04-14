import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UserProfile } from 'src/models/UserProfile';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  constructor(private httpService: HttpService) {}

  getUserProfile(path: string): Observable<UserProfile> {
    return this.httpService.get(path, false);
  }

  saveProfile(path: string, userData: FormData): Observable<UserProfile> {
    return this.httpService.put(path, userData);
  }
}
