import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass'],
})
export class HomeComponent {
  opened = false;

  open(): void {
    this.opened = true;
  }

  close(status: string): void {
    console.log(`Dialog result: ${status}`);
    this.opened = false;
  }
}
