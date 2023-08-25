import { NgModule } from '@angular/core';
import { HomeComponent } from 'src/components/home/home.component';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthenticationModule } from './authentication.module';
import { HttpClientModule } from '@angular/common/http';
import { HeaderMenuComponent } from 'src/components/header-menu/header-menu.component';
import { DebounceClickDirective } from 'src/directives/debounce-click.directive';
import { AllBikeAdsComponent } from 'src/components/all-bike-ads/all-bike-ads.component';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import { BikeAdComponent } from 'src/components/bike-ad/bike-ad.component';
import { AdminComponent } from 'src/components/admin/admin.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderMenuComponent,
    AllBikeAdsComponent,
    DebounceClickDirective,
    BikeAdComponent,
    AdminComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    AuthenticationModule,
    HttpClientModule,
    MatTableModule,
    MatInputModule,
    MatSelectModule,
    MatCardModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
