import { Route } from '@angular/router';
import { Page404Component } from 'app/authentication/page404/page404.component';
import { RequestDetailsComponent } from './request-details/request-details.component';
import { RequestsListComponent } from './requests-list/requests-list.component';

export const REQUESTS_ROUTE: Route[] = [
    {
        path: '',
        component: RequestsListComponent,
    },
    {
        path: ':id',
        component: RequestDetailsComponent
    },
    {
        path: '**',
        component: Page404Component
    },
];
