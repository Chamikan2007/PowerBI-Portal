import { Route } from '@angular/router';
import { Page404Component } from 'app/authentication/page404/page404.component';
import { SubscriptionDetailsComponent } from 'app/subscriptions/subscription-details/subscription-details.component';
import { ApprovalsListComponent } from './approvals-list/approvals-list.component';

export const APPROVALS_ROUTE: Route[] = [
    {
        path: '',
        component: ApprovalsListComponent,
    },
    {
        path: ':id',
        component: SubscriptionDetailsComponent
    },
    {
        path: '**',
        component: Page404Component
    },
];
