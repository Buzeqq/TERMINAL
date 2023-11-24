import { Injectable } from '@angular/core';
import { MeasurementDetails } from '../../models/measurements/measurementDetails';
import { Project } from '../../models/projects/project';
import { ParameterValue } from '../../models/parameters/parameter-value';

@Injectable({
  providedIn: 'root'
})
export class ExportService {

  constructor() { }

  public exportMeasurement(details: MeasurementDetails, project: Project) {
    const csvContent = this.buildCSV(details, project);
    const blob = new Blob([csvContent], { type: 'text/csv' });
    const url = window.URL.createObjectURL(blob);
    window.open(url);
  }

  private groupParametersInColumns(details: MeasurementDetails): Record<string, ParameterValue[]> {
    const dict : Record<string,ParameterValue[]> = {};

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

  private buildCSV(details: MeasurementDetails, project: Project): string {
    const csvRows = [];

    var headers = "Code,Date,Project,Comment";

    var data = details.code + "," + details.createdAtUtc + "," + project.name + "," + details.comment;

    const parameters = this.groupParametersInColumns(details);

    for (let i = 0; i < details.steps.length; i++) {
      for (let [name, values] of Object.entries(parameters)) {
        if (i === 0){
          headers += "," + name;
        }
        data += "," + values[i].value;
      }
      data += "\n,,,";
    }

    csvRows.push(headers);
    csvRows.push(data);

    return csvRows.join('\n');
  }
}
