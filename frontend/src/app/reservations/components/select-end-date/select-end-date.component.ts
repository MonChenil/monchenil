import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'select-end-date',
  templateUrl: './select-end-date.component.html',
})
export class SelectEndDateComponent {
  @Input() declare control: FormControl;
}
