import { Injectable } from '@angular/core';
import { ApiService } from './api-service.service';
import { ResponseDto } from '@core/models/dto/response-dto';
import { SubscriptionDto } from '@core/models/dto/subscription-dto';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class SubscriptionService {

  constructor(
    private apiService: ApiService
  ) { }

  getMySubscriptionsList(getAll: boolean = false): Observable<ResponseDto> {
    return this.apiService.get('subscriptionRequests', 'mySubscriptions', { all: getAll });
  }

  getSubscriptionsListForApproval(getAll: boolean = false): Observable<ResponseDto> {
    return this.apiService.get('subscriptionRequests', 'forApprovalOfficer', { all: getAll });
  }

  getReportsList(): Observable<ResponseDto> {
    return this.apiService.get('subscriptionRequests', 'reports', null);
  }

  createSubscription(model: SubscriptionDto): Observable<ResponseDto> {
    return this.apiService.post('subscriptionRequests', '', model);
  }

  getSubscriptionById(subscriptionId: string): Observable<ResponseDto> {
    return this.apiService.get('subscriptionRequests', `${subscriptionId}`, null);
  }

  deleteSubscription(subscriptionId: string): Observable<ResponseDto> {
    return this.apiService.delete('subscriptionRequests', `${subscriptionId}`, null);
  }

  /**
   * Approved, Rejected
   */
  sendSubscriptionAction(subscriptionId: string, action: ActionType, comment: string): Observable<ResponseDto> {
    return this.apiService.post('subscriptionRequests', `${subscriptionId}/action/${action}`, {
      comment: comment.length === 0 ? null : comment
    });
  }
}

type ActionType = 'Approved' | 'Rejected'; 