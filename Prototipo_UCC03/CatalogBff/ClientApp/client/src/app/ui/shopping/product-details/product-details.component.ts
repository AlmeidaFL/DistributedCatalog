import { Component, Input } from '@angular/core';
import { CatalogService } from '../../../../core/services/product-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../../../../core/services/cart-service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-details.component.html'
})
export class ProductDetailsComponent {
  product: ProductDetails | null = null;
  quantity: number = 1;
  isLoading: boolean = true;
  errorMessage: string | null = null;

  constructor(private route: ActivatedRoute, private productService: CatalogService, private cartService: CartService) {}

  ngOnInit(): void {
    const productId = this.route.snapshot.paramMap.get('productId'); // Obtemos o ID do produto da URL
    if (productId) {
      this.fetchProductDetails(productId);
    }
  }

  private fetchProductDetails(productId: string){
    this.productService.getProductDetails(productId).subscribe({
      next: (data) => {
        this.product = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = 'Erro ao carregar os detalhes do produto.';
        this.isLoading = false;
      },
    });
  }


  async addToCart(product: ProductDetails, quantity: number) {
    this.cartService.addToCart(product.id, quantity).subscribe({
      next: (data) => {
        // Maybe sync local storage
      },
      error: (err) => {
        console.error(err);
      },
    })
  }
}

export class ProductDetails {
  id:string
  vendorId: string
  image: string
  name: string
  price: number
  stockQuantity: number
  categories: string[]
  description: string
}