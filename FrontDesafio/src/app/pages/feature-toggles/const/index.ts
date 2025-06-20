import { TableGrid } from '../../../components/table/table-data.interface';

export const featurToggleTableGrid: TableGrid = {
  columns: [
    {
      field: 'id',
      header: 'Identificador',
    },
    {
      field: 'nomeUnico',
      header: 'Nome único',
    },
    {
      field: 'descricao',
      header: 'Descrição',
    },
    {
      field: 'status',
      header: 'Ativo Globalmente',
      type: 'status',
    },
  ],

  pageSizeOptions: [50, 100, 250],
  emptyMessage: 'Nenhuma feature toggle encontrada. Verifique os filtros.',
  sortable: true,
  showSearch: true,
  searchPlaceholder: 'Digite para buscar',
  showFilters: true,
  paginator: true,
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

  data: [],
  pageSize: 50,
};
