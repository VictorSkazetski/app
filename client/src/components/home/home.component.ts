import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.sass'],
})
export class HomeComponent implements OnInit {
  constructor(private account: AccountService, private router: Router) {}

  ngOnInit(): void {
    if (this.account.isUserAdmin()) {
      this.router.navigateByUrl('/admin');
    }
  }
}
