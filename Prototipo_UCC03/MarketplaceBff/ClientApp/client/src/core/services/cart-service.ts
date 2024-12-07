import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { AuthService } from "./authentication-service";
import { CartResume } from "../domain/cart-resume";
import { Product } from "../domain/product";
import { apiUrl } from "./bff-config";

type CartTotal = number;
type ProductsIds = string[]

@Injectable(
    { providedIn: 'root' }
)
export class CartService {
    baseClientUrl = apiUrl + "/cart"

    private cartCountSubject = new BehaviorSubject<number>(0);
    cartCount$ = this.cartCountSubject.asObservable();

    constructor(private httpClient: HttpClient, private authService: AuthService){
    }

    addToCart(productId: string, quantity: number = 1): Observable<Cart> {
        if (this.authService?.userId !== undefined) {
            const cartTotal = this.updateLocalStorage(productId, this.authService.userId, quantity);
            this.cartCountSubject.next(cartTotal);
            return this.httpClient.put<Cart>(`${this.baseClientUrl}/${this.authService.userId}/product`, {
                id: productId,
                quantity: quantity
            });
        }
        throw new Error("User not authenticated");
    }
    
    updateLocalStorage(productId: string, cartKey: string, quantity: number): number {
        const cart = this.getLocalCart();
    
        const updatedQuantity = cart + quantity;
    
        localStorage.setItem(cartKey, JSON.stringify(updatedQuantity));
    
        return updatedQuantity;
    }
    
    public getLocalCart(): number {
        return parseInt(localStorage.getItem(this.authService?.userId!) || '0', 10);
    }
    

    public getCartResume(): Observable<CartResume> {
        return this.httpClient.get<CartResume>(`${this.baseClientUrl}/${this.authService?.userId}/`);
        if (this.authService?.userId !== undefined){
            throw new Error("User not authenticated");
        }
    }


    public mockProducts(): Product[] {
        return  [
            new Product('1','Produto 1',29.99,'assets/products/product1.jpg'),
            new Product('2', 'Produto 2', 59.99, 'assets/products/product2.jpg'),
            new Product('3', 'Produto 3', 19.99, 'assets/products/product3.jpg'),
            new Product('4', 'Produto 4', 99.99, 'assets/products/product4.jpg'),
            new Product('1','Produto 1',29.99,'assets/products/product1.jpg'),
            new Product('2', 'Produto 2', 59.99, 'assets/products/product2.jpg'),
            new Product('3', 'Produto 3', 19.99, 'assets/products/product3.jpg'),
            new Product('4', 'Produto 4', 99.99, 'assets/products/product4.jpg'),
            new Product('1','Produto 1',29.99,'assets/products/product1.jpg'),
            new Product('2', 'Produto 2', 59.99, 'assets/products/product2.jpg'),
            new Product('3', 'Produto 3', 19.99, 'assets/products/product3.jpg'),
            new Product('4', 'Produto 4', 99.99, 'assets/products/product4.jpg'),
            new Product('1','Produto 1',29.99,'assets/products/product1.jpg'),
            new Product('2', 'Produto 2', 59.99, 'assets/products/product2.jpg'),
            new Product('3', 'Produto 3', 19.99, 'assets/products/product3.jpg'),
            new Product('4', 'Produto 4', 99.99, 'assets/products/product4.jpg'),
          ];
    }
}

export interface Cart {
    userId: string;
    products: CartProduct[];
    totalPrice: number;
}

export interface CartProduct {
    id: string;
    quantity: number;
    price: number
}