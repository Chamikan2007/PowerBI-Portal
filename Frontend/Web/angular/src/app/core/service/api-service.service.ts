// import { AppSettingsService } from "./app-settings.service";
// import { AppToastNotificationService } from "../../shared-components/common/app-toast-notification/app-toast-notification.service";
// import { BusyIndicatorService } from "../../shared-components/common/busy-indicator/busy-indicator.service";
// import { BusyOperation } from "../../shared-components/common/busy-indicator/busy-indicator.model";
// import { CommonHelperService } from "./helper.service";
// import { DeveloperHelper } from "../../helpers/developer-helper";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
// import { HttpRequestMethod } from "../../models/constants.model";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { share } from 'rxjs/operators';
import { Subject } from "rxjs/internal/Subject";
import { environment } from "environments/environment";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  apiUrl: string = '';
  private userLogoutSoucre = new Subject();
  userLogout$ = this.userLogoutSoucre.asObservable();

  constructor(
    private httpClient: HttpClient,
    // private appSettingsService: AppSettingsService,
    // private toastNotificationService: AppToastNotificationService,
    // private busyIndicatorService: BusyIndicatorService,
    // private tenantContextService: TenantContextService
  ) {
    // let appSettings = appSettingsService.getSettings();
    // this.apiUrl = appSettings.webApiUrl;
    this.apiUrl = environment.apiUrl;
  }

  public get(controller: string, action: string, parameters: any, displayLoading: boolean = true, contentType: string = ContentTypes.jsonencode, tenantContextKey: string = null as any, tenantContextId: string = null as any): Observable<any> {
    return this.sendApiRequest(controller, action, parameters, 'GET', true, displayLoading, contentType, null as any, tenantContextKey, tenantContextId);
  }

  public post(controller: string, action: string, parameters: any, useQueryParams: boolean = false, displayLoading: boolean = true, contentType: string = ContentTypes.jsonencode, tenantContextKey: string = null as any, tenantContextId: string = null as any): Observable<any> {
    return this.sendApiRequest(controller, action, parameters, 'POST', useQueryParams, displayLoading, contentType, null as any, tenantContextKey, tenantContextId);
  }

  public postForm(controller: string, action: string, form: FormData, displayLoading: boolean = true): Observable<any> {
    return this.sendApiRequest(controller, action, {}, 'POST', false, displayLoading, ContentTypes.multipartformdata, form);
  }

  public delete(controller: string, action: string, parameters: any, useQueryParams: boolean = true, displayLoading: boolean = true, contentType: string = ContentTypes.jsonencode): Observable<any> {
    return this.sendApiRequest(controller, action, parameters, 'DELETE', useQueryParams, displayLoading, contentType);
  }

  public postUrl(url: string, parameters: any, useQueryParams: boolean = false, displayLoading: boolean = true, contentType: string = ContentTypes.jsonencode): Observable<any> {
    return this.sendHttpRequest(this.apiUrl + '/' + url, parameters, 'POST', useQueryParams, displayLoading, contentType);
  }

  public postFormUrl(url: string, form: FormData, displayLoading: boolean = true): Observable<any> {
    return this.sendHttpRequest(this.apiUrl + '/' + url, {}, 'POST', false, displayLoading, ContentTypes.multipartformdata, form);
  }

  public put(controller: string, action: string, parameters: any, useQueryParams: boolean = false, displayLoading: boolean = true, contentType: string = ContentTypes.jsonencode, tenantContextKey: string = null as any, tenantContextId: string = null as any): Observable<any> {
    return this.sendApiRequest(controller, action, parameters, 'PUT', useQueryParams, displayLoading, contentType, null as any, tenantContextKey, tenantContextId);
  }

  // Common private methods

  private sendApiRequest(controller: string, action: string, parameters: any, requestMethod: string, useQueryParams: boolean, displayLoading: boolean = true, contentType: string, form: FormData = null as any, tenantContextKey: string = null as any, tenantContextId: string = null as any): Observable<any> {
    let url: string;
    if (action == null || action.length === 0) {
      url = this.apiUrl + '/' + controller;
    } else {
      url = this.apiUrl + '/' + controller + '/' + action;
    }

    return this.sendHttpRequest(url, parameters, requestMethod, useQueryParams, displayLoading, contentType, form, tenantContextKey, tenantContextId);
  }

  private serializeObj(obj: any) {
    let result = [];
    for (let property in obj) {
      result.push(encodeURIComponent(property) + '=' + encodeURIComponent(obj[property]));
    }
    return result.join('&');
  }

  private sendHttpRequest(url: string, parameters: any, requestMethod: string, useQueryParams: boolean, displayLoading: boolean = true, contentType: string = ContentTypes.jsonencode, form: FormData = null as any, tenantContextKey: string = null as any, tenantContextId: string = null as any): Observable<any> {
    let options = {
      headers: new HttpHeaders({}),
      params: null,
      body: null,
      withCredentials: true
    };

    // Adding no-cache headers to fix IE response caching issue.
    options.headers = options.headers.append('Cache-Control', 'no-cache');
    options.headers = options.headers.append('Pragma', 'no-cache');
    options.headers = options.headers.append('Expires', 'Sat, 01 Jan 2000 00:00:00 GMT');

    if (useQueryParams) {
      // Construct parameters.
      let params = new HttpParams();
      for (let property in parameters) {
        params = params.append(property, parameters[property]);
      }
      options.params = params as any;
    } else {
      if (contentType === ContentTypes.jsonencode) {
        options.headers = options.headers.append('Content-Type', ContentTypes.jsonencode);
        options.body = JSON.stringify(parameters) as any;
      } else if (contentType === ContentTypes.multipartformdata) {
        options.body = form as any;
      } else {
        options.headers = options.headers.append('Content-Type', ContentTypes.urlencode);
        options.body = this.serializeObj(parameters) as any;
      }
    }

    // We create a sharable out of the request, because we need to have multiple event subscriptions.
    let sharable = this.httpClient.request(requestMethod, url, options as any).pipe(share());

    // let operation: BusyOperation = null as any;

    // Loading indicator.
    if (displayLoading) {
      // operation = this.busyIndicatorService.startOperation();
    }

    // Subscribe and handle common events.
    sharable.subscribe(
      {
        next: (data: any) => {
          // On data recieved.
        },
        error: (err) => {
          // On error.
          if (err.status !== 400) {
            // Let the page handle 400 code as it wishes.
            // DeveloperHelper.logError(err);
            if (err.status === 401) {
              this.userLogoutSoucre.next(null);
            } else if (err.status === 409) {
              // this.toastNotificationService.showToast('error', 'Record modified', 'This record has been modified elsewhere. Please reload the page.');
            } else if (err.status >= 500) {
              // this.toastNotificationService.showToast('error', 'Error!', 'An error occured while communicating with the server.');
            } else if (err.status === 403) {
              // this.toastNotificationService.showToast('error', 'Forbidden', 'You do not have permission to perform this action.');
            }
          }

          // if (operation != null) {
          //   operation.isCompleted = true;
          //   this.busyIndicatorService.endOperation(operation);
          // }
        },
        complete: () => {
          // On completed.
          // if (operation != null) {
          //   operation.isCompleted = true;
          //   this.busyIndicatorService.endOperation(operation);
          // }
        }
      }
    );

    return sharable;
  }
}

class ContentTypes {
  static urlencode = 'application/x-www-form-urlencoded;charset=UTF-8';
  static jsonencode = 'application/json';
  static multipartformdata = 'multipart/form-data';
}
