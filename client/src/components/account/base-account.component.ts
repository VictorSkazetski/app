import { Component, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { filter } from 'rxjs';
import { UserAccount } from 'src/models/UserAccount';
import { UserTokens } from 'src/models/UserTokens';
import { AccountService } from 'src/services/account.service';
import { LocalStorageService } from 'src/services/localStorage.service';

@Component({
  template: '',
})
export abstract class BaseAccountComponent {
  protected form: FormGroup;
  protected submitted: boolean = false;
  isRegistrationStep: boolean = true;
  @Output() emailUrl: string = '';

  constructor(
    private account: AccountService,
    private route: ActivatedRoute,
    private router: Router,
    private localStorage: LocalStorageService
  ) {}

  get formControls() {
    return this.form.controls;
  }

  login(): void {
    this.account
      .getUserTokens(this.route.snapshot.url[0].path, this.form.value)
      .pipe(filter((userTokens) => userTokens !== null))
      .subscribe((userTokens: UserTokens) => {
        this.account.loginUser();
        this.localStorage.saveTokens('tokens', userTokens);
        this.router.navigateByUrl('');
      });
  }

  registration(): void {
    this.account
      .registerUser(this.route.snapshot.url[0].path, this.form.value)
      .pipe(filter((user) => user !== null))
      .subscribe((user: UserAccount) => {
        this.isRegistrationStep = false;
        this.emailUrl = user.emailHost;
      });
  }

  close(): void {
    this.router.navigateByUrl('');
  }
}
