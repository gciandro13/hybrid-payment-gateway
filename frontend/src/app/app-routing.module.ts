import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // Add your routes here
  // Example:
  // { path: 'payments', component: PaymentsComponent },
  // { path: 'bitcoin', component: BitcoinComponent },
  // { path: '', redirectTo: '/payments', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
