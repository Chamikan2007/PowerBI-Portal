import { Route } from '@angular/router';
import { Page404Component } from 'app/authentication/page404/page404.component';

export const ADMIN_ROUTE: Route[] = [
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.routes').then((m) => m.DASHBOARD_ROUTE),
  },
  {
    path: 'teachers',
    loadChildren: () =>
      import('./teachers/admin-teachers.routes').then(
        (m) => m.ADMIN_TEACHER_ROUTE
      ),
  },
  {
    path: 'students',
    loadChildren: () =>
      import('./students/admin-students.routes').then(
        (m) => m.ADMIN_STUDENT_ROUTE
      ),
  },
  {
    path: 'courses',
    loadChildren: () =>
      import('./courses/courses.routes').then((m) => m.COURSE_ROUTE),
  },
  {
    path: 'library',
    loadChildren: () =>
      import('./library/library.routes').then((m) => m.LIBRARY_ROUTE),
  },
  {
    path: 'departments',
    loadChildren: () =>
      import('./departments/departments.routes').then(
        (m) => m.DEPARTMENT_ROUTE
      ),
  },
  {
    path: 'staff',
    loadChildren: () =>
      import('./staff/staff.routes').then((m) => m.STAFF_ROUTE),
  },
  {
    path: 'holidays',
    loadChildren: () =>
      import('./holidays/holidays.routes').then((m) => m.HOLIDAY_ROUTE),
  },
  {
    path: 'fees',
    loadChildren: () => import('./fees/fees.routes').then((m) => m.FEES_ROUTE),
  },
  {
    path: 'attendance',
    loadChildren: () =>
      import('./attendance/attendance.routes').then((m) => m.ATTENDANCE_ROUTE),
  },
  { path: '**', component: Page404Component },
];
