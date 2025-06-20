export interface IFeatureToggles {
  id: number;
  nomeUnico: string;
  descricao: string;
  ativoGlobalmente: boolean;
}

export interface ICreateFeatureToggles {
  nomeUnico: string;
  descricao: string;
  ativoGlobalmente: boolean;
}

export interface IUpdateFeatureToggles {
  nomeUnico: string;
  descricao: string;
  ativoGlobalmente: boolean;
}
