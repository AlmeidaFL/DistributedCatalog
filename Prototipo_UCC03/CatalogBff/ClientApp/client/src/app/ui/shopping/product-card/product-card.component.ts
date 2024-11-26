import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CartService } from '../../../../core/services/cart-service';
import { Product } from '../../../../core/domain/product';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'product-card',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './product-card.component.html'
})
export class ProductCardComponent {

  @Input() product: Product | undefined

  constructor(private cartService: CartService){
  }

  addToCart(productId: string){
    this.cartService.addToCart(productId).subscribe({
      next: (data) => {
        // Maybe sync local storage
      },
      error: (err) => {
        console.error(err);
      },
    })
  }
}