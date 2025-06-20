import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Optional,
  Output,
  Self,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { TooltipModule } from 'primeng/tooltip';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss'],
  imports: [CommonModule, InputTextModule, TooltipModule],
  providers: [],
})
export class InputComponent implements ControlValueAccessor {
  @Input() label?: string;
  @Input() placeholder?: string = '';
  @Input() icon?: string;
  @Input() iconPosition: 'left' | 'right' = 'left';
  @Input() disabled: boolean = false;
  @Input() readonly: boolean = false;
  @Input() required: boolean = false;
  @Input() type: string = 'text';
  @Input() errorMessage?: string;
  @Input() helperText?: string;
  @Input() infoTooltip?: string;
  @Input() class: string = '';
  @Input() size: 'small' | 'normal' | 'large' = 'normal';
  @Input() maxLength = 255;

  @Output() valueChange = new EventEmitter<string>();

  id = 'input_' + Math.random().toString(36).substr(2, 9);
  value: string = '';

  constructor(@Optional() @Self() public control: NgControl) {
    if (this.control) {
      this.control.valueAccessor = this;
    }
  }

  onChange = (value: any) => {};
  onTouched = () => {};

  writeValue(value: string): void {
    this.value = value ?? '';
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

  onInputChange(event: Event): void {
    const value = (event.target as HTMLInputElement).value;
    this.value = value;
    this.onChange(value);
    this.valueChange.emit(value);
  }

  getInputClass(): string {
    const classes = [this.class];

    if (this.size !== 'normal') {
      classes.push(`size-${this.size}`);
    }

    return classes.filter(Boolean).join(' ');
  }
}
