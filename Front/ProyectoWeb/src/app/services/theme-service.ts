import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  theme = signal<"light" | "dark" | "auto">(
    (localStorage.getItem("theme") as "light" | "dark" | "auto") ?? "auto"
  );

  setTheme(newTheme: "light" | "dark" | "auto") {
    this.theme.set(newTheme);
    localStorage.setItem("theme", newTheme);
    this.applyTheme();
  }

  applyTheme() {
    const html = document.documentElement;
    if (this.theme() === "auto") {
      html.removeAttribute("data-theme");
    } else {
      html.setAttribute("data-theme", this.theme());
    }
  }

  constructor() {
    this.applyTheme();
  }
}
