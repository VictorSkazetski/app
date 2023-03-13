import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { combineLatest } from 'rxjs';
import { AccountService } from 'src/services/account.service';
import { BaseAccountComponent } from '../account/base-account.component';

@Component({
  selector: 'registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.sass'],
})
export class RegistrationComponent extends BaseAccountComponent {
  constructor(account: AccountService, route: ActivatedRoute, router: Router) {
    super(account, route, router);
  }

  ngOnInit() {
    this.buildForm();
  }

  buildForm() {
    this.form = new FormGroup({
      UserEmail: new FormControl(null, [
        Validators.required,
        Validators.email,
        Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$'),
      ]),
      UserPassword: new FormControl(null, [
        Validators.required,
        Validators.minLength(6),
      ]),
      UserConfirmPassword: new FormControl(null, [Validators.required]),
    });
    this.formSubmited();
    this.passwordMatched();
  }

  passwordMatched(): void {
    combineLatest([
      this.formControls['UserPassword'].valueChanges,
      this.formControls['UserConfirmPassword'].valueChanges,
    ]).subscribe((values: [string, string]) => {
      if (
        this.formControls['UserPassword'].touched ||
        this.formControls['UserConfirmPassword'].dirty
      ) {
        this.setMatchedError(values[0], values[1]);
      }
      this.setNullError(values[0], values[1]);
      this.setRequiredError(values[1]);
    });
  }

  formSubmited(): void {
    this.form.valueChanges.subscribe((_) => {
      if (this.submitted) {
        this.submitted = false;
      }
    });
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    this.registration();
  }

  isPasswordsMatchedWithMatchPasswordError(
    password: string,
    confirmPassord: string
  ): boolean {
    return (
      password === confirmPassord &&
      this.formControls['UserConfirmPassword'].hasError('matchPassword')
    );
  }

  isConfirmPasswordEmptyWithMatchPasswordError(
    confirmPassord: string
  ): boolean {
    return (
      confirmPassord === '' &&
      this.formControls['UserConfirmPassword'].hasError('matchPassword')
    );
  }

  setRequiredError(confirmPassord: string): void {
    if (this.isConfirmPasswordEmptyWithMatchPasswordError(confirmPassord)) {
      this.formControls['UserConfirmPassword'].setErrors({ required: true });
    }
  }

  setNullError(password: string, confirmPassord: string): void {
    if (
      this.isPasswordsMatchedWithMatchPasswordError(password, confirmPassord)
    ) {
      this.formControls['UserConfirmPassword'].setErrors(null);
    }
  }

  setMatchedError(password: string, confirmPassord: string): void {
    if (password !== confirmPassord) {
      this.formControls['UserConfirmPassword'].setErrors({
        matchPassword: true,
      });
    }
  }
}
