using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

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

        public static Result<Address, Error> Create(
            string city,
            string street,
            string houseNumber,
            string apartment,
            string postalCode)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Errors.General.ValueIsInvalid("City");
            }
            if (string.IsNullOrWhiteSpace(street))
            {
                return Errors.General.ValueIsInvalid("Street");
            }
            if (string.IsNullOrWhiteSpace(houseNumber))
            {
                return Errors.General.ValueIsInvalid("House number");
            }
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                return Errors.General.ValueIsInvalid("Postal code");
            }

            var address = new Address(city, street, houseNumber, apartment, postalCode);

            return address;
        }

    }
}
