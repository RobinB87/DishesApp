import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { User } from '../../../core/models/auth';
import { IAppState, selectAuthState } from '../../../core/store';
import { LogIn } from '../../../core/store/auth';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
})
export class LogInComponent implements OnInit {
  user: User = new User();
  getState: Observable<any>;
  errorMessage: string | null;

  constructor(private store: Store<IAppState>) {
    this.getState = this.store.select(selectAuthState);
  }

  ngOnInit() {
    this.getState.subscribe((state) => {
      this.errorMessage = state.errorMessage;
    });
  }

  onSubmit(): void {
    const payload = {
      email: this.user.email,
      password: this.user.password,
    };
    this.store.dispatch(new LogIn(payload));
  }
}
