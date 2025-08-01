import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsList } from './news-list';

describe('NewsList', () => {
  let component: NewsList;
  let fixture: ComponentFixture<NewsList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewsList]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewsList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
