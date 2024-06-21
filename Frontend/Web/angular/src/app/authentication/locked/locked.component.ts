import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService, Role } from '@core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
@Component({
  selector: 'app-locked',
  templateUrl: './locked.component.html',
  styleUrls: ['./locked.component.scss'],
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    RouterLink,
  ],
})
export class LockedComponent implements OnInit {
  authForm!: UntypedFormGroup;
  submitted = false;
  userImg!: string;
  userFullName!: string;
  hide = true;
  constructor(
    private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) { }
  ngOnInit() {
    this.authForm = this.formBuilder.group({
      password: ['', Validators.required],
    });
    // this.userImg = this.authService.currentUserValue.img;
    this.userFullName = this.authService.currentUserValue.requestContext.displayName;
  }
  get f() {
    return this.authForm.controls;
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.authForm.invalid) {
      return;
    } else {
      const role = this.authService.currentUserValue.requestContext.roles;
      // if (role === Role.All || role === Role.Admin) {
      //   this.router.navigate(['/admin/dashboard/main']);
      // } else if (role === Role.Teacher) {
      //   this.router.navigate(['/teacher/dashboard']);
      // } else if (role === Role.Student) {
      //   this.router.navigate(['/student/dashboard']);
      // } else {
      //   this.router.navigate(['/authentication/signin']);
      // }
    }
  }
}