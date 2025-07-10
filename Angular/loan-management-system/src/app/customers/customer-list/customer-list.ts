import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../services/customer';
import { Customer } from '../../models/customer';
import { SearchBarComponent } from '../search-bar/search-bar';
import { HttpClient, HttpHandler } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.html',
  styleUrls: ['./customer-list.css'],
  standalone: false,
  providers: [CustomerService, HttpClient],
})
export class CustomerListComponent implements OnInit {
  customers: Customer[] = [];
  filteredCustomers: Customer[] = [];
  selectedCustomerId: number | null = null;
  loading: boolean = false;
  error: string | null = null;

  constructor(
    private customerService: CustomerService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loading = true;
    this.customerService.getCustomers().subscribe({
      next: (data) => {
        this.customers = data;
        this.filteredCustomers = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load customers. Please try again.';
        this.loading = false;
      },
    });
  }

  selectCustomer(id: number): void {
    this.selectedCustomerId = id;
    this.navigateToCustomerPage(id);
  }
  navigateToCustomerPage(id: number) {
    this.router.navigate(['/customers/' + id.toString()], {
      relativeTo: this.route,
    });
  }

  isLongTermCustomer(registrationDate: string): boolean {
    const regDate = new Date(registrationDate);
    const fiveYearsAgo = new Date();
    fiveYearsAgo.setFullYear(fiveYearsAgo.getFullYear() - 5);
    return regDate < fiveYearsAgo;
  }

  public onSearch(term: string): void {
    this.filteredCustomers = this.customers.filter((customer) =>
      `${customer.firstName} ${customer.lastName}`
        .toLowerCase()
        .includes(term.toLowerCase())
    );
  }
}
