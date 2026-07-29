// import { Component, OnInit } from "@angular/core";
// import { Router } from "@angular/router";
// import { environment } from "src/environments/environment";
// import { OpenIdConnectService } from "../shared/open-id-connect.service";

// @Component({
//   selector: "app-signin-oidc",
//   templateUrl: "./signin-oidc.component.html",
//   styleUrls: ["./signin-oidc.component.css"],
// })
// export class SigninOidcComponent implements OnInit {
//   constructor(
//     private openIdConnectService: OpenIdConnectService,
//     private router: Router
//   ) {}

//   ngOnInit() {
//     this.openIdConnectService.userLoaded$.subscribe((userLoaded) => {
//       if (userLoaded) {
//         this.router.navigate(["./"]);
//       } else {
//         if (!environment.production) {
//           console.log("An error happened: user was not loaded.");
//         }
//       }
//     });

//     this.openIdConnectService.handleCallBack();
//   }
// }
