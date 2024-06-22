import { KeyValue } from "@angular/common";

export class SubscriptionDetailsMeta {
    public static destinationItems: KeyValue<string, string>[] = [
        { key: '1', value: 'Windows File Share' },
        { key: '2', value: 'Email' },
    ];

    public static renderFormatItems: KeyValue<string, string>[] = [
        { key: '1', value: 'Word' },
        { key: '2', value: 'Excel' },
        { key: '3', value: 'PowerPoint' },
        { key: '4', value: 'PDF' },
        { key: '5', value: 'Accessible PDF' },
        { key: '6', value: 'Tiff file' },
        { key: '7', value: 'MHTML(web archive)' },
        { key: '8', value: 'CSV(comma delimited)' },
        { key: '9', value: 'XML file with report data' },
        { key: '10', value: 'Data Feed' },
    ];

    public static priorityItems: KeyValue<string, string>[] = [
        { key: '1', value: 'Normal' },
        { key: '2', value: 'Low' },
        { key: '3', value: 'High' },
    ];

    public static scheduleTypeItems: KeyValue<string, string>[] = [
        { key: '1', value: 'Hour' },
        { key: '2', value: 'Day' },
        { key: '3', value: 'Week' },
        { key: '4', value: 'Month' },
        { key: '5', value: 'Once' }
    ];

    public static daysOfWeekItems: KeyValue<string, string>[] = [
        { key: '1', value: 'Sun' },
        { key: '2', value: 'Mon' },
        { key: '3', value: 'Tue' },
        { key: '4', value: 'Wed' },
        { key: '5', value: 'Thu' },
        { key: '6', value: 'Fri' },
        { key: '7', value: 'Sat' },
    ];

    public static monthsOfYearItems: KeyValue<string, string>[] = [
        { key: '1', value: 'Jan' },
        { key: '2', value: 'Feb' },
        { key: '3', value: 'Mar' },
        { key: '4', value: 'Apr' },
        { key: '5', value: 'May' },
        { key: '6', value: 'Jun' },
        { key: '7', value: 'Jul' },
        { key: '8', value: 'Aug' },
        { key: '9', value: 'Sep' },
        { key: '10', value: 'Oct' },
        { key: '11', value: 'Nov' },
        { key: '12', value: 'Dec' },
    ];

    public static weekOfMonthItems: KeyValue<string, string>[] = [
        { key: '1', value: '1st' },
        { key: '2', value: '2nd' },
        { key: '3', value: '3rd' },
        { key: '4', value: '4th' },
        { key: '5', value: 'Last' }
    ];
}