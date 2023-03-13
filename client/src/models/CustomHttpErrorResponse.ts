import type { HttpErrorResponse } from '@angular/common/http';

export declare class CustomHttpErrorResponse extends HttpErrorResponse {
  Message: string;
  SupportMessage: string;
}
