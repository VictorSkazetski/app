import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AppToastrService {
  constructor(private toastr: ToastrService) {}

  showError(message: string, title: string = 'Ошибка'): void {
    this.toastr.error(message, title, { timeOut: 2500 });
  }

  showSuccess(message: string, title: string = ''): void {
    this.toastr.success(message, title, { timeOut: 2500 });
  }
}
