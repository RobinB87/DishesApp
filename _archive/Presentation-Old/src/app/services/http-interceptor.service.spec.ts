import { TestBed, inject } from '@angular/core/testing';

import { Services\httpInterceptorService } from './services\http-interceptor.service';

describe('Services\httpInterceptorService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [Services\httpInterceptorService]
    });
  });

  it('should be created', inject([Services\httpInterceptorService], (service: Services\httpInterceptorService) => {
    expect(service).toBeTruthy();
  }));
});
