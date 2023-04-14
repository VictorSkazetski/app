import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { ToastrModule } from 'ngx-toastr';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { RefreshTokenInterceptor } from 'src/services/refreshTokenInterceptor.service';
import { BikeSellComponent } from 'src/components/bike-sell/bike-sell.component';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { UploadPictureModule } from './upload-picture.module';

const routes: Routes = [
  {
    path: 'bike',
    component: BikeSellComponent,
  },
];

@NgModule({
  declarations: [BikeSellComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    DialogsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    NgxSliderModule,
    UploadPictureModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RefreshTokenInterceptor,
      multi: true,
    },
  ],
  exports: [RouterModule],
})
export class BikeSellModule {}
