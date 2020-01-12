using SampleLibrary.Core.Commands;

namespace SampleLibrary.Core.Interfaces
{
    public interface ICommandHandler<in T>  where T : Command
    {
        void Handle(T command);
    }
}