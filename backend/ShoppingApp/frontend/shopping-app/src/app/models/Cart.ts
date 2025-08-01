import { Product } from "./Product";

export interface CartItem {
  productId: number;
  quantity: number;
  product?: Product; 
}