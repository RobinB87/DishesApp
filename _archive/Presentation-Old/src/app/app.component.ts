import { Component } from "@angular/core";
import { AuthService } from "./shared/services/auth.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"],
})
export class AppComponent {
  title = "My Dishes App";
  clientHeight: number;

  // constructor(private openIdConnectService: OpenIdConnectService) {
  constructor(private authService: AuthService) {
    if (localStorage.getItem("authToken")) {
      this.authService.setUserDetails();
    }

    this.clientHeight = window.innerHeight;
  }
}
