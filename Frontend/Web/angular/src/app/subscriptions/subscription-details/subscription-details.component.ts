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
import { ReportDto, SubscriptionDto } from '@core/models/dto/subscription-dto';
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
      // reportPath: ['ssssss', [this.autocompleteStringValidator(this.reportsList), Validators.required]],
      description: ['', [Validators.required]],
      owner: [{ value: '', disabled: true }, [Validators.required]],
      subscriptionType: [0, Validators.required],
      destinationType: [1, Validators.required],
      email_to: ['', [Validators.required, Validators.email]],
      email_cc: ['', [Validators.email]],
      email_bcc: ['', [Validators.email]],
      email_replyto: ['', [Validators.email]],
      email_subject: ['', [Validators.required]],
      includeReportCheckbox: [true, []],
      includeLinkCheckbox: [true, []],
      renderFormat: [0, [Validators.required]],
      priority: [0, [Validators.required]],
      comment: [''],
      scheduleDetailType: [1, [Validators.required]],
      scheduleType: [1, [Validators.required]],
      hourly_hour: ['', [Validators.required]],
      hourly_minute: ['', [Validators.required]],
      // hourly_run_time: ['00:00 AM', [Validators.required]],
      hourly_start_time: ['', [Validators.required]],
      hourly_start_date: [new Date(), [Validators.required]],
      stop_date: [{ value: '', disabled: true }],
      hourly_end_date: [null, []],
      daily_start_time: ['', [Validators.required]],
      daily_start_date: [new Date(), [Validators.required]],
      daily_end_date: [null, []],
      weekly_start_time: ['', [Validators.required]],
      weekly_start_date: [new Date(), [Validators.required]],
      weekly_end_date: [null, []],
      monthly_start_time: ['', [Validators.required]],
      monthly_start_date: [new Date(), [Validators.required]],
      monthly_end_date: [null, []],
      onetime_start_time: ['', [Validators.required]],
      dailyScheduleType: [0, []],
      monthlyScheduleType: [0, []],
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

    if (this.subscriptionId === '0') {
      // create mode
      this.isReadOnly = false;

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
    window.history.back();
    // this.router.navigate(['/', 'subscriptions']);
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
    this.model.subscriptionType = +event.target.value;
  }

  onDestinationChange(event: any) {
    this.model.destination = +event.value;
  }

  // email options
  onDeliveryOptionEmailToChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.to = event.target.value;
  }

  onDeliveryOptionEmailCcChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.cc = event.target.value;
  }

  onDeliveryOptionEmailBccChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.bcc = event.target.value;
  }

  onDeliveryOptionEmailReplyToChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.replyTo = event.target.value;
  }

  onDeliveryOptionEmailSubjectChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.subject = event.target.value;
  }

  onIncludeReportChange(event: boolean) {
    this.model.deliveryOption.emailDeliveryOption.includeReport = event;
  }

  onIncludeLinkChange(event: boolean) {
    this.model.deliveryOption.emailDeliveryOption.includeLink = event;
  }

  onRenderFormatChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.renderFormat = +event.value;
  }

  onPriorityChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.priority = +event.value;
  }

  onCommentTextChange(event: any) {
    this.model.deliveryOption.emailDeliveryOption.comment = event.target.value;
  }

  // schedule detail type
  onScheduleDetailTypeChange(event: any) {
    this.model.scheduleDetailType = +event.value;
  }

  // schedule types
  onScheduleTypeChange(event: any) {
    this.model.scheduleType = +event.value;
  }

  /* 
   * hourly Schedular running here
   * @params, hours, minutes, startDateTime
   * additional @params, startHour, startMinute
   * methods onHourlyRunScheduleEveryHourChange, onHourlyRunScheduleEveryMinuteChange, onHourlyRunScheduleStartTimeChange
   */
  onHourlyRunScheduleEveryHourChange(event: any) {
    this.model.schedule.hourlySchedule.hours = +event.target.value;
  }

  onHourlyRunScheduleEveryMinuteChange(event: any) {
    this.model.schedule.hourlySchedule.minutes = +event.target.value;
  }

  onHourlyRunScheduleStartTimeChange(event: any) {
    this.model.schedule.hourlySchedule.selectedTime = new Date(event.value) as Date;
  }

  // onHourlyRunScheduleEveryTimeChange(event: any) {
  //   let dt = event.value as Date;
  //   this.model.schedule.hourlySchedule.hours = dt.getHours();
  //   this.model.schedule.hourlySchedule.minutes = dt.getMinutes();
  // }


  /* 
   * Daily Schedular running here
   * @params, hours, minutes, startDateTime
   * additional @params, startHour, startMinute
   * methods onHourlyRunScheduleEveryHourChange, onHourlyRunScheduleEveryMinuteChange, onHourlyRunScheduleStartTimeChange
   */
  onDailyScheduleTypeChange(event: any) {
    if (event.value) {
      this.model.schedule.dailySchedule.dailyScheduleType = +event.value;
      if ( event.value == 0 ) {
        this.model.schedule.dailySchedule.repeatIn = 0;
        this.model.schedule.dailySchedule.days = Array.from(SubscriptionDetailsMeta.daysOfWeekItems.map(d => ({ key: d.key, value: true }) as KeyValue<number, boolean>));
        this.model.schedule.dailySchedule.everyWeekDay = false;
      } else if (event.value == 1) {
       this.model.schedule.dailySchedule.everyWeekDay = true;
        this.model.schedule.dailySchedule.repeatIn = 0;
        this.model.schedule.dailySchedule.days = [];
      } else if ( event.value == 2 ) {
        this.model.schedule.dailySchedule.days = [];
        this.model.schedule.dailySchedule.everyWeekDay = true;
      }
    }
  }

  onDailyScheduleDayStatusChange(event: boolean, opt: KeyValue<number, string>) {
    this.model.schedule.dailySchedule.days[opt.key].value = event;
  }

  onDailyRunScheduleRepeatAfterDayCountChange(event: any) {
    this.model.schedule.dailySchedule.repeatIn = +event.target.value;
  }

  onDailyRunScheduleStartTimeChange(event: any) {
    this.model.schedule.dailySchedule.selectedTime = new Date(event.value) as Date;
  }

  /* 
   * Weekly Schedular running here
   * @params, hours, minutes, startDateTime
   * additional @params, startHour, startMinute
   * methods onHourlyRunScheduleEveryHourChange, onHourlyRunScheduleEveryMinuteChange, onHourlyRunScheduleStartTimeChange
   */
  onWeeklyRunScheduleRepeatAfterDayCountChange(event: any) {
    this.model.schedule.weeklySchedule.repeatAfterDaysCount = +event.target.value;
  }

  onWeeklyScheduleDayStatusChange(event: boolean, opt: KeyValue<number, string>) {
    this.model.schedule.weeklySchedule.selectedDays[opt.key].value = event;
    // let day = this.model.schedule.weeklySchedule.selectedDays.find((d) => d.key === opt.value);
    // day!.value = event;
  }

  onWeeklyRunScheduleStartTimeChange(event: any) {
    this.model.schedule.weeklySchedule.selectedTime = new Date(event.value) as Date;
  }

  /* 
   * Monthly Schedular running here
   * @params, hours, minutes, startDateTime
   * additional @params, startHour, startMinute
   * methods onHourlyRunScheduleEveryHourChange, onHourlyRunScheduleEveryMinuteChange, onHourlyRunScheduleStartTimeChange
   */
  onMonthlyScheduleMonthStatusChange(event: boolean, opt: KeyValue<number, string>) {
    this.model.schedule.monthlySchedule.selectedDays[opt.key].value = event;
    // let month = this.model.schedule.monthlySchedule.selectedMonths.find((d) => d.key === opt.value);
    // month!.value = event;
  }

  onMonthlyScheduleTypeChange(event: any) {
    if (event.value) {
      this.model.schedule.monthlySchedule.monthlyScheduleType = +event.value;
    }
  }

  onMonthlyScheduleWeekOfMonthChange(event: any) {
    this.model.schedule.monthlySchedule.onWeekOfMonth = +event.value;
  }

  onMonthlyScheduleOnDayOfWeekChange(event: boolean, opt: KeyValue<number, string>) {
    this.model.schedule.monthlySchedule.onDaysOfWeek[opt.key].value = event;
  }

  onMonthlyScheduleOnCalendarDaysChange(event: any) {
    this.model.schedule.monthlySchedule.onCalendarDays = event.target.value;
  }

  onMonthlyRunScheduleStartTimeChange(event: any) {
    this.model.schedule.monthlySchedule.selectedTime = new Date(event.value) as Date;
  }

  /* 
   * one-time Schedular running here
   * @params, hours, minutes, startDateTime
   * additional @params, startHour, startMinute
   * methods onOneTimeRunScheduleStartTimeChange
   */
  onOneTimeRunScheduleStartTimeChange(event: any) {
    this.model.schedule.oneTimeSchedule.selectedTime = new Date(event.value) as Date;
  }

  _createStartDateTime(startDateTime: Date, startHour: number, startMinute: number): Date {
    let startDate = new Date(startDateTime);
    startDate.setHours(startHour);
    startDate.setMinutes(startMinute);
    return startDate;
  }

  // start and end date
  onStartDateChange(event: any) {
    let dt = new Date(event.value) as Date;
    let selectedTime;
    
    switch (this.model.scheduleType) {
      case 1:
        selectedTime = new Date(this.model.schedule.hourlySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime', this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes()));
        this.model.schedule.hourlySchedule.startDateTime =  this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 2:
        selectedTime = new Date(this.model.schedule.dailySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.dailySchedule.startDateTime = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 3:
        selectedTime = new Date(this.model.schedule.weeklySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.weeklySchedule.startDateTime = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 4:
        selectedTime = new Date(this.model.schedule.monthlySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.monthlySchedule.startDateTime = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 5:
        selectedTime = new Date(this.model.schedule.oneTimeSchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.oneTimeSchedule.startDateTime = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
    }
  }

  // StopDate Enable
  onHourlyRunScheduleIsEnabledStopTimeChange(event: any) {
    this.model.isDisabledStopDate = !event;
    event ? this.subscriptionForm.get('stop_date')?.enable() : this.subscriptionForm.get('stop_date')?.disable();
  }

  // stop and end date
  onStopDateChange(event: any) {
    let dt = new Date(event.value) as Date;
    let selectedTime;
    
    switch (this.model.scheduleType) {
      case 1:
        selectedTime = new Date(this.model.schedule.hourlySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.hourlySchedule.stoptDate = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 2:
        selectedTime = new Date(this.model.schedule.dailySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.dailySchedule.stoptDate = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 3:
        selectedTime = new Date(this.model.schedule.weeklySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.weeklySchedule.stoptDate = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 4:
        selectedTime = new Date(this.model.schedule.monthlySchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.monthlySchedule.stoptDate = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
      case 5:
        selectedTime = new Date(this.model.schedule.oneTimeSchedule.selectedTime);
        console.log(selectedTime, 'selectedTime');
        this.model.schedule.oneTimeSchedule.stoptDate = this._createStartDateTime(new Date(this.getFormattedDate(dt)), selectedTime.getHours(), selectedTime.getMinutes());
        break;
    }
  }

  validateFormInputs(formGroup: UntypedFormGroup) {
  //   Object.keys(this.subscriptionForm.controls).forEach(field => {
  //     const control = this._createStartDateTime.subscriptionForm.get(field);
  //     if (control instanceof UntypedFormGroup) {
  //       this.validateFormInputs(control);
  //     } else {
  //       control.markAsTouched({ onlySelf: true });
  //     }
  //   });
  }

  onSubmit() {
    // let isValid = this.validateFormInputs();

    // if (this.subscriptionForm.valid) {

      switch (this.model.scheduleType) {
        case 1: // hourly
          this.model.schedule.dailySchedule = null; // daily
          this.model.schedule.weeklySchedule = null; // weekly
          this.model.schedule.monthlySchedule = null; // monthly
          this.model.schedule.oneTimeSchedule = null; // onetime
          if(this.model.isDisabledStopDate) this.model.schedule.hourlySchedule.stoptDate = null;
          break;
        case 2: // daily
          this.model.schedule.hourlySchedule = null; // hourly
          this.model.schedule.weeklySchedule = null; // weekly
          this.model.schedule.monthlySchedule = null; // monthly
          this.model.schedule.oneTimeSchedule = null; // onetime
          if(this.model.isDisabledStopDate) this.model.schedule.dailySchedule.stoptDate = null;
          break;
        case 3: // weekly
          this.model.schedule.hourlySchedule = null; // hourly
          this.model.schedule.dailySchedule = null; // daily
          this.model.schedule.monthlySchedule = null; // monthly
          this.model.schedule.oneTimeSchedule = null; // onetime
          if(this.model.isDisabledStopDate) this.model.schedule.weeklySchedule.stoptDate = null;
          break;
        case 4: // monthly
          this.model.schedule.hourlySchedule = null; // hourly
          this.model.schedule.dailySchedule = null; // daily
          this.model.schedule.weeklySchedule = null; // weekly
          this.model.schedule.oneTimeSchedule = null; // onetime
          if(this.model.isDisabledStopDate) this.model.schedule.monthlySchedule.stoptDate = null;
          break;
        case 5: // onetime
          this.model.schedule.hourlySchedule = null; // hourly
          this.model.schedule.dailySchedule = null; // daily
          this.model.schedule.weeklySchedule = null; // weekly
          this.model.schedule.monthlySchedule = null; // monthly
          if(this.model.isDisabledStopDate) this.model.schedule.oneTimeSchedule.stoptDate = null;
          break;
      }
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
    // } else {
    //   console.error('Fields are Mandatory');
    // }
  }

  onCancel() {
    window.history.back();
    // this.router.navigate(['/', 'subscriptions']);
  }

  // read-only mode related
  getDestinationText(destination: number) {
    let text = '';
    let item = this.destinationItems.find(d => d.key === destination);

    if (item) {
      text = item.value;
    }
    return text;
  }

  getNumberInDigits(value: number, length: number) {
    return value.toString().padStart(length, '0');
  }

  isKeySelected(key: string, keyValueList: KeyValue<string, boolean>[]) {
    if (key) {
      let keyValueItem = keyValueList.find(d => d.key == key);
      if (keyValueItem) {
        return keyValueItem.value == true;
      }
    }
    return false;
  }

  getFormattedTime(value?: Date) {
    if (value) {
      let time = `${value.getHours().toString().padStart(2, '0')}:${value.getMinutes().toString().padStart(2, '0')}`;
      return `${time}`;
    }
    return '';
  }

  getFormattedDate(value?: Date) {
    if (value) {
      let date = `${value.getFullYear().toString().padStart(4, '0')}-${value.getMonth().toString().padStart(2, '0')}-${value.getDate().toString().padStart(2, '0')}`;
      return `${date}`;
    }
    return '';
  }

  getFormattedScheduleStartDate() {
    let date;
    switch (this.model.scheduleType) {
      case 1:
        date = this.model.schedule.hourlySchedule.startDateTime;
        break;
      case 2:
        date = this.model.schedule.dailySchedule.startDateTime;
        break;
      case 3:
        date = this.model.schedule.weeklySchedule.startDateTime;
        break;
      case 4:
        date = this.model.schedule.monthlySchedule.startDateTime;
        break;
      case 5:
        date = this.model.schedule.oneTimeSchedule.startDateTime;
        break;
    }
    return this.getFormattedDate(date);
  }

  getFormattedScheduleEndDate() {
    let date;
    switch (this.model.scheduleType) {
      case 1:
        date = this.model.schedule.hourlySchedule.stoptDate;
        break;
      case 2:
        date = this.model.schedule.dailySchedule.endDate;
        break;
      case 3:
        date = this.model.schedule.weeklySchedule.endDate;
        break;
      case 4:
        date = this.model.schedule.monthlySchedule.endDate;
        break;
      case 5:
        date = this.model.schedule.oneTimeSchedule.endDate;
        break;
    }
    return this.getFormattedDate(date);
  }

  isScheduleEndDateSet() {
    let date = null;
    switch (this.model.scheduleType) {
      case 1:
        date = this.model.schedule.hourlySchedule.stoptDate;
        break;
      case 2:
        date = this.model.schedule.dailySchedule.endDate;
        break;
      case 3:
        date = this.model.schedule.weeklySchedule.endDate;
        break;
      case 4:
        date = this.model.schedule.monthlySchedule.endDate;
        break;
      case 5:
        date = this.model.schedule.oneTimeSchedule.endDate;
        break;
    }
    return (date != null);
  }

}
