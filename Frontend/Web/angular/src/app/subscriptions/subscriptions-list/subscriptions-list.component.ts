import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FeatherIconsComponent } from '@shared/components/feather-icons/feather-icons.component';
import {
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { SubscriptionRejectReasonComponent } from './subscription-reject-reason/subscription-reject-reason.component';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-subscriptions-list',
  standalone: true,
  imports: [
    FeatherIconsComponent,
    MatButtonModule,

  ],
  templateUrl: './subscriptions-list.component.html',
  styleUrl: './subscriptions-list.component.scss'
})
export class SubscriptionsListComponent {

  readonly dialog = inject(MatDialog);


  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  newSubscriptionClicked() {
    this.router.navigate(['0'], { relativeTo: this.activatedRoute.parent });
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(SubscriptionRejectReasonComponent, {
      width: '600px',
      disableClose: true,
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
  goBack() {
    this.router.navigate(['/', 'admin', 'dashboard']);
  }

  editClicked(event: any) {
    // get id
    this.router.navigate(['0'], { relativeTo: this.activatedRoute.parent });
  }

  deleteClicked(event: any) {
    this.openDialog('100ms', '100ms');

    // this.router.navigate(['0'], { relativeTo: this.activatedRoute.parent });
  }
}
