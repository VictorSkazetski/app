import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from 'src/components/admin/admin.component';
import { AllBikeAdsComponent } from 'src/components/all-bike-ads/all-bike-ads.component';
import { BikeAdComponent } from 'src/components/bike-ad/bike-ad.component';
import { HomeComponent } from 'src/components/home/home.component';
import { ProfileComponent } from 'src/components/profile/profile.component';

const routes: Routes = [
  {
    path: 'account',
    loadChildren: () =>
      import('./authentication.module').then((m) => m.AuthenticationModule),
  },
  {
    path: 'profile',
    loadChildren: () => import('./profile.module').then((m) => m.ProfileModule),
    component: ProfileComponent,
  },
  {
    path: 'sell-your-bike',
    loadChildren: () =>
      import('./bike-sell.module').then((m) => m.BikeSellModule),
  },
  {
    path: 'bikes',
    component: AllBikeAdsComponent,
  },
  {
    path: 'bikes/bike/:id',
    component: BikeAdComponent,
  },
  {
    path: 'admin',
    component: AdminComponent,
  },
  {
    path: '',
    component: HomeComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
