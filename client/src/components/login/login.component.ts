import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from 'src/services/account.service';
import { BaseAccountComponent } from '../account/base-account.component';

@Component({
  selector: 'login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
})
export class LoginComponent extends BaseAccountComponent {
  constructor(account: AccountService, route: ActivatedRoute, router: Router) {
    super(account, route, router);
  }

  ngOnInit() {
    this.buildForm();
  }

  public buildForm() {
    this.form = new FormGroup({
      UserEmail: new FormControl(null),
      UserPassword: new FormControl(null),
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }
  }
}
