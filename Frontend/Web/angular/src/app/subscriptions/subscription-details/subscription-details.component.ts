import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormGroup } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

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
export class SubscriptionDetailsComponent {

  subscriptionForm!: UntypedFormGroup;
  submitted = false;
  loading = false;
  error = '';
  hide = true;

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  goBack() {
    this.router.navigate(['/', 'subscriptions']);
  }

  onSubmit() {

  }

  onCancel() {
    this.router.navigate(['/', 'subscriptions']);
  }

}
