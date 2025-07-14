import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxResult } from '../../models/tax-result.model';
import { Router } from '@angular/router';
import { TaxCalculatorService } from '../../services/tax-calculator.service';
import { TaxInput } from '../../models/tax-input.model';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-result-view',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './result-view.html',
  styleUrl: './result-view.css'
})
export class ResultView implements OnInit {
  salary: number = 0;
  result!: TaxResult;
  errorMessage: string | null = null;
  loading: boolean = false;

  constructor(private router: Router, private taxService: TaxCalculatorService) {
    const nav = this.router.getCurrentNavigation();
    const state = nav?.extras?.state as { salary: number, result: TaxResult };
    if (state) {
      this.salary = state.result.grossAnnual;
      this.result = state.result;
    }
  }
  ngOnInit(): void { }
  calculateAgain() {
    this.loading = true;
    const input: TaxInput = { salary: this.salary };
    this.taxService.calculateTax(input).subscribe({
      next: (res) => {
        this.result = res;
        this.errorMessage = null;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        this.errorMessage = 'Recalculation failed. Please try again.';
        console.error(err);
      }
    });
  }
  goBack() {
    this.router.navigate(['/']);
  }
}
