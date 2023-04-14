import { Component } from '@angular/core';
import { filter } from 'rxjs';
import { AccountService } from 'src/services/account.service';
import { HttpService } from 'src/services/http.service';
import { LocalStorageService } from 'src/services/localStorage.service';

@Component({
  selector: 'header-menu',
  templateUrl: './header-menu.component.html',
  styleUrls: ['./header-menu.component.sass'],
})
export class HeaderMenuComponent {
  isUserLogin: boolean;

  constructor(
    private localStorage: LocalStorageService,
    private httpservice: HttpService,
    private account: AccountService
  ) {}

  ngOnInit() {
    this.isUserLogin = this.localStorage.getTokens('tokens') ? true : false;
    console.log('HEADER', this.isUserLogin);
    this.account.isUserLogged
      .pipe(filter((status) => status !== null))
      .subscribe((status) => {
        console.log('HEADER after subscribe', this.isUserLogin);
        this.isUserLogin = status;
      });
  }

  // clickEvent(): void {
  //   this.httpservice.get('Weatherforecast').subscribe((resp) => {
  //     console.log('resp', resp);
  //   });
  // }            // Тест refresh JWT
}
