using AutoMapper;
using SampleLibrary.Core.Commands;
using SampleLibrary.Core.Entity;

namespace SampleLibrary.Application.AutoMapper
{
    public static class Mapper<TEntity, TCommand>
        where TEntity : Entity
        where TCommand : CommandBase
    {
        public static TEntity CommandToEntity(TCommand command)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCommand, TEntity>());

            var mapper = config.CreateMapper();
            return mapper.Map<TEntity>(command);
        }

        public static TCommand EntityToCommand(TEntity command)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEntity, TCommand>());

            var mapper = config.CreateMapper();
            return mapper.Map<TCommand>(command);
        }
    }
}