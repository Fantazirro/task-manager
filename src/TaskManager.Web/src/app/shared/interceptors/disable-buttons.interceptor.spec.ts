import { TestBed } from '@angular/core/testing';

import { DisableButtonsInterceptor } from './disable-buttons.interceptor';

describe('DisableButtonsInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      DisableButtonsInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: DisableButtonsInterceptor = TestBed.inject(DisableButtonsInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
