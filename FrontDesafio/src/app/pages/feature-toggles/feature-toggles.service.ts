import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IConfigStatusResponse } from '../../domain/interfaces/config-ambiente';
import {
  ICreateFeatureToggles,
  IFeatureToggles,
  IUpdateFeatureToggles,
} from '../../domain/interfaces/feature-toggles';
import { BaseService } from '../../infrastructure/services/base.service';

@Injectable({
  providedIn: 'root',
})
export class FeatureTogglesService extends BaseService {
  constructor(public override http: HttpClient) {
    super(http);
  }

  getFeatureToggles(): Observable<IFeatureToggles[]> {
    return this.get('/featureToggles');
  }

  saveFeatureToggle(
    featureToggle: ICreateFeatureToggles
  ): Observable<IFeatureToggles> {
    return this.post('/featureToggles', featureToggle);
  }

  updateFeatureToggle(
    id: number,
    featureToggle: IUpdateFeatureToggles
  ): Observable<IFeatureToggles> {
    return this.put(`/featureToggles/${id}`, featureToggle);
  }

  featureToggleSetConfig(
    featureToggleId: number,
    ambienteId: number,
    input: { ativoNesteAmbiente: boolean }
  ): Observable<IFeatureToggles> {
    return this.post(
      `/featureToggles/${featureToggleId}/ambientes/${ambienteId}/config`,
      input
    );
  }

  featureToggleStatus(
    featureName: string,
    environmentName: string
  ): Observable<IConfigStatusResponse> {
    return this.get(
      `/featureToggles/status?featureName=${featureName}&environmentName=${environmentName}`
    );
  }
}
