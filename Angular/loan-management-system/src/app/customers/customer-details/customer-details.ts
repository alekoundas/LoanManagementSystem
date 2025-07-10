import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomerService } from '../../services/customer';
import { Customer } from '../../models/customer';

@Component({
  selector: 'app-customer-details',
  templateUrl: './customer-details.html',
  styleUrls: ['./customer-details.css'],
  standalone: false,
})
export class CustomerDetailsComponent implements OnInit {
  customer!: Customer;
  loading: boolean = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.loadCustomer();
  }

  loadCustomer(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loading = true;
      this.customerService.getCustomerById(+id).subscribe({
        next: (data) => {
          this.customer = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load customer details.';
          this.loading = false;
        },
      });
    }
  }

  goToLoans(): void {
    if (this.customer) {
      this.router.navigate(['/loans/customer', this.customer.customerId]);
    }
  }
}
