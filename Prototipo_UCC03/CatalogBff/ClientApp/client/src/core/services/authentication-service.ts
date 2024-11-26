import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Role, User } from "../domain/user"
import { apiUrl } from './bff-config';
import { DeliveryAddress, PaymentCard } from '../../app/ui/shopping/checkout/checkout.component';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = apiUrl
  private isAuthenticated: boolean = false;
  userNulable: User | null | undefined

  constructor(private httpClient: HttpClient) {
    this.isAuthenticated = !!localStorage.getItem('authToken');
  }

  async login(email: string, password: string) {
    const userLogin = {
      email:email,
      password:password
    };
  
    this.userNulable = await this.httpClient.post<User>(`${this.baseUrl}/auth/login`, userLogin).pipe(
      map(response => Object.assign(new User(), response))
    ).toPromise();
    this.isAuthenticated = true;
  }

  async register(user: User) {
    this.userNulable = await this.httpClient.post<User>(`${this.baseUrl}/auth/register`, user).pipe(
      map(response => Object.assign(new User(), response))
    ).toPromise();
  }

  addCard(card: PaymentCard): Observable<PaymentCard[]> {
    return this.httpClient.put<PaymentCard[]>(`${this.baseUrl}/08dd1268-cfb8-4ede-894c-777840daec2c/cards`, card);
  }

  addAddress(address: DeliveryAddress): Observable<DeliveryAddress[]> {
    return this.httpClient.put<DeliveryAddress[]>(`${this.baseUrl}/08dd1268-cfb8-4ede-894c-777840daec2c/addresses`, address);
  }

  getAddresses(): Observable<DeliveryAddress[]> {
    return this.httpClient.get<DeliveryAddress[]>(`${this.baseUrl}/08dd1268-cfb8-4ede-894c-777840daec2c/addresses`);
  }

  getCards(): Observable<PaymentCard[]> {
    return this.httpClient.get<PaymentCard[]>(`${this.baseUrl}/08dd1268-cfb8-4ede-894c-777840daec2c/cards`);
  }
  
  
  get user(): User{
    try{
      return this.userNulable!
    }
    catch (error){
      console.error(error)
      return undefined!;
    }
  }

  logout() {
    localStorage.removeItem('authToken');
    this.isAuthenticated = false;
  }

  isUserAuthenticated(): boolean {
    return this.isAuthenticated;
  }

  isVendor(): boolean {
    return this.user?.role == Role.Vendor
  }

  get userId(): string | undefined{
    return this.user?.id;
  }
}

class UserLogin {
  email: string
  password: string
}

