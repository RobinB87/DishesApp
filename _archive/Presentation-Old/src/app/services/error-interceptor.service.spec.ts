import { TestBed, inject } from '@angular/core/testing';

import { Services\errorInterceptorService } from './services\error-interceptor.service';

describe('Services\errorInterceptorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Services\errorInterceptorService]
    });
  });

  it('should be created', inject([Services\errorInterceptorService], (service: Services\errorInterceptorService) => {
    expect(service).toBeTruthy();
  }));
});
