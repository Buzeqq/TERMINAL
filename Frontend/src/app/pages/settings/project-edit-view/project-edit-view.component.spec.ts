import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProjectEditViewComponent } from './project-edit-view.component';

describe('ProjectsManagementViewComponent', () => {
  let component: ProjectEditViewComponent;
  let fixture: ComponentFixture<ProjectEditViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProjectEditViewComponent]
    });
    fixture = TestBed.createComponent(ProjectEditViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
