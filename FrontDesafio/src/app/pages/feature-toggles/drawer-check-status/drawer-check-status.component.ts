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
import { first, forkJoin } from 'rxjs';
import { DrawerComponent } from '../../../components/drawer/drawer.component';
import { InputSelectComponent } from '../../../components/input-select/input-select.component';
import IInputSelect from '../../../domain/interfaces/input-select.interface';
import { ToastMessageService } from '../../../infrastructure/services/toast.service';
import { AmbientesService } from '../../ambientes/ambientes.service';
import { FeatureTogglesService } from '../feature-toggles.service';

@Component({
  selector: 'app-drawer-check-status',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    InputSelectComponent,
    DrawerComponent,
    ButtonModule,
  ],
  templateUrl: './drawer-check-status.component.html',
  styleUrl: './drawer-check-status.component.scss',
})
export class DrawerCheckStatusComponent {
  formGroup: FormGroup;

  ambientes: IInputSelect[] = [];
  featureToggles: IInputSelect[] = [];

  @Output() onSubmitForm = new EventEmitter();

  @ViewChild('drawer') drawer!: DrawerComponent;

  constructor(
    private fb: FormBuilder,
    private featureTogglesService: FeatureTogglesService,
    private ambientesService: AmbientesService,
    private toastService: ToastMessageService
  ) {
    this.formGroup = this.fb.group({
      featureToggleNome: [null, Validators.required],
      ambienteNome: [null, Validators.required],
    });
  }

  open() {
    this.formGroup.reset({
      featureToggleNome: null,
      ambienteNome: null,
    });
    this.drawer.openDrawer();
    this.listAll();
  }

  close() {
    this.drawer.closeDrawer();
  }

  listAll() {
    forkJoin({
      ambientes: this.ambientesService.getAmbientes().pipe(first()),
      featureToggles: this.featureTogglesService
        .getFeatureToggles()
        .pipe(first()),
    }).subscribe(({ ambientes, featureToggles }) => {
      this.ambientes = ambientes.map((item) => ({
        label: item.nomeUnico,
        value: item.nomeUnico,
      }));
      this.featureToggles = featureToggles.map((item) => ({
        label: item.nomeUnico,
        value: item.nomeUnico,
      }));
    });
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
