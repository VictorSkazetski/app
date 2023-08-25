import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { UsersActions } from 'src/models/UsersActions';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  constructor(private httpService: HttpService) {}

  getUsersActions(): Observable<UsersActions> {
    return this.httpService.get('view/users/actions', false);
  }

  refreshUsersActionsData(): Observable<UsersActions> {
    return this.httpService.get('view/users/actions', true);
  }
}
