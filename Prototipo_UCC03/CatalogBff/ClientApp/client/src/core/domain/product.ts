export class Product{
    id: string
    name: string
    quantity: number
    price: number
    image: string
    constructor(id: string, name: string, price: number, image: string, quantity: number = 1){
      this.id = id
      this.name = name
      this.price = price
      this.image = image
      this.quantity = quantity
    }
  }