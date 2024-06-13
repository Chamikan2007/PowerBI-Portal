import { Route } from '@angular/router';
import { AllstaffComponent } from './all-staff/all-staff.component';
import { AddStaffComponent } from './add-staff/add-staff.component';
import { EditStaffComponent } from './edit-staff/edit-staff.component';
import { AboutStaffComponent } from './about-staff/about-staff.component';
import { Page404Component } from 'app/authentication/page404/page404.component';
export const STAFF_ROUTE: Route[] = [
  {
    path: 'all-staff',
    component: AllstaffComponent,
  },
  {
    path: 'add-staff',
    component: AddStaffComponent,
  },
  {
    path: 'edit-staff',
    component: EditStaffComponent,
  },
  {
    path: 'about-staff',
    component: AboutStaffComponent,
  },
  { path: '**', component: Page404Component },
];
