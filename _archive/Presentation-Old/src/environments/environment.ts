// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

// export const environment = {
//   production: false,
//   apiUrl: 'https://mydishesappapi.azurewebsites.net/api'
// };

export const environment = {
  production: false,
  apiUrl: "https://localhost:44302/api",
  // openIdConnectSettings: {
  //   authority: "https://localhost:44398/",
  //   client_id: "dishesmanagementclient",
  //   redirect_uri: "https://localhost:4200/signin-oidc",
  //   scope: "openid profile roles dishesmanagementapi",
  //   response_type: "id_token token",
  //   post_logout_redirect_uri: "https://localhost:4200/",
  //   automaticSilentRenew: true,
  //   silent_redirect_uri: "https://localhost:4200/redirect-silentrenew",
  // },
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
