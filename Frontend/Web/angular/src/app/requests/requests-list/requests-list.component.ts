import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  goBack() {
    this.router.navigate(['/', 'admin', 'dashboard']);
  }

  editClicked(event: any) {
    this.router.navigate(['0'], { relativeTo: this.activatedRoute.parent });
  }
}
