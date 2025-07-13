import { Component } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { HttpClient, provideHttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TaxResult } from './models/tax-result.model';
import { TaxInput } from './models/tax-input.model';
import { TaxCalculatorService } from './services/tax-calculator.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDividerModule } from '@angular/material/divider';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule,
     FormsModule, 
     MatProgressSpinnerModule, 
     MatDividerModule,
     MatCardModule, 
     MatFormFieldModule,
     MatInputModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  salary: number = 0;
  result!: TaxResult | null;
  error: string | null = null;
  isLoading = false;
  errorMessage = '';

  constructor(private taxService: TaxCalculatorService) {}

  calculate() {
    this.error = null;
    this.result = null;
    this.errorMessage = '';
    this.isLoading = true;

    if (!this.salary || this.salary <= 0) {
      this.error = 'Please enter a valid positive salary.';
      this.isLoading = false;
      return;
    }
    const input: TaxInput = { salary: this.salary };

    this.taxService.calculateTax(input).subscribe({
      next: (data : TaxResult) => {this.result = data; 
                                  this.isLoading = false;
      },
      error: () => {this.errorMessage = 'API call failed. Is the backend running? CORS might be blocking it.';
                    this.isLoading = false;
      }
    });
  }
}