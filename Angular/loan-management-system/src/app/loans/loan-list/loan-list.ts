import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoanService } from '../../services/loan';
import { Loan } from '../../models/loan';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loan-list',
  templateUrl: './loan-list.html',
  styleUrls: ['./loan-list.css'],
  standalone: false,
  providers:[CommonModule]
})
export class LoanListComponent implements OnInit {
  loans: Loan[] = [];
  customerId: number | null = null;
  loading: boolean = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private loanService: LoanService
  ) {}

  ngOnInit(): void {
    this.loadLoans();
  }

  loadLoans(): void {
    this.loading = true;
    this.customerId = this.route.snapshot.paramMap.get('id')
      ? +this.route.snapshot.paramMap.get('id')!
      : null;
    if (this.customerId) {
      this.loanService.getLoansByCustomerId(this.customerId).subscribe({
        next: (data) => {
          this.loans = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load loans.';
          this.loading = false;
        },
      });
    } else {
      this.loanService.getLoans().subscribe({
        next: (data) => {
          this.loans = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load all loans.';
          this.loading = false;
        },
      });
    }
  }
}
