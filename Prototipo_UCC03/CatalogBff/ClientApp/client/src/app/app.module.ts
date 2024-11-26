// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RegisterUserComponent } from './ui/authentication/register-user/register-user.component';
import { TopBarComponent } from './ui/top-bar/top-bar/top-bar.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

@NgModule({
  imports: [BrowserModule, FormsModule, TopBarComponent, ReactiveFormsModule, RegisterUserComponent],
  providers: [],
})
export class AppModule {}
