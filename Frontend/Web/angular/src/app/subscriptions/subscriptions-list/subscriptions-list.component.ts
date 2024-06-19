import { Component, inject, OnInit } from '@angular/core';
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
import { SubscriptionService } from '@core/service/subscription.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ResponseDto } from '@core/models/dto/response-dto';
import { ApprovalStatus, SubscriptionDto } from '@core/models/dto/subscription-dto';
import { AuthService, Role } from '@core';

@Component({
  selector: 'app-subscriptions-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    FeatherIconsComponent,
    MatButtonModule
  ],
  templateUrl: './subscriptions-list.component.html',
  styleUrl: './subscriptions-list.component.scss'
})
export class SubscriptionsListComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  subscriptionList: any[] = [];
  approvalStatus = ApprovalStatus;

  currentUserId: string = '';
  userRoles: Role[] = [];
  approverRole = Role.Approver;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private subscriptionService: SubscriptionService
  ) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUserValue.requestContext.userId;
    this.userRoles = this.authService.currentUserValue.requestContext.roles
    this.loadData();
  }

  loadData() {
    this.subscriptionList.length = 0;

    this.subscriptionService.getMySubscriptionsList().subscribe({
      next: (response: ResponseDto) => {
        if (response && response.isSuccess) {
          this.subscriptionList = response.data as SubscriptionDto[];
        }
      }
    });
  }

  newSubscriptionClicked() {
    this.router.navigate(['0'], { relativeTo: this.activatedRoute.parent });
  }

  openRejectCommentDialog(subscriptionId: string, enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(SubscriptionRejectReasonComponent, {
      data: { subscriptionId: subscriptionId },
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
    if (confirm('Are you sure you want to Delete this subscription?')) {
      this.subscriptionService.deleteSubscription(event.subscriptionId).subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess) {
            this.loadData();
          }
        }
      });
    }
  }

  approveClicked(event: any) {

  }

  rejectClicked(event: any) {
    if (confirm('Are you sure you want to Reject this subscription?')) {
      this.openRejectCommentDialog(event.subscriptionId, '100ms', '100ms');
      // this.router.navigate(['0'], { relativeTo: this.activatedRoute.parent });
    }
  }
}
