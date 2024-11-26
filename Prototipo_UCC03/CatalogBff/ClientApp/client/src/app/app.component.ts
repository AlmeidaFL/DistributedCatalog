// Componente TypeScript
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { AddProductComponent } from './ui/add-product/add-product.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,  imports: [FormsModule, AddProductComponent]
})
export class AppComponent {
  inputValue: string = '';

  onInputChange(newValue: any) {
    console.log('Novo valor do input:', newValue);
    // Aqui você pode fazer qualquer ação que precise quando o valor mudar
  }
}
