import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable } from "rxjs";
import { UserRole } from "../shared/models/user-role.enum";
import { User } from "../shared/models/user.model";
import { AuthService } from "../shared/services/auth.service";

@Injectable({
  providedIn: "root",
})
export class AuthGuard implements CanActivate {
  userDataSubscription: any;
  userData = new User();

  constructor(private router: Router, private authService: AuthService) {
    this.userDataSubscription = this.authService.userData
      .asObservable()
      .subscribe((data) => {
        this.userData = data;
      });
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    if (this.userData.role === UserRole.User) {
      return true;
    }

    this.router.navigate(["/login"], { queryParams: { returnUrl: state.url } });
    return false;
  }
}
