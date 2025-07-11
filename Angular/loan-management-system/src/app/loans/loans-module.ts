import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Loans } from './loans';
import { LoansRoutingModule } from './loans.routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoanListComponent } from './loan-list/loan-list';
import { LoanService } from '../services/loan';

const routes: Routes = [{ path: '', component: Loans }];

// @NgModule({
//   declarations: [Loans],
//   imports: [RouterModule.forChild(routes)],
// })
// export class LoansModule {}


@NgModule({
  declarations: [Loans],
  imports: [
    LoansRoutingModule,
    FormsModule,
    DatePipe,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
  ],
  providers:[LoanService]
})
export class LoansModule {}
