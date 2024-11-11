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

  public productByName = new Map<string, Product>()
  serverUrl = "http://localhost:4200/api/product"

  actualPrice: number = 0
  actualName: string = ""
  actualDescription: string = "" 

  constructor(private client: HttpClient){
  }

  public dropped(files: NgxFileDropEntry[]){
    files.forEach(item => {
      let image: File | undefined
      let fileEntry = item.fileEntry as FileSystemFileEntry
      fileEntry.file((file) => {
        image = file
      })

        if(item.fileEntry.isFile && image != undefined){
          this.productByName.set(item.fileEntry.name, {name:"", description:"", price:0, image: undefined})
        }
    })
  }

  public upload(){
    const formData = new FormData()
    const productsArray = Array.from(this.productByName.values()).map((product) => {
      return {
        name: product.name,
        description: product.description,
        price: product.price,
        image: product.image
      };
    });


    this.client.post(this.serverUrl, productsArray)
    .subscribe(data => {
      console.log(data)
    })
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
  image: File | undefined
}
