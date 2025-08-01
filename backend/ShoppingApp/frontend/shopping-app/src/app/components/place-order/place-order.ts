import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderService } from '../../services/order-service';

@Component({
  selector: 'app-place-order',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './place-order.html',
  styleUrl: './place-order.css'
})
export class PlaceOrder {
loginForm: FormGroup;
  loading = false;

  constructor(private router: Router, private orderService: OrderService) {
    this.loginForm = new FormGroup({
      PaymentType: new FormControl('', Validators.required),
      CustomerName: new FormControl('', Validators.required),
      CustomerPhone: new FormControl('', Validators.required),
      CustomerEmail: new FormControl('', [Validators.required, Validators.email]),
      CustomerAddress: new FormControl('', Validators.required),
    });
  }

  get PaymentType() { return this.loginForm.get('PaymentType'); }
  get CustomerName() { return this.loginForm.get('CustomerName'); }
  get CustomerPhone() { return this.loginForm.get('CustomerPhone'); }
  get CustomerEmail() { return this.loginForm.get('CustomerEmail'); }
  get CustomerAddress() { return this.loginForm.get('CustomerAddress'); }

  submitOrder() {
    if (this.loginForm.invalid) return;

    const cartItems = JSON.parse(localStorage.getItem('cart') || '[]');
    const formData = {
      paymentType: this.PaymentType?.value,
      customerName: this.CustomerName?.value,
      customerPhone: this.CustomerPhone?.value,
      customerEmail: this.CustomerEmail?.value,
      customerAddress: this.CustomerAddress?.value,
      CartItems: cartItems.map((item: any) => ({
        ProductId: item.productId,
        Quantity: item.quantity
      }))
    };

    this.loading = true;

    this.orderService.placeOrder(formData).subscribe({
      next: () => {
        localStorage.removeItem('cart');
        this.router.navigate(['/home']);
      },
      error: err => {
        console.error('Order failed:', err);
        this.loading = false;
      }
    });
  }
}

