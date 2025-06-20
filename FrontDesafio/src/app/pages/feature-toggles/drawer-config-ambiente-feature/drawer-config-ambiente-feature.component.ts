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
import { ToggleComponent } from '../../../components/toggle/toggle.component';
import IInputSelect from '../../../domain/interfaces/input-select.interface';
import { ToastMessageService } from '../../../infrastructure/services/toast.service';
import { AmbientesService } from '../../ambientes/ambientes.service';
import { FeatureTogglesService } from '../feature-toggles.service';

@Component({
  selector: 'app-drawer-config-ambiente-feature',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    InputSelectComponent,
    DrawerComponent,
    ButtonModule,
    ToggleComponent,
  ],
  templateUrl: './drawer-config-ambiente-feature.component.html',
  styleUrl: './drawer-config-ambiente-feature.component.scss',
})
export class DrawerConfigAmbienteFeatureComponent {
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
      featureToggleId: [null, Validators.required],
      ambienteId: [null, Validators.required],
      ativoNesteAmbiente: [true],
    });
  }

  open() {
    this.formGroup.reset({
      featureToggleId: null,
      ambienteId: null,
      ativoNesteAmbiente: true,
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
        value: item.id,
      }));
      this.featureToggles = featureToggles.map((item) => ({
        label: item.nomeUnico,
        value: item.id,
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
