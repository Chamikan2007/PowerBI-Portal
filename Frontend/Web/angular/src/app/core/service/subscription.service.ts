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
    return this.apiService.get('Subscriptions', 'mySubscriptions', { all: getAll });
  }

  getSubscriptionsListForApproval(getAll: boolean = false): Observable<ResponseDto> {
    return this.apiService.get('Subscriptions', 'forApprovalOfficer', { all: getAll });
  }

  getReportsList(): Observable<ResponseDto> {
    return this.apiService.get('Subscriptions', 'reports', null);
  }

  createSubscription(reportPath: string, email: string): Observable<ResponseDto> {
    return this.apiService.post('Subscriptions', '', {
      reportPath: reportPath,
      email: email
    });
  }

  getSubscriptionById(subscriptionId: string): Observable<ResponseDto> {
    return this.apiService.get('Subscriptions', subscriptionId, null);
  }

  deleteSubscription(subscriptionId: string): Observable<ResponseDto> {
    return this.apiService.delete('Subscriptions', subscriptionId, null);
  }

  /**
   * Approved, Rejected
   */
  sendSubscriptionAction(subscriptionId: string, action: ActionType, comment: string): Observable<ResponseDto> {
    return this.apiService.post('Subscriptions', `${subscriptionId}/action/${action}`, {
      comment: comment.length === 0 ? null : comment
    });
  }
}

type ActionType = 'Approved' | 'Rejected'; 