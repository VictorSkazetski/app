import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs';
import { UserAccount } from 'src/models/UserAccount';
import { VerifyUserEmail } from 'src/models/VerifyUserEmail';
import { AccountService } from 'src/services/account.service';

@Component({
  selector: 'verify-email',
  templateUrl: './verify-email.component.html',
  styleUrls: ['./verify-email.component.sass'],
})
export class VerifyEmailComponent implements OnInit {
  verifiedUserEmail: string;

  constructor(private route: ActivatedRoute, private account: AccountService) {}

  ngOnInit() {
    const token: string = this.route.snapshot.queryParams['token'];
    const userEmail: string = this.route.snapshot.queryParams['email'];
    this.account
      .verifyEmail(this.route.snapshot.url[0].path, {
        userEmail: atob(userEmail),
        token: atob(token),
      } as VerifyUserEmail)
      .pipe(filter((user: VerifyUserEmail) => user !== null))
      .subscribe((user: VerifyUserEmail) => {
        this.verifiedUserEmail = user.userEmail;
      });
  }
}
