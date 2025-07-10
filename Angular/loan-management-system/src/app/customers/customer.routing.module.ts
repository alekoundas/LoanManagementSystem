import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerListComponent } from './customer-list/customer-list';
import { CustomerDetailsComponent } from './customer-details/customer-details';

const routes: Routes = [
  { path: '', component: CustomerListComponent },
  { path: ':id', component: CustomerDetailsComponent },
  { path: ':id/loans', redirectTo: '/loans/customer/:id', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomersRoutingModule {}
