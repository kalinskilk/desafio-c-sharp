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
import { ToggleComponent } from '../../../components/toggle/toggle.component';
import { IFeatureToggles } from '../../../domain/interfaces/feature-toggles';
import { ToastMessageService } from '../../../infrastructure/services/toast.service';

@Component({
  selector: 'app-drawer-edit-feature',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    InputComponent,
    ToggleComponent,
    DrawerComponent,
    ButtonModule,
  ],
  templateUrl: './drawer-edit-feature.component.html',
  styleUrl: './drawer-edit-feature.component.scss',
})
export class DrawerEditFeatureComponent {
  public formGroup: FormGroup;

  @Output() onSubmitForm = new EventEmitter();

  @ViewChild('drawer') drawer!: DrawerComponent;

  constructor(
    private fb: FormBuilder,
    private toastService: ToastMessageService
  ) {
    this.formGroup = this.fb.group({
      nomeUnico: ['', Validators.required],
      descricao: ['', Validators.required],
      ativoGlobalmente: [true],
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
    } else {
      this.formGroup.markAllAsTouched();
      this.toastService.mostrarErro('Preencha todos os campos', false, 5000);
    }
  }
}
