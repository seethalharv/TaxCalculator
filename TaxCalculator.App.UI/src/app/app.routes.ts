import { Routes } from '@angular/router';
import { SalaryInput } from './pages/salary-input/salary-input';
import { ResultView } from './pages/results-view/result-view';


export const routes: Routes = [
  { path: '', component: SalaryInput },
  { path: 'result', component: ResultView }
];

