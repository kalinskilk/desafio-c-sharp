export interface IBaseService {
  get?: (...params: any[]) => void;
  post?: (...params: any[]) => void;
}
