
export interface Product {
  productId: number;
  productName: string;
  image: string;
  price:number;
  category: string;
  color: string;
  model: string | null;
  storage: number;
  sellStartDate: string;
  sellEndDate: string;
  isNew: number;
}