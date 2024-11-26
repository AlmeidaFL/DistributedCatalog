import { HttpClient } from "@angular/common/http";
import { AuthService } from "./authentication-service";
import { CartService } from "./cart-service";
import { apiUrl } from "./bff-config";
import { DeliveryAddress } from "../../app/ui/shopping/checkout/checkout.component";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class OrderService {
    baseClientUrl = apiUrl + "/orders"
    correlationId: string

    constructor(
        private authService: AuthService,
        private httpClient: HttpClient) {}

    public startCheckout(){
        this.httpClient.post<string>(`/pre-order/${this.authService.userId}`, {}).subscribe((data) => {
            this.correlationId = data;
        })
    }

    public order(){
        this.httpClient.post(`/${this.correlationId}`, {})
    }

    public calculateShipment(address: DeliveryAddress){
        return 15;
    }
}