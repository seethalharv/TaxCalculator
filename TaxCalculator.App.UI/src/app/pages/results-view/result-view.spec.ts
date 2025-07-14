import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { ResultView } from './result-view';
import { Router } from '@angular/router';
import { TaxCalculatorService } from '../../services/tax-calculator.service';
import { TaxResult } from '../../models/tax-result.model';
import { TaxInput } from '../../models/tax-input.model';
import { of, throwError } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

describe('ResultView', () => {
  let component: ResultView;
  let fixture: ComponentFixture<ResultView>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockTaxService: jasmine.SpyObj<TaxCalculatorService>;

  const mockResult: TaxResult = {
    grossAnnual: 60000,
    grossMonthly: 5000,
    netAnnual: 48000,
    netMonthly: 4000,
    annualTax: 12000,
    monthlyTax: 1000
  };

  beforeEach(async () => {
    mockRouter = jasmine.createSpyObj('Router', ['navigate', 'getCurrentNavigation']);
    mockTaxService = jasmine.createSpyObj('TaxCalculatorService', ['calculateTax']);

    mockRouter.getCurrentNavigation.and.returnValue({
      extras: {
        state: {
          salary: 60000,
          result: mockResult
        }
      }
    } as any);

    await TestBed.configureTestingModule({
      imports: [ResultView, CommonModule, FormsModule],
      providers: [
        { provide: Router, useValue: mockRouter },
        { provide: TaxCalculatorService, useValue: mockTaxService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ResultView);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize with salary and result from router state', () => {
    expect(component.salary).toBe(60000);
    expect(component.result).toEqual(mockResult);
  });

  it('should call taxService.calculateTax and update result on calculateAgain()', () => {
    const newResult: TaxResult = {
      grossAnnual: 70000,
      grossMonthly: 5833,
      netAnnual: 56000,
      netMonthly: 4666,
      annualTax: 14000,
      monthlyTax: 1166
    };
    mockTaxService.calculateTax.and.returnValue(of(newResult));

    component.salary = 70000;
    component.calculateAgain();

    expect(component.loading).toBeFalse();
    expect(component.result).toEqual(newResult);
    expect(component.errorMessage).toBeNull();
  });

  it('should handle errors from calculateAgain()', () => {
    mockTaxService.calculateTax.and.returnValue(throwError(() => new Error('API error')));
    component.salary = 75000;
    component.calculateAgain();

    expect(component.loading).toBeFalse();
    expect(component.errorMessage).toBe('Recalculation failed. Please try again.');
  });

  it('should navigate back on goBack()', () => {
    component.goBack();
    expect(mockRouter.navigate).toHaveBeenCalledWith(['/']);
  });
});
