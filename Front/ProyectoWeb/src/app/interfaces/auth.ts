export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest{
  email: string;
  password: string;
  name: string;
  description: string;
  imageUrl: string;
}

export interface UpdateAccountRequest {
  name: string;
  description: string;
  imageUrl: string;
  password: string;
}