using System;
using System.Collections.Generic;
using System.Text;
using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests.Domain
{
    public class ClubTests
    {
        [Fact]
        public void Create_WithValidName_ReturnsClubWithNameAndNewId()
        {
            // Arrange
            const string name = "Tech Club";

            // Act
            var club = Club.Create(name, "Câu lạc bộ công nghệ");

            // Assert
            Assert.Equal(name, club.Name);
            Assert.NotEqual(Guid.Empty, club.Id);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WithBlankName_ThrowsArgumentException(string name)
        {
            // Arrange (tên rỗng hoặc toàn khoảng trắng đã nằm ở tham số)

            // Act
            var act = () => Club.Create(name, "mô tả");

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Create_WithNameLongerThan100_ThrowsArgumentException()
        {
            // Arrange
            var longName = new string('a', Club.MaxNameLength + 1);

            // Act
            var act = () => Club.Create(longName, "mô tả");

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(49, true)]
        [InlineData(50, false)]
        public void HasRoom_DependsOnCurrentMemberCount(int memberCount, bool expected)
        {
            // Arrange
            var club = Club.Create("Tech Club", "mô tả");

            // Act
            var result = club.HasRoom(memberCount);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
