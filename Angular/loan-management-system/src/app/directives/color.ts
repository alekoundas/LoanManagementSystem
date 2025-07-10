import { Directive, ElementRef, Input, OnInit } from '@angular/core';
import { PaletteUtil } from '../utils/palette.util';

@Directive({
  selector: '[appColor]',
  standalone: false,
})
export class ColorDirective implements OnInit {
  @Input() appColor: number = 0;

  constructor(private el: ElementRef) {}

  ngOnInit(): void {
    this.el.nativeElement.style.color = PaletteUtil.getColor(this.appColor);
  }
}
