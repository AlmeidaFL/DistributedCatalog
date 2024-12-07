import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';
import { Product } from '../../../../core/domain/product';
import { CatalogService } from '../../../../core/services/product-service';
import { ProductDetails } from '../product-details/product-details.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private catalogService: CatalogService) {}

  ngOnInit(): void {
    this.catalogService.getAllProducts().subscribe({
      next: (productDetails: ProductDetails[]) => {
        // Mapeando os dados recebidos para o modelo Product
        this.products = productDetails.map(
          (details) =>
            new Product(details.id, details.name, details.price, details.image)
        );
      },
      error: (err) => {
        console.error('Erro ao buscar produtos:', err);
      }
    });
  }
}