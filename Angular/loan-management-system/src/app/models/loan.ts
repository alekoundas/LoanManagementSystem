import { LoanProduct } from './loan-product';

export interface Loan {
  loanId: number;
  applicationId: number;
  customerId: number;
  loanProductId: number;
  approvedAmount: number;
  disbursementDate: string;
  maturityDate: string;
  interestRate: number;
  currentBalance: number;
  originalTermMonths: number;
  loanStatus: string;
  lastPaymentDate: string | null;
  nextPaymentDueDate: string | null;
  loanProduct?: LoanProduct; // Optional for display purposes
}
