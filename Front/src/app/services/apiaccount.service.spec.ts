import { TestBed } from '@angular/core/testing';

import { ApiaccountService } from './apiaccount.service';

describe('ApiaccountService', () => {
  let service: ApiaccountService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiaccountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
