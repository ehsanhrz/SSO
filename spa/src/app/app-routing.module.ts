import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CallbackComponent } from './callback/callback/callback.component';
import { HomeComponentComponent } from './home-component/home-component.component';

const routes: Routes = [
  { path: "", component: HomeComponentComponent, pathMatch: "full" },
  {
    path: 'callback',
    loadChildren: () => import("./callback/callback.module").then(c => c.CallbackModule)
  },
  // Add any other routes here if needed
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
