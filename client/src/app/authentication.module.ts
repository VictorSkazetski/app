import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { LoginComponent } from 'src/components/login/login.component';
import { RegistrationComponent } from 'src/components/registration/registration.component';
import { ToastrModule } from 'ngx-toastr';
import { EmailConfirmComponent } from 'src/components/email-confirm/email-confirm.component';
import { VerifyEmailComponent } from 'src/components/verify-email/verify-email.component';

const routes: Routes = [
  {
    path: 'registration',
    component: RegistrationComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'verify',
    component: VerifyEmailComponent,
  },
];

@NgModule({
  declarations: [
    LoginComponent,
    RegistrationComponent,
    EmailConfirmComponent,
    VerifyEmailComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    DialogsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
  ],
  exports: [RouterModule],
})
export class AuthenticationModule {}
