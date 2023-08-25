import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/services/admin.service';
import { UsersActions } from 'src/models/UsersActions';
import { UserAction } from 'src/models/UserAction';

@Component({
  selector: 'admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass'],
})
export class AdminComponent implements OnInit {
  usersActions: UserAction[];
  columnsToDisplay: string[] = ['action', 'userEmail', 'dateTime'];
  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.adminService
      .getUsersActions()
      .subscribe((usersActionsData: UsersActions) => {
        this.usersActions = usersActionsData.usersActions;
      });
  }

  refreshUsersActionsData(): void {
    this.adminService
      .refreshUsersActionsData()
      .subscribe((usersActionsData: UsersActions) => {
        this.usersActions = usersActionsData.usersActions;
      });
  }
}
