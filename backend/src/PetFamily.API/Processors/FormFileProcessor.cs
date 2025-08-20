
using PetFamily.Application.VolunteerOperations.Dtos;

namespace PetFamily.API.Processors
{
    public class FormFileProcessor : IAsyncDisposable
    {
        private readonly List<CreateFileDto> _fileDto = [];

        public List<CreateFileDto> Process(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                var stream = file.OpenReadStream();
                var fileDto = new CreateFileDto(stream, file.Name);
                _fileDto.Add(fileDto);
            }

            return _fileDto;
        }
        public async ValueTask DisposeAsync()
        {
            foreach(var file in _fileDto)
            {
                await file.Stream.DisposeAsync();
            }
        }
    }
}
