import { Component } from '@angular/core';
import { FeatherIconsComponent } from '@shared/components/feather-icons/feather-icons.component';

@Component({
  selector: 'app-requests-list',
  standalone: true,
  imports: [
    FeatherIconsComponent
  ],
  templateUrl: './requests-list.component.html',
  styleUrl: './requests-list.component.scss'
})
export class RequestsListComponent {

  editClicked($event: any) {
    debugger;
  }
}
