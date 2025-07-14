import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaxResult } from '../../models/tax-result.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-result-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './result-view.html',
  styleUrl: './result-view.css'
})
export class ResultView {
result!: TaxResult;

  constructor(private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.result = history.state.result;   
  }
   goBack() {
    this.router.navigate(['/']);  // Assumes '' or '/' is the salary input route
  }
}
