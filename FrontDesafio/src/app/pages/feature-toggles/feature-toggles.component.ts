import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { first } from 'rxjs';
import TableComponent from '../../components/table/table.component';
import {
  IConfigAmbiente,
  IConfigStatus,
} from '../../domain/interfaces/config-ambiente';
import { IFeatureToggles } from '../../domain/interfaces/feature-toggles';
import { ToastMessageService } from '../../infrastructure/services/toast.service';
import { featurToggleTableGrid } from './const';
import { DrawerCheckStatusComponent } from './drawer-check-status/drawer-check-status.component';
import { DrawerConfigAmbienteFeatureComponent } from './drawer-config-ambiente-feature/drawer-config-ambiente-feature.component';
import { DrawerEditFeatureComponent } from './drawer-edit-feature/drawer-edit-feature.component';
import { FeatureTogglesService } from './feature-toggles.service';

@Component({
  selector: 'app-feature-toggles',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    CardModule,
    TableComponent,
    DrawerEditFeatureComponent,
    DrawerConfigAmbienteFeatureComponent,
    DrawerCheckStatusComponent,
    ButtonModule,
  ],
  templateUrl: './feature-toggles.component.html',
  styleUrl: './feature-toggles.component.scss',
})
export class FeatureTogglesComponent implements OnInit {
  grid = featurToggleTableGrid;

  @ViewChild('drawerEditFeature')
  drawerEditFeature!: DrawerEditFeatureComponent;
  @ViewChild('drawerConfigAmbienteFeature')
  drawerConfigAmbienteFeature!: DrawerConfigAmbienteFeatureComponent;
  @ViewChild('drawerCheckStatus')
  drawerCheckStatus!: DrawerCheckStatusComponent;

  constructor(
    private featureTogglesService: FeatureTogglesService,
    private toastMessageService: ToastMessageService,
    private router: Router
  ) {
    this.grid.actions = [
      {
        icon: 'pi pi-pencil',
        tooltip: 'Editar',
        class: 'p-button-text p-button-primary',
        onClick: (item: IFeatureToggles) => this.onEdit(item),
      },
    ];
  }
  ngOnInit(): void {
    this.listFeatureToggles();
  }

  listFeatureToggles() {
    this.featureTogglesService
      .getFeatureToggles()
      .pipe(first())
      .subscribe((res) => {
        this.grid.data = res.map((item: IFeatureToggles) => ({
          ...item,
          status: item.ativoGlobalmente ? 'Ativo' : 'Inativo',
        }));
      });
  }

  onEdit(item: IFeatureToggles) {
    this.drawerEditFeature.open(item);
  }

  onNew() {
    this.drawerEditFeature.open();
  }

  onConfigOptions() {
    this.drawerConfigAmbienteFeature.open();
  }

  onCheckStatus() {
    this.drawerCheckStatus.open();
  }

  onSaveEdit(event: IFeatureToggles) {
    if (event.id) {
      this.onUpdate(event);
    } else {
      this.onSave(event);
    }
  }

  onSaveOrUpdateActions(msg: string) {
    this.toastMessageService.mostrarSucesso(msg);
    this.drawerEditFeature.closeDrawer();
    this.listFeatureToggles();
  }

  onSave(event: IFeatureToggles) {
    this.featureTogglesService
      .saveFeatureToggle(event)
      .pipe(first())
      .subscribe(() => {
        this.onSaveOrUpdateActions('Feature Toggle criado com sucesso');
      });
  }

  onUpdate(event: IFeatureToggles) {
    this.featureTogglesService
      .updateFeatureToggle(event.id, event)
      .pipe(first())
      .subscribe(() => {
        this.onSaveOrUpdateActions('Feature Toggle atualizado com sucesso');
      });
  }

  onBack() {
    this.router.navigate(['/home']);
  }

  onSaveConfigAmbiente(event: IConfigAmbiente) {
    this.featureTogglesService
      .featureToggleSetConfig(event.featureToggleId, event.ambienteId, {
        ativoNesteAmbiente: event.ativoNesteAmbiente,
      })
      .subscribe(() => {
        this.drawerConfigAmbienteFeature.close();
        this.toastMessageService.mostrarSucesso(
          'Configuração salva com sucesso'
        );
      });
  }

  onVerificaAtivoGlobalmente(item: IConfigStatus) {
    this.featureTogglesService
      .featureToggleStatus(item.featureToggleNome, item.ambienteNome)
      .subscribe((res) => {
        this.toastMessageService.mostrarSucesso(
          `Feature toogle ${res.ativo ? 'Ativo' : 'Inativo'} `
        );
      });
  }
}
