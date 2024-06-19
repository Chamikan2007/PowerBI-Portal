import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule, MatOption } from '@angular/material/autocomplete';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ResponseDto } from '@core/models/dto/response-dto';
import { ReportDto, SubscriptionDto } from '@core/models/dto/subscription-dto';
import { SubscriptionService } from '@core/service/subscription.service';
import { Observable } from 'rxjs/internal/Observable';
import { map, startWith } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-subscription-details',
  standalone: true,
  imports: [
    RouterLink,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatLabel,
    MatOption,
    AsyncPipe,
    MatInputModule,
    MatAutocompleteModule
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

  reportsList: ReportDto[] = [];
  reportPicker = new FormControl('');
  filteredReports: Observable<ReportDto[]> | undefined;

  model: SubscriptionDto = new SubscriptionDto();

  constructor(
    private formBuilder: UntypedFormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private subscriptionService: SubscriptionService
  ) { }

  ngOnInit(): void {
    this.loadData();

    this.subscriptionForm = this.formBuilder.group({
      reportPath: [this.autocompleteStringValidator(this.reportsList), Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });

    this.filteredReports = this.reportPicker.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }

  autocompleteStringValidator(validOptions: Array<ReportDto>): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (validOptions.indexOf(control.value) !== -1) {
        return null  /* valid option selected */
      }
      return { 'invalidAutocompleteString': { value: control.value } }
    }
  }

  private _filter(value: string): ReportDto[] {
    const filterValue = value.toLowerCase();
    return this.reportsList.filter(report => report.path.toLowerCase().includes(filterValue));
  }

  get f() {
    return this.subscriptionForm.controls;
  }

  loadData() {
    this.reportsList.length = 0;

    this.subscriptionService.getReportsList().subscribe({
      next: (response: ResponseDto) => {
        if (response.isSuccess) {
          this.reportsList = response.data as ReportDto[];
        }
      }
    });
  }

  goBack() {
    this.router.navigate(['/', 'subscriptions']);
  }

  setSelectedReport(event: any) {
    let report = this.reportsList.find(r => r.path === event.value) as ReportDto;
    if (report) {
      this.model.report = report;
    }
    else {
      this.model.report = new ReportDto();
    }
  }

  onSubmit() {
    if (this.subscriptionForm.invalid) {
      this.error = 'Input values are not valid!';
      return;
    }
    else {
      this.model.email = this.f['email'].value;

      this.subscriptionService.createSubscription(this.model.report.path, this.model.email).subscribe({
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
