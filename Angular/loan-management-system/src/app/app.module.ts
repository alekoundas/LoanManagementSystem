import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app.routing.module';
import { AppComponent } from './app';
import { HomeComponent } from './home/home';
import { PageNotFoundComponent } from './page-not-found/page-not-found';
import { FormsModule } from '@angular/forms';
import { LoggingInterceptor } from './interceptors/logging-interceptor';
import { ErrorInterceptor } from './interceptors/error-interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { LoansModule } from './loans/loans-module';
import { CustomersModule } from './customers/customers-module';
import { SearchBarComponent } from './customers/search-bar/search-bar';
import { CustomerService } from './services/customer';
import { RouterOutlet } from '@angular/router';

@NgModule({
  imports: [
    AppComponent,
    CommonModule,
    AppRoutingModule,
    FormsModule,
    DatePipe,
    CurrencyPipe,
    LoansModule,
    CustomersModule,
    HomeComponent,
    PageNotFoundComponent,
    BrowserModule,
    HttpClientModule,
    RouterOutlet,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
})
export class AppModule {}
