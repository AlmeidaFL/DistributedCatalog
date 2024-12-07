import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/authentication-service';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [],
  templateUrl: './user-management.component.html'
})
export class UserManagementComponent {
  activeSection: string = 'pedidos';

  constructor(private authService: AuthService){

  }

  get userName(): string{
    return this.authService.user.name
  }

  showSection(section: string): void {
    this.activeSection = section;
  }
}