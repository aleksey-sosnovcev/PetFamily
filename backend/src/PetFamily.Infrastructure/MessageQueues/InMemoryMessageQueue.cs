using PetFamily.Application.FileProvider;
using PetFamily.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.MessageQueues
{
    public class InMemoryMessageQueue<TMessage> : IMessageQueue<TMessage>
    {
        private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage>();
        
        public async Task WriteAsync(TMessage paths, CancellationToken cancellationToken = default)
        {
            await _channel.Writer.WriteAsync(paths, cancellationToken);
        }
        
        public async Task<TMessage> ReadAsync(CancellationToken cancellationToken = default)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
    }
}
