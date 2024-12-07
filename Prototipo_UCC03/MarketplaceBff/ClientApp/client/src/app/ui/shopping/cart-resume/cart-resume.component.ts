import { Component } from '@angular/core';
import { CartService } from '../../../../core/services/cart-service';
import { CartResume } from '../../../../core/domain/cart-resume';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { OrderService } from '../../../../core/services/order-service';

@Component({
  selector: 'cart-resume',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './cart-resume.component.html'
})
export class CartResumeComponent {
  cartResume: CartResume | null = null;

  constructor(private orderService: OrderService, private cartService: CartService, private router: Router) {}

  ngOnInit(): void {
    this.loadCart();
  }

  async loadCart() {
    this.cartService.getCartResume().subscribe({
      next: (resume) => {
        this.cartResume = resume;
      },
      error: (err) => {
        console.error(err);
      },
    });
  }

  getProductQuantity(productId: string): number {
    const product = this.cartResume?.products?.find(p => p.id === productId);
    return product?.quantity ?? 0;
  }
  
  checkout(): void {
    this.orderService.startCheckout()
    this.router.navigate(['/checkout'])
  }
}
