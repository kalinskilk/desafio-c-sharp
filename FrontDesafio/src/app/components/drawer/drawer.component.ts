import { NgTemplateOutlet } from '@angular/common';
import { Component, Input, TemplateRef } from '@angular/core';
import { Drawer } from 'primeng/drawer';

@Component({
  selector: 'app-drawer',
  imports: [Drawer, NgTemplateOutlet],
  templateUrl: './drawer.component.html',
  styleUrl: './drawer.component.scss',
})
export class DrawerComponent {
  @Input() templateBody: string | TemplateRef<any> | any;
  @Input() position = 'right';
  @Input() header = '';

  visible = false;
  openDrawer() {
    this.visible = true;
  }

  closeDrawer() {
    this.visible = false;
  }
}
