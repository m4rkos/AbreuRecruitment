import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArtWorksComponent } from './art-works.component';

describe('ArtWorksComponent', () => {
  let component: ArtWorksComponent;
  let fixture: ComponentFixture<ArtWorksComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ArtWorksComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ArtWorksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
