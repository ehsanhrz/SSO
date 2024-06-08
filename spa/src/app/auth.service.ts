import { Injectable } from '@angular/core';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';

const authConfig: AuthConfig = {
  issuer: 'http://localhost:61006', // Your OpenIddict server
  redirectUri: window.location.origin + '/callback',
  clientId: 'angular_spa',
  responseType: 'code',
  scope: 'openid profile email', // The scopes you want to request
  showDebugInformation: true,
  loginUrl: 'http://localhost:61006/Account/Login',
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
    this.oauthService.initLoginFlow();
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
