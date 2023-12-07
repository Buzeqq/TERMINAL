import { Injectable } from '@angular/core';
import { SampleDetails } from '../../models/samples/sampleDetails';
import { Project } from '../../models/projects/project';
import { ParameterValue } from '../../models/parameters/parameter-value';

@Injectable({
  providedIn: 'root'
})
export class ExportService {

  constructor() { }

  public exportSamples(details: SampleDetails, project: Project) {
    const csvContent = this.buildCSV(details, project);
    const blob = new Blob([csvContent], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    window.open(url);
  }

  private groupParametersInColumns(details: SampleDetails): Record<string, ParameterValue[]> {
    const dict : Record<string, ParameterValue[]> = {};

    details.steps.forEach(s => {
      s.parameters.forEach(p => {
        if (dict[p.name] == undefined) {
          dict[p.name] = [];
        }
        dict[p.name].push(p);
      });
    });

    return dict;
  }

  private buildCSV(details: SampleDetails, project: Project): string {
    const csvRows = [];

    let headers = "Code,Date,Project,Comment";
    let data = `${details.code},${details.createdAtUtc},${project.name},${details.comment}`;

    const parameters = this.groupParametersInColumns(details);

    for (let i = 0; i < details.steps.length; i++) {
      for (const [name, values] of Object.entries(parameters)) {
        if (i === 0) {
          headers += `,${name}`;
          if (values[0].unit) {
            headers += ` [${values[0].unit}]`;
          }
        }
        data += `,${values[i].value}`;
      }
      if (i === 0) {
        headers += ",Comment";
      }
      data += `,${details.steps[i].comment}\n,,,`;
    }

    csvRows.push(headers);
    csvRows.push(data);

    return csvRows.join('\n');
  }
}
