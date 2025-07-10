export interface LoanProduct {
  loanProductId: number;
  productName: string;
  description: string;
  interestRate: number;
  maxLoanAmount: number;
  minCreditScore: number;
  minTermMonths: number;
  maxTermMonths: number;
  isActive: boolean;
}
