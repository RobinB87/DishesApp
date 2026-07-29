import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { IAppState } from '../../core/store';
import { GetStatus } from '../../core/store/auth';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.css'],
})
export class StatusComponent implements OnInit {
  constructor(private store: Store<IAppState>) {}

  ngOnInit() {
    this.store.dispatch(new GetStatus());
  }
}
