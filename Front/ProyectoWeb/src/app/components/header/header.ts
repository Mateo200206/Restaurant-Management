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

    onToggleAuto(){
      if (this.themeService.theme() === "auto") {
        // Al desmarcar Auto, se queda en el tema que se está viendo
        this.themeService.setTheme(this.themeService.isLight() ? "light" : "dark");
      } else {
        this.themeService.setTheme("auto");
      }
    }

    logout(){
      this.authService.logout();
      this.router.navigate(["/"]);
    }

}
