import { Component } from '@angular/core';
import { Products } from "../products/products";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [Products, RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {

}
