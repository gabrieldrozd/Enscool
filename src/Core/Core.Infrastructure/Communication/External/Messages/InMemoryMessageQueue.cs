using System.Threading.Channels;
using Core.Application.Communication.External.Messages;

namespace Core.Infrastructure.Communication.External.Messages;

internal sealed class InMemoryMessageQueue
{
    private readonly Channel<IMessage> _messageChannel = Channel.CreateUnbounded<IMessage>();

    public ChannelWriter<IMessage> Writer => _messageChannel.Writer;

    public ChannelReader<IMessage> Reader => _messageChannel.Reader;
}