import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShareImgService {
  bSubject = new BehaviorSubject(null);

  shareImgPath(imgPath: string) {
    this.bSubject.next(imgPath);
  }

  getImgPath(): Observable<string> {
    return this.bSubject.asObservable();
  }
}
