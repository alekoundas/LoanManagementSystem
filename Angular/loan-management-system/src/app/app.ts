import { Component } from '@angular/core';
import {  RouterOutlet } from '@angular/router';
import { CustomersRoutingModule } from './customers/customer.routing.module';
import { LoansRoutingModule } from './loans/loans.routing.module';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: false,
  // imports:[],
  providers: [CustomersRoutingModule,LoansRoutingModule],
  //viewProviders:[RouterOutlet]
})
export class AppComponent {
  protected title = 'loan-management-system';
}

// bootstrapApplication(AppComponent);
