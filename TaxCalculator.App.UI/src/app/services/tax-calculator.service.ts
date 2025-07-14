import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TaxInput } from '../models/tax-input.model';
import { TaxResult } from '../models/tax-result.model';
import { environment } from '../../environments/environment'; 

@Injectable({
  providedIn: 'root'
})
export class TaxCalculatorService {
  private readonly baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) {}

  calculateTax(input: TaxInput): Observable<TaxResult> {
   return this.http.post<TaxResult>(`${this.baseUrl}/api/taxcalculator/calculate`, input);
  }
}
