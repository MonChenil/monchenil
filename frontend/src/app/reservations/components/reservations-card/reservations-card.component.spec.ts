import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsCardComponent } from './reservations-card.component';

describe('ReservationsCardComponent', () => {
  let component: ReservationsCardComponent;
  let fixture: ComponentFixture<ReservationsCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReservationsCardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReservationsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
