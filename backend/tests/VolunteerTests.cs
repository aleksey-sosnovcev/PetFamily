using CSharpFunctionalExtensions;
using FluentAssertions;
using PetFamily.Domain.Enum;
using PetFamily.Domain.Pets;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;

namespace tests
{
    public class VolunteerTests
    {
        [Fact]
        public void Move_Pet_Should_Not_Move_When_Pet_Already_At_New_Position()
        {
            // Arrange
            const int petCount = 5;

            var volunteer = CreateVolunteerWithPet(petCount);

            var newPosition = Position.Create(2).Value;

            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var fourthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];

            //Act
            var result = volunteer.MovePet(secondPet, newPosition);

            //Assert

            result.IsSuccess.Should().BeTrue();
            //проверить позиции питомцев
            firstPet.Position.Value.Should().Be(1);
            secondPet.Position.Value.Should().Be(2);
            thirdPet.Position.Value.Should().Be(3);
            fourthPet.Position.Value.Should().Be(4);
            fifthPet.Position.Value.Should().Be(5);
        }

        [Fact]
        public void Move_Pet_Should_Move_Other_Pet_Forward_When_Position_Is_Lower()
        {
            // Arrange
            const int petCount = 5;

            var volunteer = CreateVolunteerWithPet(petCount);

            var newPosition = Position.Create(2).Value;

            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var fourthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];

            //Act
            var result = volunteer.MovePet(fourthPet, newPosition);

            //Assert

            result.IsSuccess.Should().BeTrue();
            //проверить позиции питомцев
            firstPet.Position.Value.Should().Be(1);
            secondPet.Position.Value.Should().Be(3);
            thirdPet.Position.Value.Should().Be(4);
            fourthPet.Position.Value.Should().Be(2);
            fifthPet.Position.Value.Should().Be(5);
        }

        [Fact]
        public void Move_Pet_Should_Move_Other_Pet_Back_When_Position_Is_Upper()
        {
            // Arrange
            const int petCount = 5;

            var volunteer = CreateVolunteerWithPet(petCount);

            var newPosition = Position.Create(4).Value;

            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var fourthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];

            //Act
            var result = volunteer.MovePet(secondPet, newPosition);

            //Assert

            // 1 2 3 4 5 
            // 1 2->4 3->2 4->3 5

            result.IsSuccess.Should().BeTrue();
            //проверить позиции питомцев
            firstPet.Position.Value.Should().Be(1);
            secondPet.Position.Value.Should().Be(4);
            thirdPet.Position.Value.Should().Be(2);
            fourthPet.Position.Value.Should().Be(3);
            fifthPet.Position.Value.Should().Be(5);
        }

        [Fact]
        public void Move_Pet_Should_Move_Other_Pet_Forward_When_Position_Is_First()
        {
            // Arrange
            const int petCount = 5;

            var volunteer = CreateVolunteerWithPet(petCount);

            var newPosition = Position.Create(1).Value;

            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var fourthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];

            //Act
            var result = volunteer.MovePet(fifthPet, newPosition);

            //Assert

            result.IsSuccess.Should().BeTrue();
            //проверить позиции питомцев
            firstPet.Position.Value.Should().Be(2);
            secondPet.Position.Value.Should().Be(3);
            thirdPet.Position.Value.Should().Be(4);
            fourthPet.Position.Value.Should().Be(5);
            fifthPet.Position.Value.Should().Be(1);
        }

        [Fact]
        public void Move_Pet_Should_Move_Other_Pet_Back_When_Position_Is_Last()
        {
            // Arrange
            const int petCount = 5;

            var volunteer = CreateVolunteerWithPet(petCount);

            var secondPosition = Position.Create(5).Value;

            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var fourthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];

            //Act
            var result = volunteer.MovePet(firstPet, secondPosition);

            //Assert

            result.IsSuccess.Should().BeTrue();
            //проверить позиции питомцев
            firstPet.Position.Value.Should().Be(5);
            secondPet.Position.Value.Should().Be(1);
            thirdPet.Position.Value.Should().Be(2);
            fourthPet.Position.Value.Should().Be(3);
            fifthPet.Position.Value.Should().Be(4);
        }

        [Fact]
        public void Deleted_Pet_Should_Move_Other_Pet_Back_()
        {
            // Arrange
            const int petCount = 5;

            var volunteer = CreateVolunteerWithPet(petCount);

            var secondPosition = Position.Create(5).Value;

            var firstPet = volunteer.Pets[0];
            var secondPet = volunteer.Pets[1];
            var thirdPet = volunteer.Pets[2];
            var fourthPet = volunteer.Pets[3];
            var fifthPet = volunteer.Pets[4];

            //Act
            var result = volunteer.DeletePet(secondPet);

            //Assert

            result.IsSuccess.Should().BeTrue();
            //проверить позиции питомцев
            firstPet.Position.Value.Should().Be(1);
            thirdPet.Position.Value.Should().Be(2);
            fourthPet.Position.Value.Should().Be(3);
            fifthPet.Position.Value.Should().Be(4);
        }

        [Fact]
        public void Add_Pet()
        {
            // Arrange
            var volunteer = CreateVolunteerWithPet(0);
            var pet = CreatePet();

            // Act
            var result = volunteer.AddPet(pet);

            // Assert
            result.IsSuccess.Should().BeTrue();
            volunteer.Pets.Count.Should().Be(1);
            volunteer.Pets.Should().Contain(pet);
        }

        // первый тест
        [Fact]
        public void Move_Pet_From_First_Position_To_First()
        {
            // Arrange
            var volunteer = CreateVolunteerWithPet(1);

            var secondPosition = Position.Create(1).Value;

            var firstPet = volunteer.Pets[0];

            // Act
            var result = volunteer.MovePet(firstPet, secondPosition);

            // Assert
            result.IsSuccess.Should().BeTrue();
            //проверить позиции задач
            firstPet.Position.Value.Should().Be(1);
        }

        private Volunteer CreateVolunteerWithPet(int petCount)
        {
            var fullName = FullName.Create("Test", "Test", "Test").Value;
            var email = Email.Create("test@mail.ru").Value;
            var description = Description.Create("Test").Value;
            var phoneNumber = PhoneNumber.Create("Test").Value;
            var details = Details.Create("Test", "Test").Value;
            var socialNetwork = new List<SocialNetwork>();
            var volunteer = new Volunteer(
                VolunteerId.NewVolunteerId(),
                fullName,
                email,
                description,
                phoneNumber,
                details,
                socialNetwork
                );

            for (var i = 0; i < petCount; i++)
            {
                var pet = CreatePet();
                volunteer.AddPet(pet);
            }

            return volunteer;
        }

        private Pet CreatePet()
        {
            var name = Name.Create("Test").Value;
            var descriptions = Description.Create("Test").Value;
            var infoHealth = InfoHealth.Create("Test").Value;
            var address = Address.Create("Test", "Test", "Test", "Test", "Test").Value;
            var phoneNumber = PhoneNumber.Create("Test").Value;
            var details = Details.Create("Test", "Test").Value;
            var pet = new Pet(
                PetId.NewPetId(),
                name, "Test",
                descriptions,
                "Test",
                "Test",
                infoHealth,
                address,
                default,
                default,
                phoneNumber,
                true,
                default,
                true,
                StatusType.NeedHelp,
                details,
                default);

            return pet;
        }
    }
}