import { Component, ViewChild, OnInit } from '@angular/core';
import {
  CalendarOptions,
  DateSelectArg,
  EventClickArg,
  EventApi,
} from '@fullcalendar/core';
import { EventInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import timeGridPlugin from '@fullcalendar/timegrid';
import listPlugin from '@fullcalendar/list';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import {
  UntypedFormBuilder,
  UntypedFormGroup,
  Validators,
} from '@angular/forms';
import { Calendar } from './calendar.model';
import { FormDialogComponent } from './dialogs/form-dialog/form-dialog.component';
import { CalendarService } from './calendar.service';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox';
import { UnsubscribeOnDestroyAdapter } from '@shared/UnsubscribeOnDestroyAdapter';
import { Direction } from '@angular/cdk/bidi';
import { FullCalendarModule } from '@fullcalendar/angular';
import { MatButtonModule } from '@angular/material/button';
import { BreadcrumbComponent } from '@shared/components/breadcrumb/breadcrumb.component';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from '@danielmoncada/angular-datetime-picker';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss'],
  standalone: true,
  imports: [
    BreadcrumbComponent,
    MatButtonModule,
    MatCheckboxModule,
    FullCalendarModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    MatDialogModule
  ],
})
export class CalendarComponent
  extends UnsubscribeOnDestroyAdapter
  implements OnInit {
  @ViewChild('calendar', { static: false })
  calendar: Calendar | null;
  public addCusForm: UntypedFormGroup;
  dialogTitle: string;
  filterOptions = 'All';
  calendarData!: Calendar;
  filterItems: string[] = [
    'work',
    'personal',
    'important',
    'travel',
    'friends',
  ];

  calendarEvents?: EventInput[];
  tempEvents?: EventInput[];

  public filters = [
    { name: 'work', value: 'Work', checked: true },
    { name: 'personal', value: 'Personal', checked: true },
    { name: 'important', value: 'Important', checked: true },
    { name: 'travel', value: 'Travel', checked: true },
    { name: 'friends', value: 'Friends', checked: true },
  ];

  breadscrums = [
    {
      title: 'Calendar',
      items: [],
      active: 'Calendar',
    },
  ];

  constructor(
    private fb: UntypedFormBuilder,
    private dialog: MatDialog,
    public calendarService: CalendarService,
    private snackBar: MatSnackBar
  ) {
    super();
    this.dialogTitle = 'Add New Event';
    const blankObject = {} as Calendar;
    this.calendar = new Calendar(blankObject);
    this.addCusForm = this.createCalendarForm(this.calendar);
  }

  public ngOnInit(): void {
    this.getEvents();
  }

  getEvents(): void {
    this.calendarService.getAllCalendars().subscribe(
      (data: Calendar[]) => {
        this.calendarEvents = data;
        this.tempEvents = this.calendarEvents;
        this.calendarOptions.events = data.map(
          evt => {
            return {
              id: evt.id,
              title: evt.title,
              start: evt.start,
              end: evt.end,
              className: evt.className,
              groupId: evt.groupId,
              details: evt['details']
            }
          })
      },
      (error) => {
        console.error('Error fetching events:', error);
      }
    );
  }


  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin, timeGridPlugin, listPlugin, interactionPlugin],
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek',
    },
    initialView: 'dayGridMonth',
    weekends: true,
    editable: true,
    selectable: true,
    selectMirror: true,
    dayMaxEvents: true,
    select: this.handleDateSelect.bind(this),
    eventClick: this.handleEventClick.bind(this),
    eventsSet: this.handleEvents.bind(this),
  };

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  handleDateSelect(selectInfo: DateSelectArg) {
    this.addNewEvent();
  }

  addNewEvent() {
    let tempDirection: Direction;
    if (localStorage.getItem('isRtl') === 'true') {
      tempDirection = 'rtl';
    } else {
      tempDirection = 'ltr';
    }
    const dialogRef = this.dialog.open(FormDialogComponent, {
      data: {
        calendar: this.calendar,
        action: 'add',
      },
      direction: tempDirection,
    });

    this.subs.sink = dialogRef.afterClosed().subscribe((result) => {
      if (result === 'submit') {
        this.calendarData = this.calendarService.getDialogData();
        console.log(this.calendarData.start);
        this.calendarEvents = this.calendarEvents?.concat({
          // add new event data. must create new array
          id: this.calendarData.id,
          title: this.calendarData.title,
          start: this.calendarData.start,
          end: this.calendarData.end,
          className: this.getClassNameValue(this.calendarData.className),
          groupId: this.calendarData.groupId,
          details: this.calendarData.details,
        });
        this.calendarOptions.events = this.calendarEvents;
        this.addCusForm.reset();
        this.showNotification(
          'snackbar-success',
          'Add Record Successfully...!!!',
          'bottom',
          'center'
        );
      }
    });
  }

  changeCategory(event: MatCheckboxChange, filter: { name: string }) {
    if (event.checked) {
      this.filterItems.push(filter.name);
    } else {
      this.filterItems.splice(this.filterItems.indexOf(filter.name), 1);
    }
    this.filterEvent(this.filterItems);
  }

  filterEvent(element: string[]) {
    const list = this.calendarEvents?.filter((x) =>
      element.map((y?: string) => y).includes(x.groupId)
    );

    this.calendarOptions.events = list;
  }

  handleEventClick(clickInfo: EventClickArg) {
    this.eventClick(clickInfo);
  }

  eventClick(row: EventClickArg) {
    const calendarData = {
      id: row.event.id,
      title: row.event.title,
      groupId: row.event.groupId,
      start: row.event.start,
      end: row.event.end,
      details: row.event.extendedProps['details'],
    };

    let tempDirection: Direction;
    if (localStorage.getItem('isRtl') === 'true') {
      tempDirection = 'rtl';
    } else {
      tempDirection = 'ltr';
    }

    const dialogRef = this.dialog.open(FormDialogComponent, {
      data: {
        calendar: calendarData,
        action: 'edit',
      },
      direction: tempDirection,
    });

    this.subs.sink = dialogRef.afterClosed().subscribe((result) => {
      if (result === 'submit') {
        this.calendarData = this.calendarService.getDialogData();
        this.calendarEvents?.forEach((element, index) => {
          if (this.calendarData.id === element.id) {
            this.editEvent(index, this.calendarData);
          }
        }, this);
        this.showNotification(
          'black',
          'Edit Record Successfully...!!!',
          'bottom',
          'center'
        );
        this.addCusForm.reset();
      } else if (result === 'delete') {
        this.calendarData = this.calendarService.getDialogData();
        this.calendarEvents?.forEach((element) => {
          if (this.calendarData.id === element.id) {
            row.event.remove();
          }
        }, this);

        this.showNotification(
          'snackbar-danger',
          'Delete Record Successfully...!!!',
          'bottom',
          'center'
        );
      }
    });

  }

  editEvent(eventIndex: number, calendarData: Calendar) {
    // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
    const calendarEvents = this.calendarEvents!.slice();
    const singleEvent = Object.assign({}, calendarEvents[eventIndex]);
    singleEvent.id = calendarData.id;
    singleEvent.title = calendarData.title;
    singleEvent.start = calendarData.start;
    singleEvent.end = calendarData.end;
    singleEvent.className = this.getClassNameValue(calendarData.groupId);
    singleEvent.groupId = calendarData.groupId;
    singleEvent['details'] = calendarData.details;
    calendarEvents[eventIndex] = singleEvent;
    this.calendarEvents = calendarEvents; // reassign the array

    this.calendarOptions.events = calendarEvents;
  }

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  handleEvents(events: EventApi[]) {
    // this.currentEvents = events;
  }

  createCalendarForm(calendar: Calendar): UntypedFormGroup {
    return this.fb.group({
      id: [calendar.id],
      title: [
        calendar.title,
        [Validators.required, Validators.pattern('[a-zA-Z]+([a-zA-Z ]+)*')],
      ],
      groupId: [calendar.groupId],
      start: [calendar.start, [Validators.required]],
      end: [calendar.end, [Validators.required]],
      details: [
        calendar.details,
        [Validators.required, Validators.pattern('[a-zA-Z]+([a-zA-Z ]+)*')],
      ],
    });
  }

  showNotification(
    colorName: string,
    text: string,
    placementFrom: MatSnackBarVerticalPosition,
    placementAlign: MatSnackBarHorizontalPosition
  ) {
    this.snackBar.open(text, '', {
      duration: 2000,
      verticalPosition: placementFrom,
      horizontalPosition: placementAlign,
      panelClass: colorName,
    });
  }

  getClassNameValue(category: string) {
    let className;

    if (category === 'work') className = 'fc-event-success';
    else if (category === 'personal') className = 'fc-event-warning';
    else if (category === 'important') className = 'fc-event-primary';
    else if (category === 'travel') className = 'fc-event-danger';
    else if (category === 'friends') className = 'fc-event-info';

    return className;
  }
}
