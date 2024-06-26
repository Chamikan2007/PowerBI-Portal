import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FeatherModule } from 'angular-feather';

@Component({
  selector: 'app-feather-icons',
  templateUrl: './feather-icons.component.html',
  styleUrls: ['./feather-icons.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FeatherModule
  ],
})
export class FeatherIconsComponent {
  @Input() public icon?: string;
  @Input() public class?: string;

  @Output() onClicked: EventEmitter<any> = new EventEmitter<any>();

  constructor() { }

  onClick() {
    this.onClicked.emit();
  }

}
