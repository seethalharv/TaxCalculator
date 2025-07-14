import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { SalaryInput } from './salary-input';
import { Router } from '@angular/router';
import { TaxCalculatorService } from '../../services/tax-calculator.service';
import { of, throwError } from 'rxjs';
import { TaxResult } from '../../models/tax-result.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

describe('SalaryInput', () => {
  let component: SalaryInput;
  let fixture: ComponentFixture<SalaryInput>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockTaxService: jasmine.SpyObj<TaxCalculatorService>;

  const mockResult: TaxResult = {
    grossAnnual: 50000,
    grossMonthly: 4166.67,
    netAnnual: 40000,
    netMonthly: 3333.33,
    annualTax: 10000,
    monthlyTax: 833.33
  };

  beforeEach(async () => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    mockTaxService = jasmine.createSpyObj('TaxCalculatorService', ['calculateTax']);

    await TestBed.configureTestingModule({
      imports: [SalaryInput, FormsModule, CommonModule],
      providers: [
        { provide: Router, useValue: mockRouter },
        { provide: TaxCalculatorService, useValue: mockTaxService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(SalaryInput);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the SalaryInput component', () => {
    expect(component).toBeTruthy();
  });

  it('should call taxService and navigate on successful calculation', fakeAsync(() => {
    mockTaxService.calculateTax.and.returnValue(of(mockResult));
    component.salary = 50000;

    component.calculate();
    tick(100); // simulate setTimeout delay

    expect(mockTaxService.calculateTax).toHaveBeenCalledWith({ salary: 50000 });
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/result'], {
      state: { salary: 50000, result: mockResult }
    });
    expect(component.loading).toBeFalse();
  }));

  it('should handle 400 error and set friendly message', fakeAsync(() => {
    const mockError = { status: 400 };
    mockTaxService.calculateTax.and.returnValue(throwError(() => mockError));

    component.salary = 1000;
    component.calculate();

    expect(component.errorMessage).toBe('Invalid input. Please enter a valid salary greater than 1.');
    tick(5000); // simulate timeout to clear error
    expect(component.errorMessage).toBeNull();
  }));

  it('should handle network error (status 0)', () => {
    const mockError = { status: 0 };
    mockTaxService.calculateTax.and.returnValue(throwError(() => mockError));

    component.calculate();

    expect(component.errorMessage).toBe('Unable to connect to server. Please try again.');
  });

  it('should handle unexpected error', () => {
    const mockError = { status: 500 };
    mockTaxService.calculateTax.and.returnValue(throwError(() => mockError));

    component.calculate();

    expect(component.errorMessage).toBe('Unexpected error occurred. Please try again later.');
  });
});
