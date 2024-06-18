export class User {
  isAuthenticated: boolean = false;
  requestContext: RequestContext = new RequestContext();
}

export class RequestContext {
  userId: string = '';
  displayName: string = '';
  email: string = '';
  roles: string[] = [];
}