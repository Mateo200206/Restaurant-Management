import { Injectable, signal } from '@angular/core';
import { LoginRequest, RegisterRequest, UpdateAccountRequest } from '../interfaces/auth';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  readonly URL_BASE = "https://localhost:7074/api";
  token = signal<string | null>(localStorage.getItem("token"));

    async login(loginData: LoginRequest) {
      const response = await fetch(this.URL_BASE + "/authentication/authenticate", {
        method: "POST",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(loginData)
      });
      if (!response.ok) return false;
      const jwt = await response.text();
      localStorage.setItem("token", jwt);
      this.token.set(jwt);
      return true;
    }

    logout(){
      localStorage.removeItem("token");
      this.token.set(null);
    }

    async register(registerData: RegisterRequest){
      const response = await fetch(this.URL_BASE + "/restaurants",{
      method: "POST",
      headers: { "content-type": "application/json" },
      body: JSON.stringify(registerData)
      });
      return response.ok;
    }
    
    async deleteAccount(){
      const response = await fetch(this.URL_BASE + "/restaurants", {
        method: "DELETE",
        headers: { "content-type": "application/json", Authorization: "Bearer " + this.token()},
      });
      if (!response.ok) return false;
      this.logout();
      return true;
    }

    async updateAccount(data: UpdateAccountRequest){
      const response = await fetch(this.URL_BASE + "/restaurants",{
      method: "PUT",
      headers: { "content-type": "application/json", Authorization: "Bearer " + this.token()},
      body: JSON.stringify(data)
      });
      return response.ok;
    }

    getRestaurantId(): number | null {
      const token = this.token();
      if (!token) return null;
      const payload = JSON.parse(atob(token.split(".")[1]));
      return Number(payload.sub);
    }
}
