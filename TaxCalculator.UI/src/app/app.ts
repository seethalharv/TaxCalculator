import { Component } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TaxResult } from './models/tax-result.model';
import { TaxInput } from './models/tax-input.model';
import { TaxCalculatorService } from './services/tax-calculator.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  salary: number = 0;
  result!: TaxResult | null;
  error: string | null = null;

  constructor(private taxService: TaxCalculatorService) {}

  calculate() {
    this.error = null;
    this.result = null;

    if (!this.salary || this.salary <= 0) {
      this.error = 'Please enter a valid positive salary.';
      return;
    }
    const input: TaxInput = { salary: this.salary };

    this.taxService.calculateTax(input).subscribe({
      next: (data : TaxResult) => this.result = data,
      error: () => this.error = 'API call failed. Is the backend running? CORS might be blocking it.'
    });
  }
}

// bootstrapApplication(App, {
//   providers: [provideHttpClient()]
// });
