using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.FileProvider
{
    public record FileData(FilePath FilePath, string BucketName);
    public record StreamFileData(Stream Stream, FileData FileData);

   //public record FileData(Stream Stream, FilePath FilePath, string BucketName);
}