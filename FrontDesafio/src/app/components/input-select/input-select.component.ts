import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Optional,
  Output,
  Self,
} from '@angular/core';
import {
  ControlValueAccessor,
  FormsModule,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import { SelectModule } from 'primeng/select';
import IInputSelect from '../../domain/interfaces/input-select.interface';

@Component({
  selector: 'app-input-select',
  imports: [CommonModule, SelectModule, FormsModule, ReactiveFormsModule],
  providers: [],
  templateUrl: './input-select.component.html',
  styleUrl: './input-select.component.scss',
})
export class InputSelectComponent implements ControlValueAccessor {
  @Input() options: IInputSelect[] = [];
  @Input() label = '';
  @Input() placeholder = 'Selecione um item';
  @Input() value: any;
  @Input() frmIsRequired: boolean = false;
  isDisabled: boolean = false;
  @Output() valueChange = new EventEmitter<any>();
  onChange: (value: any) => void = () => {};
  onTouched: () => void = () => {};

  constructor(@Optional() @Self() public control: NgControl) {
    if (this.control) {
      this.control.valueAccessor = this;
    }
  }

  writeValue(value: any): void {
    this.value = value;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  onSelect(event: any): void {
    this.value = event.value;
    this.onChange(this.value);
    this.onTouched();
    this.valueChange.emit(this.value);
  }
}
