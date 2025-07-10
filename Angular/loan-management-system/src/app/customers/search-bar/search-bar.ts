import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.html',
  styleUrls: ['./search-bar.css'],
  standalone: false,
})
export class SearchBarComponent {
  public searchControl = new FormControl('');
  @Output() search = new EventEmitter<string>();

  constructor() {
    this.searchControl.valueChanges
      .pipe(debounceTime(300), distinctUntilChanged())
      .subscribe((value) => {
        this.search.emit(value ? value.trim() : '');
      });
  }
}
