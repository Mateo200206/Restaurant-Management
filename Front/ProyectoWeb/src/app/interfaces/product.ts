export interface Product {
  id: number;
  name: string;
  description: string | null;
  price: number;
  finalPrice: number;
  imageUrl: string | null;
  isFeatured: boolean;
  discountPercentage: number;
  hasHappyHour: boolean;
  categoryId: number;
}

export interface ProductRequest {
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  isFeatured: boolean;
  categoryId: number;
}