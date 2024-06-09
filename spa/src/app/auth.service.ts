import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';

const authConfig: AuthConfig = {
  issuer: 'http://localhost:55984/', // Your OpenIddict server
  redirectUri: 'http://127.0.0.1:4200' + '/callback',
  clientId: 'angular_spa',
  responseType: 'code',
  scope: 'openid profile email', // The scopes you want to request
  showDebugInformation: true,
  loginUrl: 'http://localhost:55984/Account/Login',
  requireHttps: false,

};

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private oauthService: OAuthService) {
    this.oauthService.configure(authConfig);
    this.oauthService.loadDiscoveryDocumentAndTryLogin();
  }

  public login(): void {
    this.oauthService.initCodeFlow();
  }

  public logout(): void {
    this.oauthService.logOut();
  }

  public get identityClaims(): any {
    return this.oauthService.getIdentityClaims();
  }

  public get isLoggedIn(): boolean {
    return this.oauthService.hasValidAccessToken();
  }
}
