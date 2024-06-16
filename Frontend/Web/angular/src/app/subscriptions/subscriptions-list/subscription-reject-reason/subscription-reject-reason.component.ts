import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ApiService } from '@core/service/api-service.service';
import { AuthService } from '@core/service/auth.service';
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
    private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private apiService: ApiService,
  ) {
    super();
  }

  ngOnInit(): void {

  }

  onSubmitRejection() {



  }



}
