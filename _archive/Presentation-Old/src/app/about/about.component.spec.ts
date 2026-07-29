import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutComponent } from './about.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('AboutComponent', () => {
  let component: AboutComponent;
  let fixture: ComponentFixture<AboutComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [
            RouterTestingModule
        ],
        declarations: [
          AboutComponent
        ]
    });

  TestBed.compileComponents();
  fixture = TestBed.createComponent(AboutComponent);
  component = fixture.componentInstance;
  });

  describe('initialization', () => {
    it('should create', () => {
      expect(component).toBeTruthy();
    });
  });
});