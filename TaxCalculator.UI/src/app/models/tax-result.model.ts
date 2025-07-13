// src/app/models/tax-result.model.ts
export interface TaxResult {
  grossAnnual: number;
  grossMonthly: number;
  netAnnual: number;
  netMonthly: number;
  annualTax: number;
  monthlyTax: number;
}
