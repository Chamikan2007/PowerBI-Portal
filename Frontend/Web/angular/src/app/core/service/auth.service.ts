import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { User } from '../models/user';
import { Role } from '@core/models/role';
import { ApiService } from './api-service.service';
import { ResponseDto } from '@core/models/dto/response-dto';
import { StorageProvider } from './storage-provider.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private users = [
    {
      id: 1,
      img: 'assets/images/user/admin.jpg',
      username: 'hemantha.k',
      password: 'Alt@202405',
      firstName: 'Hemantha',
      lastName: 'K',
      role: Role.Approver,
      token: 'admin-token',
    },
    // {
    //   id: 1,
    //   img: 'assets/images/user/admin.jpg',
    //   username: 'admin@school.org',
    //   password: 'admin@123',
    //   firstName: 'Sarah',
    //   lastName: 'Smith',
    //   role: Role.Admin,
    //   token: 'admin-token',
    // },
    // {
    //   id: 2,
    //   img: 'assets/images/user/teacher.jpg',
    //   username: 'teacher@school.org',
    //   password: 'teacher@123',
    //   firstName: 'Ashton',
    //   lastName: 'Cox',
    //   role: Role.Teacher,
    //   token: 'teacher-token',
    // },
    // {
    //   id: 3,
    //   img: 'assets/images/user/student.jpg',
    //   username: 'student@school.org',
    //   password: 'student@123',
    //   firstName: 'Ashton',
    //   lastName: 'Cox',
    //   role: Role.Student,
    //   token: 'student-token',
    // },
  ];

  constructor(
    private http: HttpClient,
    private storageProvider: StorageProvider,
    private apiService: ApiService
  ) {
    // this.currentUserSubject = new BehaviorSubject<User>(
    //   JSON.parse(localStorage.getItem('authData') || '{}')
    // );
    // this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue() {
    let data = this.storageProvider.getStorage('authData');
    return data ? data as User : data;
  }


  isAuthenticated(): Observable<ResponseDto> {
    return this.apiService.get('Account', 'isAuthenticated', null);
  }

  signIn(username: string, password: string): Observable<ResponseDto> {
    return this.apiService.post('Account', 'signIn', { userName: username, password: password });
  }

  // template
  // login(username: string, password: string) {

  //   const user = this.users.find((u) => u.username === username && u.password === password);

  //   if (!user) {
  //     return this.error('Username or password is incorrect');
  //   } else {
  //     localStorage.setItem('currentUser', JSON.stringify(user));
  //     this.currentUserSubject.next(user);
  //     return this.ok({
  //       id: user.id,
  //       img: user.img,
  //       username: user.username,
  //       firstName: user.firstName,
  //       lastName: user.lastName,
  //       token: user.token,
  //     });
  //   }
  // }
  ok(body?: {
    id: number;
    img: string;
    username: string;
    firstName: string;
    lastName: string;
    token: string;
  }) {
    return of(new HttpResponse({ status: 200, body }));
  }
  error(message: string) {
    return throwError(message);
  }

  signOut(): Observable<ResponseDto> {
    return this.apiService.post('Account', 'signout', null);
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    // this.currentUserSubject.next(this.currentUserValue);
    return of({ success: false });
  }
}
