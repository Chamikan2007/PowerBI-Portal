import { Route } from '@angular/router';
import { AllAssetsComponent } from './all-assets/all-assets.component';
import { AddAssetComponent } from './add-asset/add-asset.component';
import { EditAssetComponent } from './edit-asset/edit-asset.component';
import { Page404Component } from 'app/authentication/page404/page404.component';

export const LIBRARY_ROUTE: Route[] = [
  {
    path: 'all-assets',
    component: AllAssetsComponent,
  },
  {
    path: 'add-asset',
    component: AddAssetComponent,
  },
  {
    path: 'edit-asset',
    component: EditAssetComponent,
  },
  { path: '**', component: Page404Component },
];
