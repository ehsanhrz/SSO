import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideOAuthClient } from 'angular-oauth2-oidc';

@NgModule({
  declarations: [AppComponent],
  imports: [BrowserModule, AppRoutingModule, OAuthModule.forRoot()],
  providers: [provideHttpClient(), provideOAuthClient()],
  bootstrap: [AppComponent],
})
export class AppModule {}
