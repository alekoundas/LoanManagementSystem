import { Component } from '@angular/core';
import { AppModule } from './app.module';
import { bootstrapApplication } from '@angular/platform-browser';
import { HomeComponent } from './home/home';
import { PageNotFoundComponent } from './page-not-found/page-not-found';
import { CustomersModule } from './customers';
import { provideHttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CustomersRoutingModule } from './customers/customer.routing.module';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: true,
  imports: [CustomersRoutingModule],
})
export class AppComponent {
  protected title = 'loan-management-system';
}

bootstrapApplication(AppComponent);
