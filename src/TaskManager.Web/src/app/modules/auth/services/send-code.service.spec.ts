import { TestBed } from '@angular/core/testing';

import { SendCodeService } from './send-code.service';

describe('SendCodeService', () => {
  let service: SendCodeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SendCodeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
