export interface IConfigAmbiente {
  ambienteId: number;
  featureToggleId: number;
  ativoNesteAmbiente: boolean;
}

export interface IConfigStatus {
  featureToggleNome: string;
  ambienteNome: string;
}

export interface IConfigStatusResponse {
  ativo: boolean;
}
