using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public class ErrorList(IEnumerable<Error> errors) : IEnumerable<Error>
    {
        private readonly List<Error> _errors = errors.ToList();
        public IEnumerator<Error> GetEnumerator()
        {
            return _errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator ErrorList(List<Error> errors) => new(errors);
        public static implicit operator ErrorList(Error error) => new([error]);
    }
}
