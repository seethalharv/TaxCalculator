import { TestBed, fakeAsync, tick } from '@angular/core/testing';
import { App } from './app';
import { TaxCalculatorService } from './services/tax-calculator.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { of, throwError } from 'rxjs';
import { TaxResult } from './models/tax-result.model';

describe('App', () => {
  let mockService: jasmine.SpyObj<TaxCalculatorService>;

  const mockTaxResult: TaxResult = {
    grossAnnual: 50000,
    grossMonthly: 4166.67,
    netAnnual: 40000,
    netMonthly: 3333.33,
    annualTax: 10000,
    monthlyTax: 833.33
  };

  beforeEach(async () => {
    mockService = jasmine.createSpyObj('TaxCalculatorService', ['calculateTax']);

    await TestBed.configureTestingModule({
      imports: [App, CommonModule, FormsModule],
      providers: [{ provide: TaxCalculatorService, useValue: mockService }]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h2')?.textContent).toContain('UK Tax Calculator');
  });

  it('should show error if salary is invalid', () => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    app.salary = 0;
    app.calculate();
    expect(app.error).toBe('Please enter a valid positive salary.');
    expect(app.result).toBeNull();
  });

  it('should call taxService and set result on success', fakeAsync(() => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    app.salary = 50000;
    mockService.calculateTax.and.returnValue(of(mockTaxResult));

    app.calculate();
    tick();

    expect(mockService.calculateTax).toHaveBeenCalledWith({ salary: 50000 });
    expect(app.result).toEqual(mockTaxResult);
    expect(app.error).toBeNull();
  }));

  it('should handle error from taxService', fakeAsync(() => {
    const fixture = TestBed.createComponent(App);
    const app = fixture.componentInstance;
    app.salary = 60000;
    mockService.calculateTax.and.returnValue(throwError(() => new Error('CORS error')));

    app.calculate();
    tick();

    expect(mockService.calculateTax).toHaveBeenCalledWith({ salary: 60000 });
    expect(app.result).toBeNull();
    expect(app.error).toContain('API call failed');
  }));
});
