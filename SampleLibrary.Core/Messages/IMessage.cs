namespace SampleLibrary.Core.Messages
{
    public interface IMessage<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
