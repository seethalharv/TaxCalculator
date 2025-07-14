import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TaxCalculatorService } from '../../services/tax-calculator.service';
import { TaxInput } from '../../models/tax-input.model';
import { TaxResult } from '../../models/tax-result.model';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-salary-input',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './salary-input.html',
  styleUrl: './salary-input.css',
})
export class SalaryInput {
  salary: number = 0;

  constructor(
    private taxService: TaxCalculatorService,
    private router: Router
  ) {}

  errorMessage: string | null = null;
  calculate() {
    const input: TaxInput = { salary: this.salary };
    this.taxService.calculateTax(input).subscribe({
      next: (result: TaxResult) => {
        this.router.navigate(['/result'], { state: { result } });
      },
      error: (err) => {
        this.errorMessage = this.getFriendlyErrorMessage(err);
        setTimeout(() => this.errorMessage = null, 5000);
        console.error('Tax calculation failed', err);
      }
    });
  }

  private getFriendlyErrorMessage(error: any): string {
  if (error.status === 400) {
    return 'Invalid input. Please enter a valid salary greater than 1.';
  } else if (error.status === 0) {
    return 'Unable to connect to server. Please try again.';
  } else {
    return 'Unexpected error occurred. Please try again later.';
  }
}

}
