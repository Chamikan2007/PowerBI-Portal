import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent, MatDialogClose } from "@angular/material/dialog";
import { Component, Inject } from "@angular/core";
import { LeaveRequestService } from "../../leave-request.service";
import { UntypedFormControl, Validators, UntypedFormGroup, UntypedFormBuilder, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { LeaveRequest } from "../../leave-request.model";
import { MAT_DATE_LOCALE, MatOptionModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatInputModule } from "@angular/material/input";
import { MatSelectModule } from "@angular/material/select";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";

export interface DialogData {
  id: number;
  action: string;
  leaveRequest: LeaveRequest;
}

@Component({
  selector: "app-form-dialog",
  templateUrl: "./form-dialog.component.html",
  styleUrls: ["./form-dialog.component.scss"],
  providers: [{ provide: MAT_DATE_LOCALE, useValue: "en-GB" }],
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    MatDialogContent,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatOptionModule,
    MatInputModule,
    MatDatepickerModule,
    MatDialogClose,
  ],
})
export class FormDialogComponent {
  action: string;
  dialogTitle: string;
  leaveRequestForm: UntypedFormGroup;
  leaveRequest: LeaveRequest;
  constructor(
    public dialogRef: MatDialogRef<FormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public leaveRequestService: LeaveRequestService,
    private fb: UntypedFormBuilder
  ) {
    // Set the defaults
    this.action = data.action;
    if (this.action === "edit") {
      this.dialogTitle = "Edit Leave Request";
      this.leaveRequest = data.leaveRequest;
    } else {
      this.dialogTitle = "New Leave Request";
      const blankObject = {} as LeaveRequest;
      this.leaveRequest = new LeaveRequest(blankObject);
    }
    this.leaveRequestForm = this.createContactForm();
  }
  formControl = new UntypedFormControl("", [
    Validators.required,
    // Validators.email,
  ]);
  getErrorMessage() {
    return this.formControl.hasError("required")
      ? "Required field"
      : this.formControl.hasError("email")
        ? "Not a valid email"
        : "";
  }
  createContactForm(): UntypedFormGroup {
    return this.fb.group({
      id: [this.leaveRequest.id],
      class: [this.leaveRequest.class, [Validators.required]],
      section: [this.leaveRequest.section, [Validators.required]],
      applyDate: [this.leaveRequest.applyDate, [Validators.required]],
      fromDate: [this.leaveRequest.fromDate, [Validators.required]],
      toDate: [this.leaveRequest.toDate, [Validators.required]],
      status: [this.leaveRequest.status, [Validators.required]],
      reason: [this.leaveRequest.reason, [Validators.required]],
    });
  }
  submit() {
    // emppty stuff
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  public confirmAdd(): void {
    this.leaveRequestService.addLeaveRequest(
      this.leaveRequestForm.getRawValue()
    );
  }
}
