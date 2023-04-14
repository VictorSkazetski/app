import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BikeSellPickPictureService {
  imgs: { [key: string]: string };
  displayImgOnContainer: number = 5;

  constructor() {
    this.setPathToImgs();
  }

  getImgPathByKey(key: string): string {
    return this.imgs[key];
  }

  setPathToImgs() {
    this.imgs = {
      city: './assets/img/bikes/city.svg',
      gravel: './assets/img/bikes/gravel.svg',
      hybrid: './assets/img/bikes/hybrid.svg',
      mountain: './assets/img/bikes/mountain.svg',
      road: './assets/img/bikes/road.svg',
    };
  }
}
