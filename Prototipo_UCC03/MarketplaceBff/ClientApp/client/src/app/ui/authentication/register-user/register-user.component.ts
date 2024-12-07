import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/authentication-service';
import { User } from '../../../../core/domain/user';
import { Router } from '@angular/router';

@Component({
  selector: 'register',
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './register-user.component.html',
})
export class RegisterUserComponent implements OnInit {
  registerForm!: FormGroup;

  constructor(private formBuilder: FormBuilder, private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(10)]],
      phone: ['', [Validators.required, Validators.pattern(/^\(\d{2}\) \d{4,5}-\d{4}$/)]],
      cnpj: ['', [Validators.required, Validators.pattern(/^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$/)]],
      isVendor: [false, [Validators.required]]
    });
  }

  async onSubmit() {
    if (this.registerForm.valid) {
      const formValues = this.registerForm.value;
      const newUser: User = {
        ...formValues,
        role: formValues.isVendor ? 'Vendor' : 'Customer'
      };
  
      try {
        await this.authService.register(newUser);
        this.router.navigate(['/login']);
      } catch (error) {
        console.error(error);
      }
    }
  }
  
}