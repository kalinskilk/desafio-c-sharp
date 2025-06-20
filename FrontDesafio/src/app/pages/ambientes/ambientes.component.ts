import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { first } from 'rxjs';
import TableComponent from '../../components/table/table.component';
import { ICreateAmbientes } from '../../domain/interfaces/ambientes';
import { ToastMessageService } from '../../infrastructure/services/toast.service';
import { AmbientesService } from './ambientes.service';
import { ambienteTableGrid } from './const';
import { DrawerEditAmbienteComponent } from './drawer-edit-ambiente/drawer-edit-ambiente.component';

@Component({
  selector: 'app-ambientes',
  imports: [
    CommonModule,
    DrawerEditAmbienteComponent,
    ButtonModule,
    TableComponent,
    CardModule,
  ],
  templateUrl: './ambientes.component.html',
  styleUrl: './ambientes.component.scss',
})
export class AmbientesComponent {
  grid = ambienteTableGrid;

  @ViewChild('drawerEditAmbiente')
  drawerEditAmbiente!: DrawerEditAmbienteComponent;

  constructor(
    private ambientesService: AmbientesService,
    private toastMessageService: ToastMessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.listAmbientes();
  }

  listAmbientes() {
    this.ambientesService
      .getAmbientes()
      .pipe(first())
      .subscribe((res) => {
        this.grid.data = res;
      });
  }

  onNew() {
    this.drawerEditAmbiente.open();
  }

  onSaveEdit(event: ICreateAmbientes) {
    this.onSave(event);
  }

  onSaveOrUpdateActions(msg: string) {
    this.toastMessageService.mostrarSucesso(msg);
    this.drawerEditAmbiente.closeDrawer();
    this.listAmbientes();
  }

  onSave(event: ICreateAmbientes) {
    this.ambientesService
      .saveAmbientes(event)
      .pipe(first())
      .subscribe(() => {
        this.onSaveOrUpdateActions('Ambiente criado com sucesso');
      });
  }

  onBack() {
    this.router.navigate(['/home']);
  }
}
