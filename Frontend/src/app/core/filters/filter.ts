import { map, Observable } from "rxjs";
import { Project } from "../models/projects/project";
import { Measurement } from "../models/measurements/measurement";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class Filter {

  filterProjects(
    projects$: Observable<Project[]>,
    phrase: string,
    filter: boolean
  ): Observable<Project[]> {

    if (filter) {
      return projects$.pipe(
        map(projects => projects.filter(
          p => this.phraseMatch(p.name, phrase)
        )));
    } else {
      return new Observable<Project[]>();

    }
  }

  filterMeasurements(
    measurements$: Observable<Measurement[]>,
    phrase: string,
    filter: boolean
  ): Observable<Measurement[]> {

    if (filter) {
      // TODO: filter by comment? comments are not returned by /api/measurements/recent
      return measurements$.pipe(
        map(measurements => measurements.filter(
          m => this.phraseMatch(m.code, phrase)
            || this.phraseMatch(m.project, phrase)
        )));
    } else {
      return new Observable<Measurement[]>();
    }
  }

  private phraseMatch(a: string, b: string) {
    return a.toLowerCase().includes(b.toLowerCase());
  }

}
