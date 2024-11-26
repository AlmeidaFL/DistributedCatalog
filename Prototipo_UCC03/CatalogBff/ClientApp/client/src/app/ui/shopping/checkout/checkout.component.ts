import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../core/services/authentication-service';
import { CartService } from '../../../../core/services/cart-service';
import { User } from '../../../../core/domain/user';
import { CartResume } from '../../../../core/domain/cart-resume';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { OrderService } from '../../../../core/services/order-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './checkout.component.html'
})
export class CheckoutComponent implements OnInit {
  isAddressModalOpen = false;
  isCardModalOpen = false;
  addresses: DeliveryAddress[] = [];
  cards: PaymentCard[] = [];
  selectedAddress?: DeliveryAddress;
  selectedCard?: PaymentCard;
  cart: CartResume = { id: '', products: [], total: 0 };
  newCard: PaymentCard = new PaymentCard();
  newAddress: DeliveryAddress = new DeliveryAddress();

  
  constructor(
    private orderService: OrderService,
    private authService: AuthService,
    private cartService: CartService,
    private router: Router) {}

  ngOnInit(): void {
    this.loadAddresses();
    this.loadCards();
    this.loadCart();
  }

  get user(): User{
    return this.authService.user;
  }
  
  toggleAddressModal() {
    this.isAddressModalOpen = !this.isAddressModalOpen;
  }
  toggleCardModal(){
    this.isCardModalOpen = !this.isCardModalOpen;
  }

  get shipmentFee(): number{
    if (this.selectedAddress !== undefined){
      return this.orderService.calculateShipment(this.selectedAddress!);
    }
    return 0;
  }

  get cartTotal(): number {
    return this.shipmentFee + this.cart.total
  }

  loadAddresses(): void {
    this.authService.getAddresses().subscribe((addresses) => (this.addresses = addresses));
  }

  addAddress(): void {
    this.authService.addAddress(this.newAddress).subscribe((addresses) => (this.addresses = addresses));
  }

  selectAddress(address: DeliveryAddress): void {
    this.selectedAddress = address;
  }

  loadCards(): void {
    this.authService.getCards().subscribe((cards) => (this.cards = cards));
  }

  addCard(): void {
    this.authService.addCard(this.newCard).subscribe((cards) => (this.cards = cards));
  }

  selectCard(card: PaymentCard): void {
    this.selectedCard = card;
  }

  loadCart(): void {
    this.cartService.getCartResume().subscribe((cart) => (this.cart = cart));
  }

  order(): void {
    this.orderService.order();
    this.router.navigate(['/'])
  }
}

export class DeliveryAddress {
  id?: string;
  street!: string;
  city!: string;
  state!: string;
  zipCode!: string;
  number!: string;
}

export class PaymentCard {
  id?: string;
  number!: string;
  cvv!: string;
  validUntilMonth!: number;
  validUntiLYear!: number;
}