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

  /** Devuelve true si lo que se ve en pantalla es el tema claro (también en modo auto) */
  isLight() {
    if (this.theme() === "auto") {
      return !window.matchMedia("(prefers-color-scheme: dark)").matches;
    }
    return this.theme() === "light";
  }

  constructor() {
    this.applyTheme();
  }
}
