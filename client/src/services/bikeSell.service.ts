import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SellBike } from 'src/models/SellBike';
import { HttpService } from './http.service';

@Injectable({
  providedIn: 'root',
})
export class BikeSellService {
  constructor(private httpService: HttpService) {}

  postBikeAd(path: string, bikeData: FormData): Observable<SellBike> {
    return this.httpService.postFormData(path, bikeData);
  }

  getYourBikes(path: string): Observable<SellBike[]> {
    return this.httpService.get(path, false);
  }

  deleteYourBike(path: string): Observable<any> {
    return this.httpService.delete(path);
  }
}
