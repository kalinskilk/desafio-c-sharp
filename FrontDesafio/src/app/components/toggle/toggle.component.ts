import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Optional,
  Output,
  Self,
} from '@angular/core';
import { ControlValueAccessor, FormsModule, NgControl } from '@angular/forms';
import { InputSwitchModule } from 'primeng/inputswitch';

@Component({
  selector: 'app-toggle',
  standalone: true,
  templateUrl: './toggle.component.html',
  styleUrls: ['./toggle.component.scss'],
  imports: [CommonModule, InputSwitchModule, FormsModule],
  providers: [],
})
export class ToggleComponent implements ControlValueAccessor {
  @Input() label?: string;
  @Input() disabled: boolean = false;
  @Input() required: boolean = false;
  @Input() errorMessage?: string;
  @Input() helperText?: string;

  @Output() valueChange = new EventEmitter<boolean>();

  id = 'toggle_' + Math.random().toString(36).substr(2, 9);
  value: boolean = false;

  onChange = (value: boolean) => {};
  onTouched = () => {};

  constructor(@Optional() @Self() public control: NgControl) {
    if (this.control) {
      this.control.valueAccessor = this;
    }
  }

  writeValue(value: boolean): void {
    this.value = value ?? false;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  onToggleChange(event: boolean): void {
    this.value = event;
    this.onChange(event);
    this.valueChange.emit(event);
  }
}
