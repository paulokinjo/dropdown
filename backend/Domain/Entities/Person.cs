using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public enum Gender
    {
        Male,
        Female,
        Other,
    }

    public class Person
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthLocation { get; set; }
        public DateTime? DeathDate { get; set; }
        public string? DeathLocation { get; set; }


        [JsonPropertyName("fullName")]
        public string Fullname => GetFullName();

        public string GetFullName()
        {
            return $"{GivenName} {Surname} {GetLifeSpan()}";
        }

        public string GetLifeSpan()
        {
            if (BirthDate == default && DeathDate == null)
            {
                return "(Living)";
            }
            if (DeathDate != null)
            {
                if (BirthDate == default)
                {
                    return $"(-{DeathDate.Value.Year})";
                }
                return $"({BirthDate?.Year}-{DeathDate.Value.Year})";
            }
            if (BirthDate != default)
            {
                if (BirthDate?.AddYears(120) <= DateTime.Now)
                {
                    return $"({BirthDate?.Year}-)";
                }
                return "(Living)";
            }
            return "(Living)";
        }
    }
}
