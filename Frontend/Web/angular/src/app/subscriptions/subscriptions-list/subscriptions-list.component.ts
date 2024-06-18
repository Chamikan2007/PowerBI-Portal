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

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private subscriptionService: SubscriptionService
  ) { }

  ngOnInit(): void {
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
