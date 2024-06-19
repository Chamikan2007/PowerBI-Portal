import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogRef, MatDialogTitle, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ResponseDto } from '@core/models/dto/response-dto';
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

  isRejectSuccess: boolean = false;

  constructor(
    public matDialogRef: MatDialogRef<SubscriptionRejectReasonComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private formBuilder: UntypedFormBuilder,
    private subscriptionService: SubscriptionService,
  ) {
    super();
  }

  ngOnInit(): void {
    this.rejectionForm = this.formBuilder.group({
      reason: ['', Validators.required,],
    });

    this.matDialogRef.afterClosed().subscribe(result => {
      this.matDialogRef.close({ isSuccess: this.isRejectSuccess });
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
      this.subscriptionService.sendSubscriptionAction(this.data.subscriptionId, 'Rejected', this.f['reason'].value).subscribe({
        next: (response: ResponseDto) => {
          this.isRejectSuccess = response.isSuccess
          this.matDialogRef.close({ isSuccess: this.isRejectSuccess });
        }
      });
    }
  }

}
