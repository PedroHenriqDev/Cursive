namespace Cursive.Domain.ValueObjects
{
    public class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Name()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(FirstName) && string.IsNullOrEmpty(LastName))
                return $"{FirstName} {LastName}";

            return string.Empty;
        }
    }
}
