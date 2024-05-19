using MonChenil.Entities;
using MonChenil.Repositories;
using MonChenil.Services;

namespace MonChenil.Tests;

public class TimeSlotsTests
{
    public static IEnumerable<object[]> OverlappingData =>
        [
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 11, 0, 0), EndDate = new(2024, 1, 1, 13, 0, 0) }
                }
            ],
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 9, 0, 0), EndDate = new(2024, 1, 1, 11, 0, 0) }
                }
            ],
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 9, 0, 0), EndDate = new(2024, 1, 1, 13, 0, 0) }
                }
            ],
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 13, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) }
                }
            ],
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 13, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 13, 0, 0) }
                }
            ]
        ];

    public static IEnumerable<object[]> NonOverlappingData =>
        [
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 13, 0, 0), EndDate = new(2024, 1, 1, 15, 0, 0) }
                }
            ],
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 9, 0, 0), EndDate = new(2024, 1, 1, 10, 0, 0) }
                }
            ],
            [
                new TimeSlot { StartDate = new(2024, 1, 1, 10, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) },
                new List<TimeSlot>
                {
                    new() { StartDate = new(2024, 1, 1, 12, 0, 0), EndDate = new(2024, 1, 1, 13, 0, 0) }
                }
            ]
        ];

    public static IEnumerable<object[]> BadDatesData =>
        [
            [new TimeSlot { StartDate = new(2024, 1, 1, 12, 0, 0), EndDate = new(2024, 1, 1, 10, 0, 0) }],
            [new TimeSlot { StartDate = new(2024, 1, 1, 12, 0, 0), EndDate = new(2024, 1, 1, 12, 0, 0) }]
        ];

    [Theory]
    [MemberData(nameof(NonOverlappingData))]
    public void WhenAddingTimeSlot_AndNoOverlappingTimeSlotExists_ThenTimeSlotIsCreated(
        TimeSlot timeSlot, List<TimeSlot> existingTimeSlots)
    {
        var timeSlotService = new TimeSlotService(new FakeRepository<TimeSlot>(existingTimeSlots));

        timeSlotService.Add(timeSlot);

        Assert.Contains(timeSlot, timeSlotService.GetAll());
    }

    [Theory]
    [MemberData(nameof(OverlappingData))]
    public void WhenAddingTimeSlot_AndOverlappingTimeSlotExists_ThenTimeSlotIsNotCreated_AndAnExceptionIsThrown(
        TimeSlot timeSlot, List<TimeSlot> existingTimeSlots)
    {
        var timeSlotService = new TimeSlotService(new FakeRepository<TimeSlot>(existingTimeSlots));

        void action() => timeSlotService.Add(timeSlot);

        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal("Time slot overlaps with another", ex.Message);
        Assert.DoesNotContain(timeSlot, timeSlotService.GetAll());
    }

    [Theory]
    [MemberData(nameof(BadDatesData))]
    public void WhenAddingTimeSlot_AndStartDateIsAfterEndDate_ThenAnExceptionIsThrown(
        TimeSlot timeSlot)
    {
        var timeSlotService = new TimeSlotService(new FakeRepository<TimeSlot>());

        void action() => timeSlotService.Add(timeSlot);

        var ex = Assert.Throws<ArgumentException>(action);
        Assert.Equal("Start date must be before end date", ex.Message);
        Assert.DoesNotContain(timeSlot, timeSlotService.GetAll());
    }
}