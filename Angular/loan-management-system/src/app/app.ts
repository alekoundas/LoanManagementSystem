import { Component } from '@angular/core';
import { AppModule } from './app.module';
import { bootstrapApplication } from '@angular/platform-browser';
import { HomeComponent } from './home/home';
import { PageNotFoundComponent } from './page-not-found/page-not-found';
import { CustomersModule } from './customers';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  imports: [AppModule, HomeComponent, PageNotFoundComponent, CustomersModule],
  styleUrl: './app.css',
})
export class AppComponent {
  protected title = 'loan-management-system';
}
bootstrapApplication(AppComponent);
