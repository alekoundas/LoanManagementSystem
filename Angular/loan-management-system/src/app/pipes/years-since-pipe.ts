import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'yearsSince',
  standalone: false,
})
export class YearsSincePipe implements PipeTransform {
  transform(value: string): number {
    const regDate = new Date(value);
    const today = new Date();
    let diff = today.getFullYear() - regDate.getFullYear();
    if (
      today.getMonth() < regDate.getMonth() ||
      (today.getMonth() === regDate.getMonth() &&
        today.getDate() < regDate.getDate())
    ) {
      diff--;
    }
    return diff;
  }
}
