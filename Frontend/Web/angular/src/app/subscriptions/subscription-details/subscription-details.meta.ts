import { KeyValue } from "@angular/common";

export class SubscriptionDetailsMeta {
    public static destinationItems: KeyValue<number, string>[] = [
        { key: 0, value: 'Windows File Share' },
        { key: 1, value: 'Email' },
    ];

    public static renderFormatItems: KeyValue<number, string>[] = [
        { key: 0, value: 'Word' },
        { key: 1, value: 'Excel' },
        { key: 2, value: 'PowerPoint' },
        { key: 3, value: 'PDF' },
        { key: 4, value: 'Accessible PDF' },
        { key: 5, value: 'Tiff file' },
        { key: 6, value: 'MHTML(web archive)' },
        { key: 7, value: 'CSV(comma delimited)' },
        { key: 8, value: 'XML file with report data' },
        { key: 9, value: 'Data Feed' },
    ];

    public static priorityItems: KeyValue<number, string>[] = [
        { key: 0, value: 'Normal' },
        { key: 1, value: 'Low' },
        { key: 2, value: 'High' },
    ];

    public static scheduleTypeItems: KeyValue<number, string>[] = [
        { key: 0, value: 'Hour' },
        { key: 1, value: 'Day' },
        { key: 2, value: 'Week' },
        { key: 3, value: 'Month' },
        { key: 4, value: 'Once' }
    ];

    public static daysOfWeekItems: KeyValue<number, string>[] = [
        { key: 0, value: 'Sun' },
        { key: 1, value: 'Mon' },
        { key: 2, value: 'Tue' },
        { key: 3, value: 'Wed' },
        { key: 4, value: 'Thu' },
        { key: 5, value: 'Fri' },
        { key: 6, value: 'Sat' },
    ];

    public static monthsOfYearItems: KeyValue<number, string>[] = [
        { key: 0, value: 'Jan' },
        { key: 1, value: 'Feb' },
        { key: 2, value: 'Mar' },
        { key: 3, value: 'Apr' },
        { key: 4, value: 'May' },
        { key: 5, value: 'Jun' },
        { key: 6, value: 'Jul' },
        { key: 7, value: 'Aug' },
        { key: 8, value: 'Sep' },
        { key: 9, value: 'Oct' },
        { key: 10, value: 'Nov' },
        { key: 11, value: 'Dec' },
    ];

    public static weekOfMonthItems: KeyValue<number, string>[] = [
        { key: 0, value: '1st' },
        { key: 1, value: '2nd' },
        { key: 2, value: '3rd' },
        { key: 3, value: '4th' },
        { key: 4, value: 'Last' }
    ];
}