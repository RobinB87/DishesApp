import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { IAppState } from './core/store/app.state';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'My dishes';

  constructor(private store: Store<IAppState>) {}
}
