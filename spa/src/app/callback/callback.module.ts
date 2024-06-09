import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CallbackComponent } from './callback/callback.component';
import { RouterModule, Routes } from '@angular/router';

const xModuleRoutes: Routes = [
  { path: 'callback', component: CallbackComponent },
];

@NgModule({
  declarations: [
    CallbackComponent
  ],
  imports: [
    RouterModule.forChild(xModuleRoutes),
    CommonModule
  ]
})
export class CallbackModule { }
