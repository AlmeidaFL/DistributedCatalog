import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'
import { FileSystemEntry, NgxFileDropEntry, NgxFileDropModule } from 'ngx-file-drop';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule, NgFor } from '@angular/common';


@Component({
  selector: 'add-product',
  standalone: true,
  imports: [FormsModule, NgxFileDropModule, CommonModule, HttpClientModule],
  templateUrl: './add-product.component.html',
})
export class AddProductComponent {
  public files: NgxFileDropEntry[] = [];
  public placeholderImage: string = 'https://via.placeholder.com/80';

  public productByName = new Map<string, Product>()
  serverUrl = "http://localhost:5203/api/product"

  actualPrice: number = 0
  actualName: string = ""
  actualDescription: string = "" 

  constructor(private client: HttpClient){
  }

  public getImage(product: any): string | null {
    if (product.image) {
      return product.image instanceof Promise ? null : product.image; // Exibe `null` se a Promise ainda não foi resolvida
    }
    return this.placeholderImage;
  }

  public dropped(files: NgxFileDropEntry[]){
    files.forEach(item => {
      let image: File | undefined
      let fileEntry = item.fileEntry as FileSystemFileEntry
      fileEntry.file((file) => {
        image = file
      })

        if(item.fileEntry.isFile && image != undefined){
          const imageUrl = window.URL.createObjectURL(image)
          this.productByName.set(item.fileEntry.name, {name:"", description:"", price:0, image: {file: image, value: imageUrl}})
        }
    })
  }

public async upload() {
  const toBase64 = (file: File): Promise<string> => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result as string);  // Garante que result é uma string
      reader.onerror = () => reject('Erro ao converter arquivo para Base64');
    });
  };

  const productsArray = await Promise.all(Array.from(this.productByName.values()).map(async (product) => {
      return {
        name: product.name,
        description: product.description,
        price: product.price,
        image: await toBase64(product.image!.file)
  }}));

  console.log(this.serverUrl);

  this.client.post(this.serverUrl, productsArray).subscribe(data => {
      console.log(data);
  });
}

  updateName(name: string | undefined, productName: string| undefined){
    if (productName == undefined || name == undefined){
      return;
    }
    if (this.productByName.get(productName) == null || this.productByName.get(productName) == undefined){
      this.productByName.set(productName, {name:"", description:"", price:0, image: undefined})
    }
    this.productByName.get(productName)!.name = name
  }

  updateDescription(description: string | undefined, productName: string | undefined){
    if (productName == undefined || description == undefined){
      return;
    }
    
    if (this.productByName.get(productName) == null || this.productByName.get(productName) == undefined){
      this.productByName.set(productName, {name:"", description:"", price:0, image: undefined})
    }
    this.productByName.get(productName)!.description = description
  }

  updatePrice(price: number | undefined, productName: string | undefined){
    if (productName == undefined || price == undefined){
      return;
    }
    if (this.productByName.get(productName) == null || this.productByName.get(productName) == undefined){
      this.productByName.set(productName, {name:"", description:"", price:0, image: undefined})
    }
    this.productByName.get(productName)!.price = price
  }

  getName(productName: string | undefined){
    if (productName == undefined){
      return;
    }
    return this.productByName.get(productName)!.name
  }

  getDescription(productName: string | undefined){
    if (productName == undefined){
      return;
    }

    return this.productByName.get(productName)!.description
  }

  getPrice(productName: string | undefined){
    if (productName == undefined){
      return;
    }
    return this.productByName.get(productName)!.price
  }
}

interface Product{
  name: string,
  description: string,
  price: number,
  image: Image | undefined
}

interface Image {
  file: File,
  value: string
}