using Domain.Entities;

namespace tests
{
    public class PersonTests
    {
        [Fact]
        public void Fullname_ShouldReturnCorrectFormat()
        {
            // Arrange  
            var person = new Person
            {
                GivenName = "John",
                Surname = "Doe",
                BirthDate = new DateTime(1980, 1, 1),
                DeathDate = new DateTime(2020, 1, 1)
            };

            // Act  
            var fullName = person.Fullname;

            // Assert  
            Assert.Equal("John Doe (1980-2020)", fullName);
        }

        [Fact]
        public void GetLifeSpan_ShouldReturnLiving_WhenNoBirthDateAndNoDeathDate()
        {
            // Arrange  
            var person = new Person();

            // Act  
            var lifeSpan = person.GetLifeSpan();

            // Assert  
            Assert.Equal("(Living)", lifeSpan);
        }

        [Fact]
        public void GetLifeSpan_ShouldReturnCorrectFormat_WhenOnlyDeathDateIsSet()
        {
            // Arrange  
            var person = new Person
            {
                DeathDate = new DateTime(2020, 1, 1)
            };

            // Act  
            var lifeSpan = person.GetLifeSpan();

            // Assert  
            Assert.Equal("(-2020)", lifeSpan);
        }

        [Fact]
        public void GetLifeSpan_ShouldReturnCorrectFormat_WhenBothBirthDateAndDeathDateAreSet()
        {
            // Arrange  
            var person = new Person
            {
                BirthDate = new DateTime(1980, 1, 1),
                DeathDate = new DateTime(2020, 1, 1)
            };

            // Act  
            var lifeSpan = person.GetLifeSpan();

            // Assert  
            Assert.Equal("(1980-2020)", lifeSpan);
        }

        [Fact]
        public void GetLifeSpan_ShouldReturnLiving_WhenBirthDateIsSetAndPersonIsNotOver120YearsOld()
        {
            // Arrange  
            var person = new Person
            {
                BirthDate = DateTime.Now.AddYears(-30)
            };

            // Act  
            var lifeSpan = person.GetLifeSpan();

            // Assert  
            Assert.Equal("(Living)", lifeSpan);
        }

        [Fact]
        public void GetLifeSpan_ShouldReturnCorrectFormat_WhenBirthDateIsSetAndPersonIsOver120YearsOld()
        {
            // Arrange  
            var person = new Person
            {
                BirthDate = new DateTime(1900, 1, 1)
            };

            // Act  
            var lifeSpan = person.GetLifeSpan();

            // Assert  
            Assert.Equal("(1900-)", lifeSpan);
        }

        [Fact]
        public void Fullname_ShouldReturnCorrectFormat_WhenOnlyGivenNameAndSurnameAreSet()
        {
            // Arrange  
            var person = new Person
            {
                GivenName = "Jane",
                Surname = "Doe"
            };

            // Act  
            var fullName = person.Fullname;

            // Assert  
            Assert.Equal("Jane Doe (Living)", fullName);
        }
    }
}