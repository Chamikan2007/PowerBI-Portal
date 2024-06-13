import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent, MatDialogClose } from "@angular/material/dialog";
import { Component, Inject } from "@angular/core";
import { LecturesService } from "../../lectures.service";
import { UntypedFormControl, Validators, UntypedFormGroup, UntypedFormBuilder, FormsModule, ReactiveFormsModule } from "@angular/forms";
import { Lectures } from "../../lectures.model";
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
  lectures: Lectures;
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
  lecturesForm: UntypedFormGroup;
  lectures: Lectures;
  constructor(
    public dialogRef: MatDialogRef<FormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    public lecturesService: LecturesService,
    private fb: UntypedFormBuilder
  ) {
    // Set the defaults
    this.action = data.action;
    if (this.action === "edit") {
      this.dialogTitle = data.lectures.sName;
      this.lectures = data.lectures;
    } else {
      this.dialogTitle = "New Lectures";
      const blankObject = {} as Lectures;
      this.lectures = new Lectures(blankObject);
    }
    this.lecturesForm = this.createContactForm();
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
      id: [this.lectures.id],
      sName: [this.lectures.sName, [Validators.required]],
      class: [this.lectures.class, [Validators.required]],
      date: [this.lectures.date, [Validators.required]],
      time: [this.lectures.time, [Validators.required]],
      status: [this.lectures.status, [Validators.required]],
    });
  }
  submit() {
    // emppty stuff
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  public confirmAdd(): void {
    this.lecturesService.addLectures(this.lecturesForm.getRawValue());
  }
}
