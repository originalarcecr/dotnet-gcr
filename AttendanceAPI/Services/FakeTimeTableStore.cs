
namespace TodoApi.AttendanceAPI.Services;

public class Period
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int GroupId { get; set; }
}

public class TimeSlot
{
    public int DayOfWeek { get; set; }
    public Period Period { get; set; }
    public Class Class { get; set; }
}


public class Timetable
{
    public List<Period> Periods { get; set; }
    public List<TimeSlot> TimeSlots { get; set; }
    public bool IncludeWeekends { get; set; }
}

public class TimetableClass
{
    public List<Period> Periods { get; set; }
    public List<TimeSlot> TimeSlots { get; set; }
    public bool IncludeWeekends { get; set; }

    public TimetableClass(bool includeWeekends = false)
    {
        Periods = new List<Period>();
        TimeSlots = new List<TimeSlot>();
        IncludeWeekends = includeWeekends;
    }

    public void AddPeriod(TimeSpan startTime, TimeSpan endTime)
    {
        Periods.Add(new Period { StartTime = startTime, EndTime = endTime });
    }

    public bool TryAddClass(Class classToAdd, int dayOfWeek, Period period)
    {
        if (!IncludeWeekends && (dayOfWeek == 0 || dayOfWeek == 6))
        {
            return false;
        }

        if (IsOverlapping(dayOfWeek, period, classToAdd.GroupId))
        {
            return false;
        }

        TimeSlots.Add(new TimeSlot
        {
            DayOfWeek = dayOfWeek,
            Period = period,
            Class = classToAdd
        });

        return true;
    }

    private bool IsOverlapping(int dayOfWeek, Period period, int groupId)
    {
        return TimeSlots.Any(ts =>
            ts.DayOfWeek == dayOfWeek &&
            ts.Class.GroupId == groupId &&
            ((ts.Period.StartTime <= period.StartTime && period.StartTime < ts.Period.EndTime) ||
             (ts.Period.StartTime < period.EndTime && period.EndTime <= ts.Period.EndTime) ||
             (period.StartTime <= ts.Period.StartTime && ts.Period.EndTime <= period.EndTime)));
    }

    public void GenerateTimetable(List<Class> classes)
    {
        Random random = new Random();
        foreach (var classItem in classes)
        {
            bool added = false;
            while (!added)
            {
                int dayOfWeek = IncludeWeekends ? random.Next(7) : random.Next(1, 6);
                Period period = Periods[random.Next(Periods.Count)];
                added = TryAddClass(classItem, dayOfWeek, period);
            }
        }
    }

    public string[,] GetTimetableArray()
{
    int rowCount = Periods.Count + 1;
    int colCount = IncludeWeekends ? 8 : 6;

    string[,] timetableArray = new string[rowCount, colCount];

    // Fill first row with day names
    timetableArray[0, 0] = "Period";
    for (int i = 1; i < colCount; i++)
    {
        timetableArray[0, i] = ((DayOfWeek)(i - 1)).ToString();
    }

    // Fill first column with period times
    for (int i = 0; i < Periods.Count; i++)
    {
        timetableArray[i + 1, 0] = $"{Periods[i].StartTime:hh\\:mm} - {Periods[i].EndTime:hh\\:mm}";
    }

    // Fill timetable with classes
    foreach (var timeSlot in TimeSlots)
    {
        int row = Periods.IndexOf(timeSlot.Period) + 1;
        int col = timeSlot.DayOfWeek + 1;
        timetableArray[row, col] = timeSlot.Class.Name;
    }

    return timetableArray;
}
}

public class FakeTimetableStore
{
    private static List<TimeSlot> timeSlots = new List<TimeSlot>
    {
        new TimeSlot
        {
            DayOfWeek = 1,
            Period = new Period { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(10) },
            Class = new Class { Id = 1, Name = "Math", GroupId = 1 }
        },
        new TimeSlot
        {
            DayOfWeek = 1,
            Period = new Period { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(11) },
            Class = new Class { Id = 2, Name = "English", GroupId = 1 }
        },
        new TimeSlot
        {
            DayOfWeek = 2,
            Period = new Period { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(10) },
            Class = new Class { Id = 3, Name = "Science", GroupId = 2 }
        },
        new TimeSlot
        {
            DayOfWeek = 2,
            Period = new Period { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(11) },
            Class = new Class { Id = 4, Name = "History", GroupId = 2 }
        },
        new TimeSlot
        {
            DayOfWeek = 3,
            Period = new Period { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(10) },
            Class = new Class { Id = 5, Name = "Geography", GroupId = 3 }
        },
        new TimeSlot
        {
            DayOfWeek = 3,
            Period = new Period { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(11) },
            Class = new Class { Id = 6, Name = "Art", GroupId = 3 }
        }
    };

    public static List<TimeSlot> GetTimeSlots()
    {
        return timeSlots;
    }

    public static string[,] GetTimetableArray()
    {
        var timetable = new TimetableClass(includeWeekends: false);
        timetable.Periods.Add(new Period { StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(10) });
        timetable.Periods.Add(new Period { StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(11) });

        var classes = new List<Class>
        {
            new Class { Id = 1, Name = "Math", GroupId = 1 },
            new Class { Id = 2, Name = "English", GroupId = 1 },
            new Class { Id = 3, Name = "Science", GroupId = 2 },
            new Class { Id = 4, Name = "History", GroupId = 2 },
            new Class { Id = 5, Name = "Geography", GroupId = 3 },
            new Class { Id = 6, Name = "Art", GroupId = 3 }
        };

        timetable.GenerateTimetable(classes);
        return timetable.GetTimetableArray();
    }
}