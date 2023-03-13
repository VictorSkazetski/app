import { Component, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, Observable } from 'rxjs';
import { UserAccount } from 'src/models/UserAccount';
import { AccountService } from 'src/services/account.service';

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
    private router: Router
  ) {}

  get formControls() {
    return this.form.controls;
  }

  login(): Observable<UserAccount> {
    return null;
  }

  registration(): void {
    this.account
      .registration(this.route.snapshot.url[0].path, this.form.value)
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
