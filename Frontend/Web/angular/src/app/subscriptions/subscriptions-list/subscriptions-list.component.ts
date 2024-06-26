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
import { MatButtonModule } from '@angular/material/button';
import { SubscriptionService } from '@core/service/subscription.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ResponseDto } from '@core/models/dto/response-dto';
import { ApprovalLevels, ApprovalStatus, SubscriptionDto, SubscriptionListModel, SubscriptionRequestApproverLevelModel } from '@core/models/dto/subscription-dto';
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
  subscriptionList: SubscriptionListModel[] = [];
  approvalLevels:  SubscriptionRequestApproverLevelModel[] = [];
  approvalStatus = ApprovalStatus;
  approvalLevel = ApprovalLevels;

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

    this.subscriptionService.getMysubscriptionRequests().subscribe({
      next: (response) => {
        if (response && response.isSuccess) {
          this.subscriptionList = response.data;
        }
      }
    });
  }

  newSubscriptionClicked() {
    this.router.navigate(['0'], { relativeTo: this.activatedRoute.parent });
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

  showApprovals(event: any, subscriptionId: string) {
    let caller = event.currentTarget.children[0];
    let div = document.getElementById(`detail_${subscriptionId}`);

    if (div) {
      if (div.classList.contains('hidden')) {

        this.subscriptionService.getSubscriptionApprovalsById(subscriptionId).subscribe({
          next: (response) => {
            if (response.isSuccess) {
              this.approvalLevels = response.data;

              div.classList.replace('hidden', 'visible');
              caller.classList.replace('fa-chevron-right', 'fa-chevron-down');
            }
          }
        });
      }
      else {
        this.approvalLevels = [];
        div.classList.replace('visible', 'hidden');
        caller.classList.replace('fa-chevron-down', 'fa-chevron-right');
      }
    }
  }
}
