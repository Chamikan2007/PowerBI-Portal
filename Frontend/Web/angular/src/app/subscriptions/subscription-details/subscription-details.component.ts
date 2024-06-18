import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ResponseDto } from '@core/models/dto/response-dto';
import { SubscriptionDto } from '@core/models/dto/subscription-dto';
import { SubscriptionService } from '@core/service/subscription.service';

@Component({
  selector: 'app-subscription-details',
  standalone: true,
  imports: [
    RouterLink,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  templateUrl: './subscription-details.component.html',
  styleUrl: './subscription-details.component.scss'
})
export class SubscriptionDetailsComponent implements OnInit {

  subscriptionForm!: UntypedFormGroup;
  submitted = false;
  loading = false;
  error = '';
  hide = true;

  model: SubscriptionDto = new SubscriptionDto();

  constructor(
    private formBuilder: UntypedFormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private subscriptionService: SubscriptionService
  ) { }

  ngOnInit(): void {
    this.subscriptionForm = this.formBuilder.group({
      report: ['', Validators.required,],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  get f() {
    return this.subscriptionForm.controls;
  }

  goBack() {
    this.router.navigate(['/', 'subscriptions']);
  }

  onSubmit() {
    if (this.subscriptionForm.invalid) {
      this.error = 'Input values are not valid!';
      return;
    }
    else {
      this.model.report = this.f['report'].value;
      this.model.email = this.f['email'].value;

      this.subscriptionService.createSubscription(this.model).subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess) {
            this.router.navigate(['../'], { relativeTo: this.activatedRoute });
          }
          else {
            this.error = response.error.errorCode;
          }
        },
        error: (error: any) => {
          this.error = 'Something went wrong.';
        }
      });
    }
  }

  onCancel() {
    this.router.navigate(['/', 'subscriptions']);
  }
}
