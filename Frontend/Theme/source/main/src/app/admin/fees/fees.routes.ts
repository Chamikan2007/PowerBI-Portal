import { Route } from '@angular/router';
import { AllFeesComponent } from './all-fees/all-fees.component';
import { AddFeesComponent } from './add-fees/add-fees.component';
import { EditFeesComponent } from './edit-fees/edit-fees.component';
import { FeeReceiptComponent } from './fee-receipt/fee-receipt.component';
import { Page404Component } from 'app/authentication/page404/page404.component';

export const FEES_ROUTE: Route[] = [
  {
    path: 'all-fees',
    component: AllFeesComponent,
  },
  {
    path: 'add-fees',
    component: AddFeesComponent,
  },
  {
    path: 'edit-fees',
    component: EditFeesComponent,
  },
  {
    path: 'fee-receipt',
    component: FeeReceiptComponent,
  },
  { path: '**', component: Page404Component },
];
