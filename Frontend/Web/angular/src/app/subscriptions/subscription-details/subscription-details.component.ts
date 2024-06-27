import { AbstractControl, FormControl, FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AsyncPipe, CommonModule, KeyValue } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { map, startWith } from 'rxjs';
import { MatAutocompleteModule, MatOption } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@danielmoncada/angular-datetime-picker';
import { MatFormFieldModule, MatLabel } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { Observable } from 'rxjs/internal/Observable';
import { ReportDto, ScheduleTypeDailyDto, ScheduleTypeHourlyDto, ScheduleTypeMonthlyDto, ScheduleTypeOneTimeDto, ScheduleTypeWeeklyDto, SubscriptionDto } from '@core/models/dto/subscription-dto';
import { ResponseDto } from '@core/models/dto/response-dto';
import { SubscriptionDetailsMeta } from './subscription-details.meta';
import { SubscriptionService } from '@core/service/subscription.service';
import { MatIconModule } from '@angular/material/icon';

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
    MatDatepickerModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    MatIconModule
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

  subscriptionId: string = '0';
  model: SubscriptionDto = new SubscriptionDto();
  isReadOnly: boolean = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private subscriptionService: SubscriptionService
  ) { }

  ngOnInit(): void {

    this.activatedRoute.url.subscribe({
      next: (urlSegments: any) => {
        this.subscriptionId = urlSegments[0].path;
        this.loadData();
      }
    });

    this.subscriptionForm = this.formBuilder.group({
      reportPath: ['', [this.autocompleteStringValidator(this.reportsList), Validators.required]],
      description: ['', [Validators.required]],
      owner: [{ value: 'SampathBnakHema\\Hemantha', disabled: true }, [Validators.required]],
      subscriptionType: ['', Validators.required],
      destinationType: ['', Validators.required],
      comment: [''],
      email_to: ['', [Validators.required, Validators.email]],
      email_cc: ['', [Validators.email]],
      email_bcc: ['', [Validators.email]],
      email_replyto: ['', [Validators.email]],
      email_subject: ['', [Validators.required]],
      includeReportCheckbox: [true, []],
      includeLinkCheckbox: [true, []],
      renderFormat: ['1', [Validators.required]],
      priority: ['1', [Validators.required]],
      scheduleDetailType: ['2', [Validators.required]],
      scheduleType: ['1', [Validators.required]],
      hourly_run_time: ['00:00 AM', [Validators.required]],
      hourly_start_time: ['00:00 AM', [Validators.required]],
      hourly_start_date: [new Date(), [Validators.required]],
      hourly_end_date: [null, []],
      daily_start_time: ['00:00 AM', [Validators.required]],
      daily_start_date: [new Date(), [Validators.required]],
      daily_end_date: [null, []],
      weekly_start_time: ['00:00 AM', [Validators.required]],
      weekly_start_date: [new Date(), [Validators.required]],
      weekly_end_date: [null, []],
      monthly_start_time: ['00:00 AM', [Validators.required]],
      monthly_start_date: [new Date(), [Validators.required]],
      monthly_end_date: [null, []],
      onetime_start_time: ['00:00 AM', [Validators.required]],
      dailyScheduleType: ['1', []],
      monthlyScheduleType: ['1', []],
    });

    this.filteredReports = this.reportPicker.valueChanges.pipe(
      startWith(''),
      map((value: any) => this._filter(value || '')),
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
    this.isReadOnly = false;

    if (this.subscriptionId === '0') {
      // create mode
      this.subscriptionService.getReportsList().subscribe({
        next: (response: ResponseDto) => {
          if (response.isSuccess) {
            this.reportsList = response.data as ReportDto[];
          }
        }
      });
    }
    else {
      // view mode
      this.isReadOnly = true;

      this.subscriptionService.getSubscriptionById(this.subscriptionId).subscribe({
        next: (response: any) => {
          this.model = response as SubscriptionDto;
        }
      });
    }
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
    this.model.description = event.target.value;
  }

  onSubscriptionTypeChange(event: any) {
    this.model.subscriptionType = +event.returnValue;
  }

  onDestinationChange(event: any) {
    this.model.destination = +event.value;
  }

  // email options
  onDeliveryOptionEmailToChange(event: any) {
    this.model.deliveryOptionEmail.to = event.target.value;
  }

  onDeliveryOptionEmailCcChange(event: any) {
    this.model.deliveryOptionEmail.cc = event.target.value;
  }

  onDeliveryOptionEmailBccChange(event: any) {
    this.model.deliveryOptionEmail.bcc = event.target.value;
  }

  onDeliveryOptionEmailReplyToChange(event: any) {
    this.model.deliveryOptionEmail.replyTo = event.target.value;
  }

  onDeliveryOptionEmailSubjectChange(event: any) {
    this.model.deliveryOptionEmail.subject = event.target.value;
  }

  onIncludeReportChange(event: boolean) {
    this.model.deliveryOptionEmail.includeReport = event;
  }

  onIncludeLinkChange(event: boolean) {
    this.model.deliveryOptionEmail.inlcudeLink = event;
  }

  onRenderFormatChange(event: any) {
    this.model.deliveryOptionEmail.renderFormat = +event.value;
  }

  onPriorityChange(event: any) {
    this.model.deliveryOptionEmail.priority = +event.value;
  }

  onCommentTextChange(event: any) {
    this.model.deliveryOptionEmail.comment = event.target.value;
  }

  // schedule detail type
  onScheduleDetailTypeChange(event: any) {
    this.model.scheduleDetailType = +event.value;
  }

  // schedule types
  onScheduleTypeChange(event: any) {
    this.model.scheduleType = +event.value;
  }

  // hourly
  onHourlyRunScheduleEveryTimeChange(event: any) {
    let dt = event.value as Date;
    this.model.schedule.scheduleDetailHourly.hour = dt.getHours();
    this.model.schedule.scheduleDetailHourly.minute = dt.getMinutes();
  }

  onHourlyRunScheduleStartTimeChange(event: any) {
    let dt = event.value as Date;
    this.model.schedule.scheduleDetailHourly.startHour = dt.getHours();
    this.model.schedule.scheduleDetailHourly.startMinute = dt.getMinutes();
    this.model.schedule.scheduleDetailHourly.startDateTime = this._createStartDateTime(this.model.schedule.scheduleDetailHourly.startDateTime, this.model.schedule.scheduleDetailHourly.startHour, this.model.schedule.scheduleDetailHourly.startMinute);
  }


  // daily 
  onDailyScheduleTypeChange(event: any) {
    if (event.value) {
      this.model.schedule.scheduleDetailDaily.dailyScheduleType = +event.value;
    }
  }

  onDailyScheduleDayStatusChange(event: boolean, opt: KeyValue<string, string>) {
    let day = this.model.schedule.scheduleDetailDaily.selectedDays.find((d) => d.key === opt.value);
    day!.value = event;
  }

  onDailyRunScheduleRepeatAfterDayCountChange(event: any) {
    this.model.schedule.scheduleDetailDaily.repeatAfterDaysCount = +event.target.value;
  }

  onDailyRunScheduleStartTimeChange(event: any) {
    let dt = event.value as Date;
    this.model.schedule.scheduleDetailDaily.startHour = dt.getHours();
    this.model.schedule.scheduleDetailDaily.startMinute = dt.getMinutes();
    this.model.schedule.scheduleDetailDaily.startDateTime = this._createStartDateTime(this.model.schedule.scheduleDetailHourly.startDateTime, this.model.schedule.scheduleDetailHourly.startHour, this.model.schedule.scheduleDetailHourly.startMinute);
  }

  // weekly
  onWeeklyRunScheduleRepeatAfterDayCountChange(event: any) {
    this.model.schedule.scheduleDetailWeekly.repeatAfterDaysCount = +event.target.value;
  }

  onWeeklyScheduleDayStatusChange(event: boolean, opt: KeyValue<string, string>) {
    let day = this.model.schedule.scheduleDetailWeekly.selectedDays.find((d) => d.key === opt.value);
    day!.value = event;
  }

  onWeeklyRunScheduleStartTimeChange(event: any) {
    let dt = event.value as Date;
    this.model.schedule.scheduleDetailWeekly.startHour = dt.getHours();
    this.model.schedule.scheduleDetailWeekly.startMinute = dt.getMinutes();
    this.model.schedule.scheduleDetailWeekly.startDateTime = this._createStartDateTime(this.model.schedule.scheduleDetailHourly.startDateTime, this.model.schedule.scheduleDetailHourly.startHour, this.model.schedule.scheduleDetailHourly.startMinute);
  }

  // monthly
  onMonthlyScheduleMonthStatusChange(event: boolean, opt: KeyValue<string, string>) {
    let month = this.model.schedule.scheduleDetailMonthly.selectedMonths.find((d) => d.key === opt.value);
    month!.value = event;
  }

  onMonthlyScheduleTypeChange(event: any) {
    if (event.value) {
      this.model.schedule.scheduleDetailMonthly.monthlyScheduleType = +event.value;
    }
  }

  onMonthlyScheduleWeekOfMonthChange(event: any) {
    this.model.schedule.scheduleDetailMonthly.onWeekOfMonth = +event.value;
  }

  onMonthlyScheduleOnDayOfWeekChange(event: boolean, opt: KeyValue<string, string>) {
    let day = this.model.schedule.scheduleDetailMonthly.onDaysOfWeek.find((d) => d.key === opt.value);
    day!.value = event;
  }

  onMonthlyScheduleOnCalendarDaysChange(event: any) {
    this.model.schedule.scheduleDetailMonthly.onCalendarDays = event.target.value;
  }

  onMonthlyRunScheduleStartTimeChange(event: any) {
    let dt = event.value as Date;
    this.model.schedule.scheduleDetailMonthly.startHour = dt.getHours();
    this.model.schedule.scheduleDetailMonthly.startMinute = dt.getMinutes();
    this.model.schedule.scheduleDetailMonthly.startDateTime = this._createStartDateTime(this.model.schedule.scheduleDetailHourly.startDateTime, this.model.schedule.scheduleDetailHourly.startHour, this.model.schedule.scheduleDetailHourly.startMinute);
  }

  // one-time

  onOneTimeRunScheduleStartTimeChange(event: any) {
    let dt = event.value as Date;
    this.model.schedule.scheduleDetailOneTime.startHour = dt.getHours();
    this.model.schedule.scheduleDetailOneTime.startMinute = dt.getMinutes();
    this.model.schedule.scheduleDetailOneTime.startDateTime = this._createStartDateTime(this.model.schedule.scheduleDetailHourly.startDateTime, this.model.schedule.scheduleDetailHourly.startHour, this.model.schedule.scheduleDetailHourly.startMinute);
  }

  _createStartDateTime(startDateTime: Date, startHour: number, startMinute: number): Date {
    let startDate = new Date(startDateTime);
    startDate.setHours(startHour);
    startDate.setMinutes(startMinute);

    return startDate;
  }

  // start and end date
  onStartDateChange(event: any) {
    switch (this.model.scheduleType) {
      case 1:
        this.model.schedule.scheduleDetailHourly.startDateTime = this._createStartDateTime(event.value._d, this.model.schedule.scheduleDetailHourly.startHour, this.model.schedule.scheduleDetailHourly.startMinute);
        break;
      case 2:
        this.model.schedule.scheduleDetailDaily.startDateTime = this._createStartDateTime(event.value._d, this.model.schedule.scheduleDetailDaily.startHour, this.model.schedule.scheduleDetailDaily.startMinute);
        break;
      case 3:
        this.model.schedule.scheduleDetailWeekly.startDateTime = this._createStartDateTime(event.value._d, this.model.schedule.scheduleDetailWeekly.startHour, this.model.schedule.scheduleDetailWeekly.startMinute);
        break;
      case 4:
        this.model.schedule.scheduleDetailMonthly.startDateTime = this._createStartDateTime(event.value._d, this.model.schedule.scheduleDetailMonthly.startHour, this.model.schedule.scheduleDetailMonthly.startMinute);
        break;
      case 5:
        this.model.schedule.scheduleDetailOneTime.startDateTime = this._createStartDateTime(event.value._d, this.model.schedule.scheduleDetailOneTime.startHour, this.model.schedule.scheduleDetailOneTime.startMinute);
        break;
    }
  }

  validateFormInputs() {
    let isValid = true;

    return isValid;
  }

  onSubmit() {
    let isValid = this.validateFormInputs();

    this.subscriptionService.createSubscription(this.model).subscribe({
      next: (response: ResponseDto) => {
        debugger;
        if (response.isSuccess) {
          this.router.navigate(['../'], { relativeTo: this.activatedRoute });
        }
        else {
          this.error = response.error.errorCode;
        }
      },
      error: (error: any) => {
        debugger;
        this.error = 'Something went wrong.';
      }
    });
  }

  onCancel() {
    this.router.navigate(['/', 'subscriptions']);
  }
} 
