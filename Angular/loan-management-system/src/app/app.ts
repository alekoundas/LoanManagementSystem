import { Component } from '@angular/core';
import { CustomersRoutingModule } from './customers/customer.routing.module';
import { LoansRoutingModule } from './loans/loans.routing.module';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: false,
  providers: [CustomersRoutingModule,LoansRoutingModule],
})
export class AppComponent {
  protected title = 'loan-management-system';
}
