import { Injectable } from '@angular/core';
import { ApiService } from './api-service.service';
import { ResponseDto, ResponseModel } from '@core/models/dto/response-dto';
import { SubscriptionDto, SubscriptionListModel, SubscriptionRequestApproverLevelModel } from '@core/models/dto/subscription-dto';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root',
})
export class SubscriptionService {

  constructor(
    private apiService: ApiService
  ) { }

  getMysubscriptionRequests(getAll: boolean = true): Observable<ResponseModel<SubscriptionListModel[]>> {
    return this.apiService.get('subscriptionRequests', 'my', { all: getAll });
  }

  getSubscriptionsListForApproval(getAll: boolean = true): Observable<ResponseModel<SubscriptionListModel[]>> {
    return this.apiService.get('subscriptionRequests', 'forApprovalOfficer', { all: getAll });
  }

  getSubscriptionApprovalsById(subscriptionId: string): Observable<ResponseModel<SubscriptionRequestApproverLevelModel[]>> {
    return this.apiService.get('subscriptionRequests', `${subscriptionId}/approvalDetails`, null);
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