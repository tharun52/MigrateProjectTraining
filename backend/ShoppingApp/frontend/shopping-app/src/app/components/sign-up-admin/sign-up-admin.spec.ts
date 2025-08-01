import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpAdmin } from './sign-up-admin';

describe('SignUpAdmin', () => {
  let component: SignUpAdmin;
  let fixture: ComponentFixture<SignUpAdmin>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignUpAdmin]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignUpAdmin);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
