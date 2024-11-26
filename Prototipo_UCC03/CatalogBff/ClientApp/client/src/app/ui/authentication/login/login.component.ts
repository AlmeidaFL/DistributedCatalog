import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../core/services/authentication-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'login',
  standalone: true,
  templateUrl: './login.component.html',
  imports: [FormsModule, RouterLink]
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  async login() {
    if (this.email && this.password) {
      try{
        await this.authService.login(this.email, this.password);
        this.router.navigate(['/']);
      }
      catch(error) {
        console.error(error)
      }
    }
  }
}
