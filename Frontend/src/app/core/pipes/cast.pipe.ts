import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cast'
})
export class CastPipe implements PipeTransform {
  transform<S, T extends S>(value: S, type?: new () => T): T {
    return <T>value;
  }
}
