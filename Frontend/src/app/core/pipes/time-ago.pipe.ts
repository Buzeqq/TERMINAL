import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeAgo'
})
export class TimeAgoPipe implements PipeTransform {

  transform(value: Date): unknown {
    const now = new Date();
    const diff = Math.abs(now.getTime() - value.getTime()) / 1000;
    const minutes = Math.round(diff / 60);
    const hours = Math.round(minutes / 60);
    const days = Math.round(hours / 24);
    const weeks = Math.round(days / 7);
    const months = Math.round(weeks / 4);
    const years = Math.round(months / 12);

    if (minutes < 60) {
      return `${minutes}min ago`;
    } else if (hours < 24) {
      return `${hours}h ago`;
    } else if (days < 7) {
      return `${days}d ago`;
    } else if (weeks < 4) {
      return `${weeks}w ago`;
    } else if (months < 12) {
      return `${months}m ago`;
    } else {
      return `${years}y ago`;
    }
  }

}
