import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactUsForm } from './contact-us-form';

describe('ContactUsForm', () => {
  let component: ContactUsForm;
  let fixture: ComponentFixture<ContactUsForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactUsForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContactUsForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
