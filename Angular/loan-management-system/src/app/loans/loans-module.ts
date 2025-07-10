import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Loans } from './loans';

const routes: Routes = [{ path: '', component: Loans }];

@NgModule({
  declarations: [Loans],
  imports: [RouterModule.forChild(routes)],
})
export class LoansModule {}
