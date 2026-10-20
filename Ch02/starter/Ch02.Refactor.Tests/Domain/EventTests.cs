using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests.Domain
{
    public class EventTests
    {
        private static Event NewEvent() =>
            new() { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop SOLID", Date = new DateTime(2026, 11, 20) };

        [Fact]
        public void Register_NewMember_ReturnsTrueAndStoresMember()
        {
            // Arrange
            var ev = NewEvent();
            var memberId = Guid.NewGuid();

            // Act
            var added = ev.Register(memberId);

            // Assert
            Assert.True(added);
            Assert.Contains(memberId, ev.RegisteredMemberIds);
        }

        [Fact]
        public void Register_SameMemberTwice_ReturnsFalseAndKeepsOneEntry()
        {
            // Arrange
            var ev = NewEvent();
            var memberId = Guid.NewGuid();
            ev.Register(memberId);

            // Act
            var addedAgain = ev.Register(memberId);

            // Assert
            Assert.False(addedAgain);
            Assert.Single(ev.RegisteredMemberIds);
        }

        [Fact]
        public void CheckIn_RegisteredMember_RecordsCheckIn()
        {
            // Arrange
            var ev = NewEvent();
            var memberId = Guid.NewGuid();
            ev.Register(memberId);

            // Act
            ev.CheckIn(memberId);

            // Assert
            Assert.Contains(memberId, ev.CheckedInMemberIds);
        }

        [Fact]
        public void CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
        {
            // Arrange
            var ev = NewEvent();

            // Act
            var act = () => ev.CheckIn(Guid.NewGuid());

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void AttendanceRate_WithNoRegistration_IsZero()
        {
            // Arrange
            var ev = NewEvent();

            // Act
            var rate = ev.AttendanceRate;

            // Assert
            Assert.Equal(0, rate);
        }

        [Fact]
        public void AttendanceRate_OneOfTwoCheckedIn_IsHalf()
        {
            // Arrange
            var ev = NewEvent();
            var a = Guid.NewGuid();
            var b = Guid.NewGuid();
            ev.Register(a);
            ev.Register(b);
            ev.CheckIn(a);

            // Act
            var rate = ev.AttendanceRate;

            // Assert
            Assert.Equal(0.5, rate);
        }
    }
}
