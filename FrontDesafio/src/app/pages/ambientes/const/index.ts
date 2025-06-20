import { TableGrid } from '../../../components/table/table-data.interface';

export const ambienteTableGrid: TableGrid = {
  columns: [
    {
      field: 'id',
      header: 'Identificador',
    },
    {
      field: 'nomeUnico',
      header: 'Nome único',
    },
  ],
  data: [],
  pageSize: 50,
  pageSizeOptions: [50, 100, 250],
  emptyMessage: 'Nenhuma feature toggle encontrada. Verifique os filtros.',
  sortable: true,
  showSearch: true,
  searchPlaceholder: 'Digite para buscar',
  showFilters: true,
  paginator: true,
  totalRecords: 0,
  statusBadge: [
    {
      color: 'success',
      value: 'true',
    },
    {
      color: 'danger',
      value: 'false',
    },
  ],
};
