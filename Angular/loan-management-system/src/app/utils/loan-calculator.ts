export interface LoanType {
  principal: number;
  annualRate: number;
  termMonths: number;
}

export interface BusinessLoan extends LoanType {
  businessFee: number;
}

export interface HomeLoan extends LoanType {
  propertyTax: number;
}

export class LoanCalculator {
  static calculateMonthlyPayment<T extends LoanType>(loan: T): number {
    const monthlyRate = loan.annualRate / 12 / 100;
    const denominator = Math.pow(1 + monthlyRate, loan.termMonths) - 1;
    let monthlyPayment =
      (loan.principal *
        monthlyRate *
        Math.pow(1 + monthlyRate, loan.termMonths)) /
      denominator;

    // Apply custom rules based on loan type
    if ('businessFee' in loan) {
      monthlyPayment += (loan as BusinessLoan).businessFee / loan.termMonths;
    } else if ('propertyTax' in loan) {
      monthlyPayment += (loan as HomeLoan).propertyTax / loan.termMonths;
    }

    return Number(monthlyPayment.toFixed(2));
  }
}
