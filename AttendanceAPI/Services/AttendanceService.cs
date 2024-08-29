using TodoApi.AttendanceAPI.Models;

namespace TodoApi.AttendanceAPI.Services;

public interface IAttendanceService {
    public IEnumerable<AttendanceCode> GetAllAttendanceCodes();
    public List<TimeSlot> GetTimetableByDay(int dayOfWeek);

    public string[,] GetTimetableArray();
    
}

public class AttendanceService : IAttendanceService
{
    public IEnumerable<AttendanceCode> GetAllAttendanceCodes()
    {
        return
        [
            new AttendanceCode
            {
                Id = 1,
                Code = "P",
                Description = "Presente",
                Active = true
            },
            new AttendanceCode
            {
                Id = 2,
                Code = "TJ",
                Description = "Tardía Justificada",
                Active = true
            },
        ];
    }

    public List<TimeSlot> GetTimetableByDay(int dayOfWeek)
    {
        return FakeTimetableStore.GetTimeSlots().Where(ts => ts.DayOfWeek == dayOfWeek).ToList();
    }

    public string[,] GetTimetableArray()
    {
        return FakeTimetableStore.GetTimetableArray();
    }
}