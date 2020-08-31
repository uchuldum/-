import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';

@Injectable()

export class ConfigService {
  readonly resourceApiURI: string;
  readonly version: string;
  /*readonly storehousePrefix: string;
  readonly businessPrefix: string;
  readonly workingUnitPrefix: string;
  readonly imagesPrefix: string;*/

  constructor() {
    this.resourceApiURI = `${environment.apiUrl}`;
    this.version = environment.version;
  }
}
