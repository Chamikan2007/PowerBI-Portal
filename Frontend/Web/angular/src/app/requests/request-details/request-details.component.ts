import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-request-details',
  standalone: true,
  imports: [],
  templateUrl: './request-details.component.html',
  styleUrl: './request-details.component.scss'
})
export class RequestDetailsComponent {

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router) { }

  goBack() {
    this.router.navigate(['/', 'requests']);
  }
}
