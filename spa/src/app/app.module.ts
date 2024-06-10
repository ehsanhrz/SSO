import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { OAuthModule } from 'angular-oauth2-oidc';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideOAuthClient } from 'angular-oauth2-oidc';
import { CallbackComponent } from './callback/callback/callback.component';

@NgModule({
  declarations: [AppComponent, CallbackComponent],
  imports: [BrowserModule, AppRoutingModule, OAuthModule.forRoot()],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent],
})
export class AppModule {}
