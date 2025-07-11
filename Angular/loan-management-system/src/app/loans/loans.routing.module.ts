import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoanListComponent } from './loan-list/loan-list';
import { LoanDetailsComponent } from './loan-details/loan-details';
import { CurrencyPipe, DatePipe } from '@angular/common';

const routes: Routes = [
  { path: '', component: LoanListComponent },
  { path: 'customer/:id', component: LoanListComponent },
  { path: ':loanId/details', component: LoanDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes), DatePipe, CurrencyPipe],
  exports: [RouterModule],
  declarations: [LoanDetailsComponent, LoanListComponent],
})
export class LoansRoutingModule {}


// const routes: Routes = [
//   { path: '', component: LoanListComponent },
//   { path: 'customer/:id', component: LoanListComponent },
//   { path: ':loanId/details', component: LoanDetailsComponent },
// ];

// @NgModule({
//   imports: [RouterModule.forChild(routes)],
//   exports: [RouterModule],
// })
// export class LoansRoutingModule {}
