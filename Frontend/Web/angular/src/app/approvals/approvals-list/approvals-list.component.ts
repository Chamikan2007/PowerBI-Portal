import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ResponseDto } from '@core/models/dto/response-dto';
import { ApprovalLevels, ApprovalStatus, SubscriptionDto, SubscriptionListModel, SubscriptionRequestApproverLevelModel } from '@core/models/dto/subscription-dto';
import { Role } from '@core/models/role';
import { AuthService } from '@core/service/auth.service';
import { SubscriptionService } from '@core/service/subscription.service';
import { FeatherIconsComponent } from '@shared/components/feather-icons/feather-icons.component';
import { SubscriptionRejectReasonComponent } from './subscription-reject-reason/subscription-reject-reason.component';

@Component({
  selector: 'app-approvals-list',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    FeatherIconsComponent,
    MatButtonModule
  ],
  templateUrl: './approvals-list.component.html',
  styleUrl: './approvals-list.component.scss'
})
export class ApprovalsListComponent {
  readonly dialog = inject(MatDialog);
  subscriptionList: SubscriptionListModel[] = [];
  approvalLevels: SubscriptionRequestApproverLevelModel[] = [];
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

    this.subscriptionService.getSubscriptionsListForApproval().subscribe({
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

  openRejectCommentDialog(subscriptionId: string, enterAnimationDuration: string, exitAnimationDuration: string): void {
    let dialogRef = this.dialog.open(SubscriptionRejectReasonComponent, {
      data: { subscriptionId: subscriptionId },
      width: '600px',
      disableClose: true,
      enterAnimationDuration,
      exitAnimationDuration,
    });

    dialogRef.afterClosed().subscribe((response: any) => {
      if (response.isSuccess) {
        this.loadData();
      }
    });
  }

  goBack() {
    this.router.navigate(['/', 'subscriptions']);
  }

  approveClicked(event: any) {
    if (confirm('Are you sure you want to Approve this subscription?')) {
      this.subscriptionService.sendSubscriptionAction(event.subscriptionId, 'Approved', '').subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess) {
            this.loadData();
          }
        }
      });
    }
  }

  rejectClicked(event: any) {
    this.openRejectCommentDialog(event.subscriptionId, '100ms', '100ms');
  }

  showHideApprovals(event: any, subscriptionId: any) {
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

      document.querySelectorAll(".detail-row").forEach(d => {
        if (d.id != `detail_${subscriptionId}`){
          d.classList.replace('visible', 'hidden');
        }
      });
    }
  }
}
