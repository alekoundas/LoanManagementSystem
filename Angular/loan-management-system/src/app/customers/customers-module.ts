import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { CustomersRoutingModule } from './customer.routing.module';
import { CustomerListComponent } from './customer-list/customer-list';
import { CustomerDetailsComponent } from './customer-details/customer-details';
import { SearchBarComponent } from './search-bar/search-bar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { YearsSincePipe } from '../pipes/years-since-pipe';
import { ColorDirective } from '../directives/color';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    CustomerListComponent,
    CustomerDetailsComponent,
    SearchBarComponent,
    YearsSincePipe,
    ColorDirective,
  ],
  imports: [
    CustomersRoutingModule,
    FormsModule,
    DatePipe,
    CommonModule,ReactiveFormsModule,
    HttpClientModule,
  ],
  exports: [
    SearchBarComponent,
    CustomerListComponent,
    CustomerDetailsComponent,
  ],
})
export class CustomersModule {}
