import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TaxInput } from '../models/tax-input.model';
import { TaxResult } from '../models/tax-result.model';

@Injectable({
  providedIn: 'root'
})
export class TaxCalculatorService {
  private apiUrl = 'https://localhost:7072/api/taxcalculator/calculate';

  constructor(private http: HttpClient) {}

  calculateTax(input: TaxInput): Observable<TaxResult> {
    return this.http.post<TaxResult>(this.apiUrl, input);
  }
}
