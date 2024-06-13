import { formatDate } from '@angular/common';
export class Calendar {
  id: string;
  title: string;
  groupId: string;
  start: string;
  end: string;
  details: string;
  className: string;



  constructor(calendar: Calendar) {
    {
      this.id = calendar.id || '';
      this.title = calendar.title || '';
      this.groupId = calendar.groupId || '';
      this.start = formatDate(new Date(), 'yyyy-MM-dd', 'en') || '';
      this.end = formatDate(new Date(), 'yyyy-MM-dd', 'en') || '';
      this.details = calendar.details || '';
      this.className = calendar.className || '';
    }
  }
  public getRandomID(): number {
    const S4 = () => {
      return ((1 + Math.random()) * 0x10000) | 0;
    };
    return S4() + S4();
  }
}
