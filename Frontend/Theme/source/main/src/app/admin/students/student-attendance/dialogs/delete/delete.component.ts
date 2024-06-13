import { MAT_DIALOG_DATA, MatDialogRef, MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose } from '@angular/material/dialog';
import { Component, Inject } from '@angular/core';
import { StudentAttendanceService } from '../../attendance.service';
import { DatePipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

export interface DialogData {
  id: number;
  rollNo: string;
  sName: string;
  date: string;
}

@Component({
  selector: 'app-delete:not(g)',
  templateUrl: './delete.component.html',
  styleUrls: ['./delete.component.scss'],
  standalone: true,
  imports: [
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatButtonModule,
    MatDialogClose,
    DatePipe,
  ],
})
export class DeleteDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public studentAttendanceService: StudentAttendanceService
  ) { }
  onNoClick(): void {
    this.dialogRef.close();
  }
  confirmDelete(): void {
    this.studentAttendanceService.deleteStudentAttendance(this.data.id);
  }
}
