import { NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { Card } from 'primeng/card';

@Component({
  selector: 'app-home',
  imports: [FormsModule, Card, NgFor, ButtonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {
  options = [
    {
      description: 'Opções para Feature Toggles',
      name: 'Feature Toggles',
      route: '/feature-toggles',
    },
    {
      description: 'Opções para Ambientes',
      name: 'Ambientes',
      route: '/ambientes',
    },
  ];

  constructor(private router: Router) {}

  navigate(route: string) {
    this.router.navigate([route]);
  }
}
