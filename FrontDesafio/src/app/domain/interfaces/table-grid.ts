export type ColumnType =
  | 'text'
  | 'date'
  | 'dateHour'
  | 'currency'
  | 'number'
  | 'status'
  | 'image'
  | 'custom'
  | 'telephone'
  | 'cpfCnpj'
  | 'editCurrency'
  | 'editFloat'
  | 'edit';

export interface TableColumn {
  field: string; // Campo do objeto que será exibido
  header: string; // Título da coluna
  type?: ColumnType; // Tipo de dado da coluna
  width?: string; // Largura da coluna (ex: '100px', '10%')
  sortable?: boolean; // Se a coluna pode ser ordenada
  frozen?: boolean; // Se a coluna é fixa
  template?: any; // Template customizado para a coluna
}

export interface TableAction {
  icon: string; // Ícone da ação (ex: 'pi pi-pencil')
  label?: string; // Texto da ação
  tooltip?: string; // Texto de ajuda
  class?: string; // Classes CSS adicionais
  disabled?: boolean; // Se a ação está desabilitada
  show?: boolean; // Se a ação deve ser exibida
  onClick: (item: any) => void; // Função executada ao clicar
}

export interface TableGrid {
  columns: TableColumn[]; // Colunas da tabela
  data: any[]; // Dados a serem exibidos
  actions?: TableAction[]; // Ações disponíveis
  showHeader?: boolean; // Mostrar cabeçalho
  showFooter?: boolean; // Mostrar rodapé
  pageSize?: number; // Itens por página
  pageSizeOptions?: number[]; // Opções de itens por página
  emptyMessage?: string; // Mensagem quando não há dados
  selectable?: boolean; // Permitir seleção de linhas
  multiSelect?: boolean; // Permitir seleção múltipla
  sortable?: boolean; // Permitir ordenação
  showSearch?: boolean; // Mostrar campo de busca
  searchPlaceholder?: string; // Placeholder do campo de busca
  showFilters?: boolean; // Mostrar filtros
  responsive?: boolean; // Tabela responsiva
  striped?: boolean; // Linhas alternadas
  hoverable?: boolean; // Efeito hover nas linhas
  bordered?: boolean; // Bordas na tabela
  dense?: boolean; // Modo compacto
  loading?: boolean; // Estado de carregamento
  selection?: any[] | any; // Itens selecionados
  totalRecords: number; // Total de registros
  paginator?: boolean; // Total de registros
  styleClass?: string; // Total de registros
}
