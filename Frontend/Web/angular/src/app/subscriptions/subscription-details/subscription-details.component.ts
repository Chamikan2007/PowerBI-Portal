import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatAutocompleteModule, MatOption } from '@angular/material/autocomplete';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ResponseDto } from '@core/models/dto/response-dto';
import { ReportDto, SubscriptionDto } from '@core/models/dto/subscription-dto';
import { SubscriptionService } from '@core/service/subscription.service';
import { Observable } from 'rxjs/internal/Observable';
import { map, startWith } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-subscription-details',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatLabel,
    MatOption,
    AsyncPipe,
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

  destinationItems: any[] = [
    { label: 'Windows File Share', value: '1' },
    { label: 'Email', value: '2' },
  ];

  renderFormatItems: any[] = [
    { label: 'Word', value: '1' },
    { label: 'Excel', value: '2' },
    { label: 'PowerPoint', value: '3' },
    { label: 'PDF', value: '4' },
    { label: 'Accessible PDF', value: '5' },
    { label: 'Tiff file', value: '6' },
    { label: 'MHTML(web archive)', value: '7' },
    { label: 'CSV(comma delimited)', value: '8' },
    { label: 'XML file with report data', value: '9' },
    { label: 'Data Feed', value: '10' },
  ];

  priorityItems: any[] = [
    { label: 'Normal', value: '1' },
    { label: 'Low', value: '2' },
    { label: 'High', value: '3' },
  ];

  scheduleTypeItems: any[] = [
    { label: 'Hour', value: '1' },
    { label: 'Day', value: '2' },
    { label: 'Week', value: '3' },
    { label: 'Month', value: '4' },
    { label: 'Once', value: '5' }
  ];

  daysOfWeekItems: any[] = [
    { label: 'Sun', value: '1' },
    { label: 'Mon', value: '2' },
    { label: 'Tue', value: '3' },
    { label: 'Wed', value: '4' },
    { label: 'Thu', value: '5' },
    { label: 'Fri', value: '6' },
    { label: 'Sat', value: '7' },
  ];

  monthsOfYearItems: any[] = [
    { label: 'Jan', value: '1' },
    { label: 'Feb', value: '2' },
    { label: 'Mar', value: '3' },
    { label: 'Apr', value: '4' },
    { label: 'May', value: '5' },
    { label: 'Jun', value: '6' },
    { label: 'Jul', value: '7' },
    { label: 'Aug', value: '8' },
    { label: 'Sep', value: '9' },
    { label: 'Oct', value: '10' },
    { label: 'Nov', value: '11' },
    { label: 'Dec', value: '12' },
  ];

  weekOfMonthItems: any[] = [
    { label: '1st', value: '1' },
    { label: '2nd', value: '2' },
    { label: '3rd', value: '3' },
    { label: '4th', value: '4' },
    { label: 'Last', value: '5' }
  ];

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

  selectedSubscriptionTypeChange(event: any) {
    this.selectedSubscriptionType = event.returnValue;
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
