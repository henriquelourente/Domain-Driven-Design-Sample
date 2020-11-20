using SampleLibrary.Core.Commands;

namespace SampleLibrary.Core.Interfaces
{
    public interface ICommandHandler<in T>  where T : CommandBase
    {
        Result Handle(T command);
    }
}