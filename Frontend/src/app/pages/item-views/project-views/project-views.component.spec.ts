import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectViewsComponent } from './project-views.component';

describe('ProjectViewsComponent', () => {
  let component: ProjectViewsComponent;
  let fixture: ComponentFixture<ProjectViewsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectViewsComponent]
    });
    fixture = TestBed.createComponent(ProjectViewsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
