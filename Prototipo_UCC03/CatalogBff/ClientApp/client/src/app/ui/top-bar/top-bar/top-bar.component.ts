import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../../../core/services/authentication-service';
import { CartService } from '../../../../core/services/cart-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-top-bar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './top-bar.component.html'
})
export class TopBarComponent implements OnInit, OnDestroy{
  cartCount = 0;
  private cartSubscription!: Subscription;
  
  @Output() onSearch = new EventEmitter<string>();
  @Output() openUserMenu = new EventEmitter<void>();
  @Output() goToCart = new EventEmitter<void>();

  searchQuery: string = '';

  constructor(private authService: AuthService, private router: Router, private cartService: CartService) {}

  ngOnInit(): void {
    // Observa mudanças no número de itens do carrinho
    this.cartSubscription = this.cartService.cartCount$.subscribe((count) => {
      this.cartCount = count;
    });
  }

  ngOnDestroy(): void {
    if (this.cartSubscription) {
      this.cartSubscription.unsubscribe();
    }
  }

  get isAuthenticated(): boolean {
    return this.authService.isUserAuthenticated();
  }

    get isVendor(): boolean {
      return this.authService.isVendor();
    }

  onUserIconClick() {
    if (this.isAuthenticated) {
      if (!this.isVendor){
        this.router.navigate(['/customer']);
      }
    } else {
      this.router.navigate(['/login']);
    }
  }

  onAddProductIconClick() {
    if (this.isAuthenticated) {
      if (this.isVendor){
        this.router.navigate(['/add-products']);
      }
    } else {
      this.router.navigate(['/login']);
    }
  }
}
