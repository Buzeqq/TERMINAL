import { Component } from '@angular/core';
import {DbService} from "../../core/services/db/db.service";
import { UUID } from "angular2-uuid";

@Component({
  selector: 'app-dexie-tester',
  templateUrl: './dexie-tester.component.html',
  styleUrls: ['./dexie-tester.component.scss']
})
export class DexieTesterComponent {

  constructor(
    private readonly dbService: DbService
  ) {  }

  addProjects() {
    const projects = [
      {id: UUID.UUID(), name: 'Pro01'},
      {id: UUID.UUID(), name: 'Pro02'},
      {id: UUID.UUID(), name: 'Pro03'}
    ]
    this.dbService.addProjects(projects)
  }

  printAllProjects() {
    this.dbService.getProjects(0, 10)
      .then(projects => console.log(projects))
  }
}
