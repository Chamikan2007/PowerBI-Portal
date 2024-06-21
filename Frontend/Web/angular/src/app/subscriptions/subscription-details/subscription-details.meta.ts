export class SubscriptionDetailsMeta {
    public static destinationItems: any[] = [
        { label: 'Windows File Share', value: '1' },
        { label: 'Email', value: '2' },
    ];

    public static renderFormatItems: any[] = [
        { label: 'Word', value: '1' },
        { label: 'Excel', value: '2' },
        { label: 'PowerPoint', value: '3' },
        { label: 'PDF', value: '4' },
        { label: 'Accessible PDF', value: '5' },
        { label: 'Tiff file', value: '6' },
        { label: 'MHTML(web archive)', value: '7' },
        { label: 'CSV(comma delimited)', value: '8' },
        { label: 'XML file with report data', value: '9' },
        { label: 'Data Feed', value: '10' },
    ];

    public static priorityItems: any[] = [
        { label: 'Normal', value: '1' },
        { label: 'Low', value: '2' },
        { label: 'High', value: '3' },
    ];

    public static scheduleTypeItems: any[] = [
        { label: 'Hour', value: '1' },
        { label: 'Day', value: '2' },
        { label: 'Week', value: '3' },
        { label: 'Month', value: '4' },
        { label: 'Once', value: '5' }
    ];

    public static daysOfWeekItems: any[] = [
        { label: 'Sun', value: '1' },
        { label: 'Mon', value: '2' },
        { label: 'Tue', value: '3' },
        { label: 'Wed', value: '4' },
        { label: 'Thu', value: '5' },
        { label: 'Fri', value: '6' },
        { label: 'Sat', value: '7' },
    ];

    public static monthsOfYearItems: any[] = [
        { label: 'Jan', value: '1' },
        { label: 'Feb', value: '2' },
        { label: 'Mar', value: '3' },
        { label: 'Apr', value: '4' },
        { label: 'May', value: '5' },
        { label: 'Jun', value: '6' },
        { label: 'Jul', value: '7' },
        { label: 'Aug', value: '8' },
        { label: 'Sep', value: '9' },
        { label: 'Oct', value: '10' },
        { label: 'Nov', value: '11' },
        { label: 'Dec', value: '12' },
    ];

    public static weekOfMonthItems: any[] = [
        { label: '1st', value: '1' },
        { label: '2nd', value: '2' },
        { label: '3rd', value: '3' },
        { label: '4th', value: '4' },
        { label: 'Last', value: '5' }
    ];
}