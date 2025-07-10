export interface LoanApplication {
  applicationId: number;
  customerId: number;
  loanProductId: number;
  requestedAmount: number;
  applicationDate: string;
  applicationStatus: string;
  assignedEmployeeId: number | null;
  decisionDate: string | null;
  approvedAmount: number | null;
  rejectionReason: string | null;
}
