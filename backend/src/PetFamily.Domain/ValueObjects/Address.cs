using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects
{
    public record Address
    {
        public string City { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string Apartment { get; }
        public string PostalCode { get; }

        private Address(
            string city,
            string street,
            string houseNumber,
            string apartment,
            string postalCode)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Apartment = apartment;
            PostalCode = postalCode;
        }

        public static Result<Address> Create(
            string city,
            string street,
            string houseNumber,
            string apartment,
            string postalCode)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Result.Failure<Address>("City cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(street))
            {
                return Result.Failure<Address>("Street cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(houseNumber))
            {
                return Result.Failure<Address>("House number cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                return Result.Failure<Address>("Postal code cannot be empty");
            }

            return new Address(city, street, houseNumber, apartment, postalCode);
        }

    }
}
