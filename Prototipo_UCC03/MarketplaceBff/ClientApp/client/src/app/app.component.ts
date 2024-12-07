// Componente TypeScript
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { AddProductComponent } from './ui/add-product/add-product.component';
import { TopBarComponent } from './ui/top-bar/top-bar/top-bar.component';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { ProductListComponent } from './ui/shopping/product-list/product-list.component';
import { ProductDetailsComponent } from './ui/shopping/product-details/product-details.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,  
  imports: [
    ProductDetailsComponent,
    ProductListComponent,
    FormsModule,
    AddProductComponent,
    RouterOutlet,
    RouterLink,
    RouterLinkActive]
})
export class AppComponent {
  inputValue: string = '';

  onInputChange(newValue: any) {
    console.log('Novo valor do input:', newValue);
    // Aqui você pode fazer qualquer ação que precise quando o valor mudar
  }
  
}
