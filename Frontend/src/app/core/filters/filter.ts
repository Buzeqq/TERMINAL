import { map, Observable } from "rxjs";
import { Project } from "../models/projects/project";
import { Sample } from "../models/samples/sample";
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

  filterSamples(
    samples$: Observable<Sample[]>,
    phrase: string,
    filter: boolean
  ): Observable<Sample[]> {

    if (filter) {
      // TODO: filter by comment? comments are not returned by /api/samples/recent
      return samples$.pipe(
        map(samples => samples.filter(
          s => this.phraseMatch(s.code, phrase)
            || this.phraseMatch(s.project, phrase)
        )));
    } else {
      return new Observable<Sample[]>();
    }
  }

  private phraseMatch(a: string, b: string) {
    return a.toLowerCase().includes(b.toLowerCase());
  }

}
