import { Component } from '@angular/core';
import { User } from '../../../core/models/auth';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css'],
})
export class SignUpComponent {
  user: User = new User();

  onSubmit(): void {
    console.log(this.user);
  }
}
