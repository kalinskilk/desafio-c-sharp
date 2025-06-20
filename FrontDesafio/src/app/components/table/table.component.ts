import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SortEvent } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputNumber } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { Table, TableModule } from 'primeng/table';
import { TooltipModule } from 'primeng/tooltip';
import { debounceTime, Subject } from 'rxjs';

import { TableGrid } from './table-data.interface';

@Component({
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    TooltipModule,
    CheckboxModule,

    InputNumber,
    InputIconModule,
    IconFieldModule,
  ],
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export default class TableComponent implements OnInit {
  @Input() grid!: TableGrid;

  @Output() onSort = new EventEmitter<SortEvent>();
  @Output() onPage = new EventEmitter<any>();
  @Output() onSearch = new EventEmitter<string>();
  @Output() onToggleFilters = new EventEmitter<void>();
  @Output() onSelect = new EventEmitter<any>();
  @Output() onBlurEdit: EventEmitter<any> = new EventEmitter<any>();

  @Input() templatePaginatorright!: TemplateRef<any>;
  @ViewChild('dt') table!: Table;

  selectedItems: any[] | any = [];

  private searchSubject = new Subject<string>();
  searchValue: string | undefined;

  constructor() {
    this.searchSubject.pipe(debounceTime(750)).subscribe((searchTerm) => {
      // this.searchValue = searchTerm;
      this.onSearch.emit(searchTerm);
      if (this.table) {
        this.search(this.table, searchTerm);
      }
    });
  }

  ngOnInit(): void {
    if (this.grid && !this.grid.styleClass?.length) {
      this.grid.styleClass = 'py-3 px-3';
    }
  }

  /**
   * Triggers when user inputs in the search field
   * Debounces the search to avoid excessive API calls
   */
  onSearchInput(): void {
    this.searchSubject.next((this.searchValue || '').replace(/[^\w\s]/gi, ''));
  }

  /**
   * Performs search on the PrimeNG table
   * @param table - PrimeNG Table reference
   * @param value - The search term
   * @param columnField - Optional column field to filter by specific column
   * @param matchMode - Optional match mode ('contains', 'startsWith', 'equals', etc.)
   */
  search(
    table: Table,
    value: string,
    columnField?: string,
    matchMode: string = 'contains'
  ): void {
    if (columnField) {
      // Column-specific filtering
      table.filter(value, columnField, matchMode);
    } else {
      // Global filtering
      table.filterGlobal(value, matchMode);
    }
  }

  /**
   * Applies the current search value to the table
   * @param table - PrimeNG Table reference
   */
  applySearch(table: Table): void {
    if (this.searchValue) {
      this.search(table, this.searchValue);
    }
  }

  /**
   * Search in multiple columns at once
   * @param table - PrimeNG Table reference
   * @param value - The search term
   * @param fields - Array of column fields to search in
   * @param matchMode - Match mode to use
   */
  multiColumnSearch(
    table: Table,
    value: string,
    fields: string[],
    matchMode: string = 'contains'
  ): void {
    if (!value || !fields.length) return;

    // Clear any existing filters first
    table.clear();

    // Apply filters to each specified column
    fields.forEach((field) => {
      table.filter(value, field, matchMode);
    });
  }

  /**
   * Clears all filters from the table
   */
  clear(table: Table) {
    table.clear();
    this.searchValue = '';
    this.onSearch.emit('');
  }

  getTableClass(): string {
    const classes = [];
    if (this.grid.striped) classes.push('table-striped');
    if (this.grid.bordered) classes.push('table-bordered');
    if (this.grid.dense) classes.push('table-dense');
    return classes.join(' ');
  }

  getStatusClass(status: string): string {
    const normalizedStatus =
      typeof status === 'string' ? status?.toLowerCase() || '' : status;
    return `status-badge ${
      typeof status === 'string' && normalizedStatus.includes('sem')
        ? 'sem'
        : ''
    }`;
  }

  getColSpan(): number {
    let span = this.grid.columns.length;
    if (this.grid.selectable || this.grid.multiSelect) span++;
    if (this.grid.actions?.length) span++;
    return span;
  }

  onSelectionChange(event: any) {
    this.onSelect.emit(this.selectedItems);
  }

  onPaginate(event: any) {
    this.onPage.emit(event);
  }
}
