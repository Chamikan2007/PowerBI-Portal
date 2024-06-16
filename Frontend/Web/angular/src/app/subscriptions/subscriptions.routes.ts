import { Route } from '@angular/router';
import { Page404Component } from 'app/authentication/page404/page404.component';
import { SubscriptionDetailsComponent } from './subscription-details/subscription-details.component';
import { SubscriptionsListComponent } from './subscriptions-list/subscriptions-list.component';

export const SUBSCRIPTIONS_ROUTE: Route[] = [
    {
        path: '',
        component: SubscriptionsListComponent,
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
