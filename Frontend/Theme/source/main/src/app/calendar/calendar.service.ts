import { Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY } from 'rxjs';
import { Calendar } from './calendar.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})

export class CalendarService {
  private readonly API_URL = 'assets/data/calendar.json';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  dataChange: BehaviorSubject<Calendar[]> = new BehaviorSubject<Calendar[]>([]);
  // Temporarily stores data from dialogs
  dialogData!: Calendar;
  constructor(private httpClient: HttpClient) { }
  get data(): Calendar[] {
    return this.dataChange.value;
  }
  getDialogData() {
    return this.dialogData;
  }
  getAllCalendars(): Observable<Calendar[]> {
    return this.httpClient
      .get<Calendar[]>(this.API_URL)
      .pipe(catchError(this.errorHandler));
  }

  addUpdateCalendar(calendar: Calendar): Observable<Calendar> {
    this.dialogData = calendar;
    // uncomment this code and call your API for add or update data
    // return this.httpClient.post<Calendar>(this.API_URL, calendar);

    return new Observable();
  }
  deleteCalendar(calendar: Calendar): Observable<Calendar> {
    // uncomment this code and call your API for add or update data
    // return this.httpClient.post<Calendar>(this.API_URL, calendar);
    this.dialogData = calendar;
    return new Observable();
  }
  errorHandler(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
