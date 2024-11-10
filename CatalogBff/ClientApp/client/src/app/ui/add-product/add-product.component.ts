import { Component } from '@angular/core';
import { FileItem, FileUploader, FileUploadModule } from 'ng2-file-upload';
import { FormsModule } from '@angular/forms'


@Component({
  selector: 'add-product',
  standalone: true,
  imports: [FileUploadModule, FormsModule],
  templateUrl: './add-product.component.html',
})
export class AddProductComponent {
  uploader: FileUploader;
  productByName = new Map<string, Product>()
  serverUrl = "https://localhost:5203/api/product/"

  actualPrice: number = 0
  actualName: string = ""
  actualDescription: string = "" 

  constructor(){
    this.uploader = new FileUploader({
      url: this.serverUrl,
      disableMultipart: true,
      formatDataFunction: async (item: FileItem) => {
        return new Promise( (resolve, _) => {
          resolve({
            name: item._file.name,
            description: this.productByName.get(item._file.name)!.description,
            price: this.productByName.get(item._file.name)!.price,
            image: item
          });
        });
      }
    })
  }

  public onChange(value: any){
    console.log("any")
  }

  updateName(name: string | undefined, productName: string| undefined){
    if (productName == undefined || name == undefined){
      return;
    }
    if (this.productByName.get(productName) == null || this.productByName.get(productName) == undefined){
      this.productByName.set(productName, {name:"", description:"", price:0})
    }
    this.productByName.get(productName)!.name = name
  }

  updateDescription(description: string | undefined, productName: string | undefined){
    if (productName == undefined || description == undefined){
      return;
    }
    if (this.productByName.get(productName) == null || this.productByName.get(productName) == undefined){
      this.productByName.set(productName, {name:"", description:"", price:0})
    }
    this.productByName.get(productName)!.description = description
  }

  updatePrice(price: number | undefined, productName: string | undefined){
    if (productName == undefined || price == undefined){
      return;
    }
    if (this.productByName.get(productName) == null || this.productByName.get(productName) == undefined){
      this.productByName.set(productName, {name:"", description:"", price:0})
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
}
