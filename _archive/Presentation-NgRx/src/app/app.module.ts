import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EffectsModule } from '@ngrx/effects';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LogInComponent } from './components/auth/log-in';
import { SignUpComponent } from './components/auth/sign-up';
import { DishAddComponent } from './components/dish/dish-add';
import { DishDetailComponent } from './components/dish/dish-detail';
import { DishListComponent } from './components/dish/dish-list';
import { StatusComponent } from './components/status';
import { DishService } from './core/services';
import {
  AuthGuardService as AuthGuard,
  AuthService,
  ErrorInterceptor,
  TokenInterceptor,
} from './core/services/auth';
import { appReducers } from './core/store';
import { AuthEffects } from './core/store/auth';
import { DishEffects } from './core/store/dish';

@NgModule({
  declarations: [
    AppComponent,
    DishListComponent,
    DishAddComponent,
    DishDetailComponent,
    SignUpComponent,
    LogInComponent,
    StatusComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot([AuthEffects, DishEffects]),
    StoreRouterConnectingModule.forRoot({ stateKey: 'router' }),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    AppRoutingModule,
    NgbModule,
  ],
  providers: [
    AuthGuard,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    DishService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
