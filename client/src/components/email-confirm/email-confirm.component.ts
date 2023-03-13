import { Component, Input } from '@angular/core';

@Component({
  selector: 'email-confirm',
  templateUrl: './email-confirm.component.html',
  styleUrls: ['./email-confirm.component.sass'],
})
export class EmailConfirmComponent {
  @Input() emailUrl: string;

  constructor() {}
}
