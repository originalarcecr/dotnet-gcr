using Microsoft.AspNetCore.Mvc;
using TodoApi.AttendanceAPI.Services;

namespace TodoApi.AttendanceAPI.Endpoints;

public static class AttendanceEndpoints
{
    public static void MapAttendanceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/AttendanceCodes/GetAll", ([FromServices] IAttendanceService _service) => _service.GetAllAttendanceCodes());

        app.MapGet("/timetable/{dayOfWeek}", ([FromServices] IAttendanceService _service, int dayOfWeek) => _service.GetTimetableByDay(dayOfWeek));
        app.MapGet("/timetable", ([FromServices] IAttendanceService _service) =>  _service.GetTimetableArray());
    }
}