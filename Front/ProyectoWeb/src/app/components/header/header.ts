import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router, RouterLink } from '@angular/router';
import { ThemeService } from '../../services/theme-service';


@Component({
  selector: 'app-header',
  imports: [RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  authService = inject(AuthService);
  themeService = inject(ThemeService);
  router = inject(Router);

    logout(){
      this.authService.logout();
      this.router.navigate(["/"]);
    }

}
