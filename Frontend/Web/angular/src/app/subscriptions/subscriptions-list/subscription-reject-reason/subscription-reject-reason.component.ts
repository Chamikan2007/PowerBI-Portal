import { CommonModule } from '@angular/common';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ResponseDto } from '@core/models/dto/response-dto';
import { AuthService } from '@core/service/auth.service';
import { SubscriptionService } from '@core/service/subscription.service';
import { UnsubscribeOnDestroyAdapter } from '@shared/UnsubscribeOnDestroyAdapter';

@Component({
  selector: 'app-subscription-reject-reason',
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,

    RouterLink,
    MatButtonModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
  ],
  templateUrl: './subscription-reject-reason.component.html',
  styleUrl: './subscription-reject-reason.component.scss'
})
export class SubscriptionRejectReasonComponent extends UnsubscribeOnDestroyAdapter implements OnInit {

  rejectionForm!: UntypedFormGroup;
  submitted = false;
  loading = false;
  error = '';
  // hide = true;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private subscriptionService: SubscriptionService,
  ) {
    super();
  }

  ngOnInit(): void {
    this.rejectionForm = this.formBuilder.group({
      reason: ['', Validators.required,],
    });
  }

  get f() {
    return this.rejectionForm.controls;
  }

  onSubmitRejection() {
    if (this.rejectionForm.invalid) {
      this.error = 'Input values are not valid!';
      return;
    }
    else {
      debugger;
      this.subscriptionService.sendSubscriptionAction(this.data.subscriptionId, 'Reject', this.f['reason'].value).subscribe({
        next: (response: ResponseDto) => {
          debugger;
          if (response.isSuccess) {

          }
        },
        error: (error: any) => {
          debugger;
        }
      });
    }
  }

}
