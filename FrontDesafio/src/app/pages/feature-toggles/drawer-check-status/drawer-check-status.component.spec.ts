import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DrawerCheckStatusComponent } from './drawer-check-status.component';

describe('DrawerCheckStatusComponent', () => {
  let component: DrawerCheckStatusComponent;
  let fixture: ComponentFixture<DrawerCheckStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DrawerCheckStatusComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DrawerCheckStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
