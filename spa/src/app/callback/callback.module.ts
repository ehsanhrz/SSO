import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CallbackComponent } from './callback/callback.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: "", component: CallbackComponent },
];

@NgModule({
  declarations: [
    CallbackComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class CallbackModule { }
