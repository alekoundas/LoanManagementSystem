// import { Routes } from '@angular/router';
// import { HomeComponent } from './home/home';
// import { AuthGuard } from './guards/auth-guard';
// import { PageNotFoundComponent } from './page-not-found/page-not-found';

// export const routes: Routes = [
//   { path: 'home', component: HomeComponent },
//   {
//     path: 'customers',
//     loadChildren: () =>
//       import('./customers/customers-module').then((m) => m.CustomersModule),
//   },
//   {
//     path: 'loans',
//     loadChildren: () =>
//       import('./loans/loans-module').then((m) => m.LoansModule),
//   },
//   { path: 'admin', component: HomeComponent, canActivate: [AuthGuard] }, // Using HomeComponent as placeholder
//   { path: '', redirectTo: '/home', pathMatch: 'full' },
//   { path: '**', component: PageNotFoundComponent },
// ];
