import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoanService } from '../../services/loan';
import { Loan } from '../../models/loan';

@Component({
  selector: 'app-loan-details',
  templateUrl: './loan-details.html',
  styleUrls: ['./loan-details.css'],
  standalone: false,
})
export class LoanDetailsComponent implements OnInit {
  public loan!: Loan;
  loading: boolean = false;
  error: string | null = null;
  public defaultDate: Date = new Date();
  constructor(
    private route: ActivatedRoute,
    private loanService: LoanService
  ) {}

  ngOnInit(): void {
    this.loadLoan();
  }

  loadLoan(): void {
    const loanId = this.route.snapshot.paramMap.get('loanId');
    if (loanId) {
      this.loading = true;
      this.loanService.getLoanById(+loanId).subscribe({
        next: (data) => {
          this.loan = data;
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load loan details.';
          this.loading = false;
        },
      });
    }
  }
}
