import { Component, OnInit } from '@angular/core';
import { filter } from 'rxjs';
import { AccountService } from 'src/services/account.service';
import { LocalStorageService } from 'src/services/localStorage.service';
import { ShareImgService } from 'src/services/shareImg.service';

@Component({
  selector: 'profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.sass'],
})
export class ProfileComponent implements OnInit {
  userAvatar: string;

  constructor(
    private account: AccountService,
    private localStorage: LocalStorageService,
    private shareImg: ShareImgService
  ) {}

  ngOnInit(): void {
    this.shareImg
      .getImgPath()
      .pipe(filter((path) => path !== null))
      .subscribe((path) => {
        this.userAvatar = path;
      });
  }

  logout(): void {
    this.account.logoutUser(this.localStorage.getTokens('tokens').refreshToken);
  }
}
