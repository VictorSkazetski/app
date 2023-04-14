import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileSettingsComponent } from 'src/components/profile-settings/profile-settings.component';
import { ProfilePickPictureComponent } from 'src/components/profile-pick-picture/profile-pick-picture.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {
  MatNativeDateModule,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
} from '@angular/material/core';
import { UploadPictureModule } from './upload-picture.module';
import { YourBikesComponent } from 'src/components/your-bikes/your-bikes.component';
import { ProfileComponent } from 'src/components/profile/profile.component';

const routes: Routes = [
  {
    path: 'your-bikes',
    component: YourBikesComponent,
  },
  {
    path: 'profile-settings',
    component: ProfileSettingsComponent,
  },
];

@NgModule({
  declarations: [
    ProfileSettingsComponent,
    ProfilePickPictureComponent,
    ProfileComponent,
    YourBikesComponent,
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    UploadPictureModule,
  ],
  exports: [RouterModule],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'ru-BY' },
    {
      provide: MAT_DATE_FORMATS,
      useValue: { display: { dateInput: 'DD-MM-YYYY' } },
    },
  ],
})
export class ProfileModule {}
