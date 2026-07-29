import { Component } from "@angular/core";
import { UserRole } from "../shared/models/user-role.enum";
import { User } from "../shared/models/user.model";
import { AuthService } from "../shared/services/auth.service";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent {
  isExpanded = false;
  userDataSubscription: any;
  userData = new User();
  userRole = UserRole;

  constructor(private authService: AuthService) {
    this.userDataSubscription = this.authService.userData
      .asObservable()
      .subscribe((data) => {
        this.userData = data;
      });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authService.logout();
  }
}
