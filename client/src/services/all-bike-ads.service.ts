import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BikeAd } from "src/models/BikeAd";
import { HttpService } from "./http.service";

@Injectable({
    providedIn: 'root',
  })
  export class AllBikeAdsService {
    constructor(private httpService: HttpService) {}
  
    getBikesAds(path: string): Observable<BikeAd[]> {
      return this.httpService.get(path, false);
    }
}