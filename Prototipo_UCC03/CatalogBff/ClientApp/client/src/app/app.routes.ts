import { Routes } from '@angular/router';
import { ProductListComponent } from './ui/shopping/product-list/product-list.component';
import { CartResumeComponent } from './ui/shopping/cart-resume/cart-resume.component';
import { TopBarLayoutComponent } from './ui/top-bar/top-bar-layout/top-bar-layout.component';
import { ProductDetailsComponent } from './ui/shopping/product-details/product-details.component';
import { LoginComponent } from './ui/authentication/login/login.component';
import { RegisterUserComponent } from './ui/authentication/register-user/register-user.component';
import { UserManagementComponent } from './ui/user-management/user-management/user-management.component';
import { AddProductComponent } from './ui/add-product/add-product.component';
import { CheckoutComponent } from './ui/shopping/checkout/checkout.component';

export const routes: Routes = [
    {
        path: '',
        component: TopBarLayoutComponent,
        children: [
            { path: '', component: ProductListComponent },
            { path: 'product/:productId', component: ProductDetailsComponent }
        ]
    },
    {
        path: 'cart',
        component: CartResumeComponent
    },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterUserComponent},
    { path: 'customer', component: UserManagementComponent},
    { path: 'add-products', component: AddProductComponent},
    { path: 'checkout', component: CheckoutComponent},
    { path: 'order', component: CheckoutComponent}
];
