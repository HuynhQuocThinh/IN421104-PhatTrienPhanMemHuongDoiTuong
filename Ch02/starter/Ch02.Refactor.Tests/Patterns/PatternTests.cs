using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Domain;
using EduHub.Ch02.Logging;
using EduHub.Ch02.Notifications;
using EduHub.Ch02.Patterns.Observers;
using EduHub.Ch02.Patterns.Strategies;
using EduHub.Ch02.Repositories;
using EduHub.Ch02.Services;

namespace Ch02.Refactor.Tests.Patterns
{
    public class PatternTests
    {
        private sealed class FakeLogger : IAppLogger
        {
            public List<string> Lines { get; } = new();
            public void Info(string message) => Lines.Add(message);
        }

        private sealed class SpyObserver : IEventObserver
        {
            public int Calls { get; private set; }
            public void OnEventCreated(Event ev, Club club) => Calls++;
        }

        // ── Factory ──
        [Theory]
        [InlineData(NotificationChannel.Email, typeof(EmailNotificationService))]
        [InlineData(NotificationChannel.Sms, typeof(SmsNotificationService))]
        [InlineData(NotificationChannel.Slack, typeof(SlackNotificationService))]
        public void Factory_Create_ReturnsServiceMatchingChannel(NotificationChannel channel, Type expected)
        {
            // Arrange
            INotificationServiceFactory factory = new NotificationServiceFactory();

            // Act
            var service = factory.Create(channel);

            // Assert
            Assert.IsType(expected, service);
        }

        [Fact]
        public void Factory_Create_UnknownChannel_Throws()
        {
            // Arrange
            var factory = new NotificationServiceFactory();

            // Act
            var act = () => factory.Create((NotificationChannel)99);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        // ── Decorator ──
        [Fact]
        public void Decorator_Add_LogsAndStillStoresInInnerRepository()
        {
            // Arrange
            var logger = new FakeLogger();
            var inner = new InMemoryClubRepository();
            IClubRepository repo = new LoggingClubRepository(inner, logger);
            var club = Club.Create("Tech Club", "mô tả");

            // Act
            repo.Add(club);

            // Assert
            Assert.Contains(logger.Lines, l => l.Contains("Tech Club"));
            Assert.Same(club, inner.GetById(club.Id));
        }

        // ── Strategy ──
        [Fact]
        public void Strategy_SameData_RendersDifferentFormats()
        {
            // Arrange
            var stats = new List<ClubStat> { new("Tech Club", 2, 1, 2, 1) };

            // Act
            var console = new ConsoleReportStrategy().Render(stats);
            var csv = new CsvReportStrategy().Render(stats);

            // Assert
            Assert.StartsWith("Club: Tech Club", console);
            Assert.StartsWith("Club,Members", csv);
            Assert.Contains("\"Tech Club\",2,1,2,1", csv);
        }

        // ── Observer ──
        [Fact]
        public void Observer_CreateEvent_NotifiesEverySubscriber()
        {
            // Arrange
            var clubs = new InMemoryClubRepository();
            var club = Club.Create("Tech Club", "mô tả");
            clubs.Add(club);
            var service = new EventService(clubs, new InMemoryMemberRepository(), new InMemoryEventRepository());
            var first = new SpyObserver();
            var second = new SpyObserver();
            service.Subscribe(first);
            service.Subscribe(second);

            // Act
            service.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

            // Assert
            Assert.Equal(1, first.Calls);
            Assert.Equal(1, second.Calls);
        }

        [Fact]
        public void Observer_Unsubscribe_StopsNotifications()
        {
            // Arrange
            var clubs = new InMemoryClubRepository();
            var club = Club.Create("Tech Club", "mô tả");
            clubs.Add(club);
            var service = new EventService(clubs, new InMemoryMemberRepository(), new InMemoryEventRepository());
            var spy = new SpyObserver();
            service.Subscribe(spy);
            service.Unsubscribe(spy);

            // Act
            service.CreateEvent(club.Id, "Workshop SOLID", new DateTime(2026, 11, 20));

            // Assert
            Assert.Equal(0, spy.Calls);
        }
    }
}
