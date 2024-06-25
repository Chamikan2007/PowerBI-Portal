import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService, User } from '@core';
import { UnsubscribeOnDestroyAdapter } from '@shared';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { StorageProvider } from '@core/service/storage-provider.service';
import { ResponseDto } from '@core/models/dto/response-dto';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
  standalone: true,
  imports: [
    RouterLink,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
  ],
})
export class SigninComponent extends UnsubscribeOnDestroyAdapter implements OnInit {
  authForm!: UntypedFormGroup;
  submitted = false;
  loading = false;
  error = '';
  hide = true;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private storageProvider: StorageProvider
  ) {
    super();
  }

  ngOnInit() {
    // this.authForm = this.formBuilder.group({
    //   username: ['hemantha.k', Validators.required],
    //   password: ['Alt@202405', Validators.required],
    // });

    this.authForm = this.formBuilder.group({
      username: [''],
      password: [''],
    });
  }

  get f() {
    return this.authForm.controls;
  }

  adminSet() {
    this.authForm.get('username')?.setValue('admin@school.org');
    this.authForm.get('password')?.setValue('admin@123');
  }

  teacherSet() {
    this.authForm.get('username')?.setValue('teacher@school.org');
    this.authForm.get('password')?.setValue('teacher@123');
  }

  studentSet() {
    this.authForm.get('username')?.setValue('student@school.org');
    this.authForm.get('password')?.setValue('student@123');
  }

  onSubmit() {
    this.submitted = true;
    this.loading = true;
    this.error = '';
    if (this.authForm.invalid) {
      this.error = 'Username and Password not valid!';
      return;
    } else {

      this.authService.signIn(this.f['username'].value, this.f['password'].value).subscribe({
        next: (resonse1: ResponseDto) => {
          if (resonse1.isSuccess) {
            this.authService.isAuthenticated().subscribe({
              next: (response2: ResponseDto) => {
                if (response2.isSuccess) {
                  let data = response2.data as User;

                  if (data.isAuthenticated) {
                    this.storageProvider.setStorage(true, 'authData', data);
                    this.router.navigate(['/subscriptions']);
                  }
                  else {
                    this.storageProvider.clearStorage();
                    this.router.navigate(['/authentication/signin']);
                  }

                  this.loading = false;
                }
                else {
                  this.submitted = false;
                  this.loading = false;
                }
              },
              error: (error) => {
                this.error = error;
                this.submitted = false;
                this.loading = false;
              },
            });

          }
          else {
            this.submitted = false;
            this.loading = false;
          }
        },
        error: (error) => {
          this.error = error;
          this.submitted = false;
          this.loading = false;
        },
      });


      // this.subs.sink = this.authService
      //   .login(this.f['username'].value, this.f['password'].value)
      //   .subscribe({
      //     next: (res) => {
      //       if (res) {
      //         setTimeout(() => {
      //           const role = this.authService.currentUserValue.role;
      //           if (role === Role.All || role === Role.Admin) {
      //             this.router.navigate(['/admin/dashboard/main']);
      //           } else if (role === Role.Teacher) {
      //             this.router.navigate(['/teacher/dashboard']);
      //           } else if (role === Role.Student) {
      //             this.router.navigate(['/student/dashboard']);
      //           } else {
      //             this.router.navigate(['/authentication/signin']);
      //           }
      //           this.loading = false;
      //         }, 1000);
      //       } else {
      //         this.error = 'Invalid Login';
      //       }
      //     },
      //     error: (error) => {
      //       this.error = error;
      //       this.submitted = false;
      //       this.loading = false;
      //     },
      //   });
    }
  }
}
