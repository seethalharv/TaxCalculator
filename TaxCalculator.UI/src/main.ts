import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { App } from './app/app';
import { TaxCalculatorService } from './app/services/tax-calculator.service';
import { HttpClient, provideHttpClient } from '@angular/common/http';

bootstrapApplication(App, {
  providers: [
    provideHttpClient(),
    TaxCalculatorService 
  ]
});