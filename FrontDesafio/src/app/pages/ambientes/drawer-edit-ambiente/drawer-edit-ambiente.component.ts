import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DrawerComponent } from '../../../components/drawer/drawer.component';
import { InputComponent } from '../../../components/input/input.component';
import { IFeatureToggles } from '../../../domain/interfaces/feature-toggles';

@Component({
  selector: 'app-drawer-edit-ambiente',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    InputComponent,
    DrawerComponent,
    ButtonModule,
  ],
  templateUrl: './drawer-edit-ambiente.component.html',
  styleUrl: './drawer-edit-ambiente.component.scss',
})
export class DrawerEditAmbienteComponent {
  public formGroup: FormGroup;

  @Output() onSubmitForm = new EventEmitter();

  @ViewChild('drawer') drawer!: DrawerComponent;

  constructor(private fb: FormBuilder) {
    this.formGroup = this.fb.group({
      nomeUnico: ['', Validators.required],
      id: [null],
    });
  }

  open(item?: IFeatureToggles) {
    if (item?.id) {
      this.formGroup.patchValue(item);
    } else {
      this.formGroup.reset();
    }
    this.drawer.openDrawer();
  }

  closeDrawer() {
    this.drawer.closeDrawer();
  }

  onSubmit() {
    if (this.formGroup.valid) {
      this.onSubmitForm.emit(this.formGroup.value);
    }
  }
}
