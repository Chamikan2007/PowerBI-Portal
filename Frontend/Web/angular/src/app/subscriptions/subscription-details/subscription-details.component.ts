import { AbstractControl, FormControl, FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AsyncPipe, CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { map, startWith } from 'rxjs';
import { MatAutocompleteModule, MatOption } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { Observable } from 'rxjs/internal/Observable';
import { ReportDto, SubscriptionDto } from '@core/models/dto/subscription-dto';
import { ResponseDto } from '@core/models/dto/response-dto';
import { SubscriptionDetailsMeta } from './subscription-details.meta';
import { SubscriptionService } from '@core/service/subscription.service';

@Component({
  selector: 'app-subscription-details',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe,
    RouterLink,
    MatButtonModule,
    MatFormFieldModule,
    MatLabel,
    MatOption,
    MatInputModule,
    MatAutocompleteModule,
    MatRadioModule,
    MatSelectModule,
    MatCheckbox,
    MatButtonToggleModule,
    MatDatepickerModule
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

  destinationItems = SubscriptionDetailsMeta.destinationItems;
  renderFormatItems = SubscriptionDetailsMeta.renderFormatItems;
  priorityItems = SubscriptionDetailsMeta.priorityItems;
  scheduleTypeItems = SubscriptionDetailsMeta.scheduleTypeItems;
  daysOfWeekItems = SubscriptionDetailsMeta.daysOfWeekItems;
  monthsOfYearItems = SubscriptionDetailsMeta.monthsOfYearItems;
  weekOfMonthItems = SubscriptionDetailsMeta.weekOfMonthItems;

  selectedSubscriptionType: string = '1';
  selectedDestination: string = '2';
  selectedRenderFormat: string = '1';
  selectedPriority: string = '1';
  selectedScheduleDetailType: string = '2';
  selectedScheduleType: string = '1';
  selectedDailyScheduleType: string = '1';
  selectedMonthlyScheduleType: string = '1';
  selectedWeekOfMonth_WeeklyScheduleType = '1';

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
      reportPath: ['', [this.autocompleteStringValidator(this.reportsList), Validators.required]],
      description: ['', [Validators.required]],
      owner: [{ value: 'SampathBnakHema\Hemantha', disabled: true }, [Validators.required]],
      subscriptionType: ['', Validators.required],
      destinationType: ['', Validators.required],
      comment: [''],
      email_to: ['', [Validators.required, Validators.email]],
      email_cc: ['', [Validators.required, Validators.email]],
      email_bcc: ['', [Validators.required, Validators.email]],
      email_replyto: ['', [Validators.required, Validators.email]],
      email_subject: ['', [Validators.required]],
      includeReportCheckbox: [true, []],
      includeLinkCheckbox: [true, []],
      renderFormat: ['1', [Validators.required]],
      priority: ['1', [Validators.required]],
      scheduleDetailType: ['2', [Validators.required]],
      scheduleType: ['1', [Validators.required]],
      hourly_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      hourly_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      hourly_start_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      hourly_start_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      hourly_start_meridiem: ['1', [Validators.required]],
      hourly_start_date: [new Date(), [Validators.required]],
      hourly_end_date: [null, []],
      daily_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      daily_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      daily_start_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      daily_start_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      daily_start_meridiem: ['1', [Validators.required]],
      daily_start_date: [new Date(), [Validators.required]],
      daily_end_date: [null, []],
      weekly_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      weekly_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      weekly_start_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      weekly_start_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      weekly_start_meridiem: ['1', [Validators.required]],
      weekly_start_date: [new Date(), [Validators.required]],
      weekly_end_date: [null, []],
      monthly_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      monthly_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      monthly_start_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      monthly_start_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      monthly_start_meridiem: ['1', [Validators.required]],
      monthly_start_date: [new Date(), [Validators.required]],
      monthly_end_date: [null, []],
      onetime_start_hour: ['00', [Validators.required, Validators.min(0), Validators.max(23)]],
      onetime_start_minute: ['00', [Validators.required, Validators.min(0), Validators.max(59)]],
      onetime_start_meridiem: ['1', [Validators.required]],
      dailyScheduleType: ['1', []],
      monthlyScheduleType: ['1', []],
      // daily_after_days_count: [0, []],
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

  onDescriptionChange(event: any) {
    event.target.value;
  }

  selectedSubscriptionTypeChange(event: any) {
    this.selectedSubscriptionType = event.returnValue;
  }

  onDeliveryOptionEmailToChange(event: any) {
    event.target.value;
  }

  onDeliveryOptionEmailCcChange(event: any) {
    event.target.value;
  }

  onDeliveryOptionEmailBccChange(event: any) {
    event.target.value;
  }

  onDeliveryOptionEmailReplyToChange(event: any) {
    event.target.value;
  }

  onDeliveryOptionEmailSubjectChange(event: any) {
    event.target.value;
  }

  onCommentTextChange(event: any) {
    event.target.value;
  }

  selectedScheduleDetailTypeChange(event: any) {
    this.selectedScheduleDetailType = event.value;
  }

  scheduleTypeChange(event: any) {
    this.selectedScheduleType = event.value;
  }

  selectedDailyScheduleTypeChange(event: any) {
    if (event.value) {
      this.selectedDailyScheduleType = event.value;
    }
  }

  selectedMonthlyScheduleTypeChange(event: any) {
    if (event.value) {
      this.selectedMonthlyScheduleType = event.value;
    }
  }

  onHourlyMeridiemChange(event: any) {
    event.value;
  }

  onHourlyRunScheduleEveryHourChange(event: any) {
    event.target.value;
  }

  onHourlyRunScheduleEveryMinuteChange(event: any) {
    event.target.value;
  }

  onDailyMeridiemChange(event: any) {
    event.value;
  }

  onWeeklyMeridiemChange(event: any) {
    event.value;
  }

  onMonthlyMeridiemChange(event: any) {
    event.value;
  }

  onOneTimeMeridiemChange(event: any) {
    event.value;
  }

  validateFormInputs() {
    let isValid = true;

    return isValid;
  }

  onSubmit() {
    let isValid = this.validateFormInputs();

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
