import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProfilePickPictureService {
  imgs: { [key: number]: string };
  displayImgOnContainer: number = 5;

  constructor() {
    this.setPathToImgs();
  }

  getImgPathByKey(key: number): string {
    return this.imgs[key];
  }

  setPathToImgs() {
    this.imgs = {
      0: './assets/img/avatar-icon/1.svg',
      1: './assets/img/avatar-icon/2.svg',
      2: './assets/img/avatar-icon/3.svg',
      3: './assets/img/avatar-icon/4.svg',
      4: './assets/img/avatar-icon/5.svg',
      5: './assets/img/avatar-icon/6.svg',
      6: './assets/img/avatar-icon/7.svg',
      7: './assets/img/avatar-icon/8.svg',
      8: './assets/img/avatar-icon/9.svg',
      9: './assets/img/avatar-icon/10.svg',
    };
  }

  // convertUserSelectedImgToBase64(
  //   imgNumber: number
  // ): Observable<string | ArrayBuffer> {
  //   const subject = new Subject<string | ArrayBuffer>();
  //   return this.httpService.getImgFromAssets(this.imgs[imgNumber]).pipe(
  //     switchMap((img) => {
  //       const reader = new FileReader();
  //       reader.onloadend = () => {
  //         subject.next(reader.result);
  //         subject.complete();
  //       };
  //       reader.readAsDataURL(img);

  //       return subject.asObservable();
  //     })
  //   );
  //   // .subscribe((img) => {
  //   //   const sub = new Subject<number>();
  //   //   const reader = new FileReader();
  //   //   reader.onloadend = () => {
  //   //     // sub.next(reader.result);
  //   //   };
  //   //   reader.readAsDataURL(img);
  //   // });
  // }
}
