using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Files;
using PetFamily.Application.Messaging;
using PetFamily.Application.Providers;

namespace PetFamily.Infrastructure.Files
{
    public class FilesCleanerService : IFilesCleanerService
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<FilesCleanerService> _logger;
        private readonly IMessageQueue<IEnumerable<FileData>> _messageQueue;


        public FilesCleanerService(
            IFileProvider fileProvider,
            ILogger<FilesCleanerService> logger,
            IMessageQueue<IEnumerable<FileData>> messageQueue)
        {
            _fileProvider = fileProvider;
            _logger = logger;
            _messageQueue = messageQueue;
        }

        public async Task Process(CancellationToken cancellationToken)
        {
            var fileDatas = await _messageQueue.ReadAsync(cancellationToken);

            foreach (var fileData in fileDatas)
            {
                await _fileProvider.RemoveFile(fileData, cancellationToken);
            }
        }
    }
}
