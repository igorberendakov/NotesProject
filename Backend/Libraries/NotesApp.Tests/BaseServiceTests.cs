using AutoMapper;
using NotesApp.Infrastructure.Mapping.Profiles;
using UnitTests.Base;

namespace NotesApp.Tests
{
    public class BaseServiceTests<TService> : AutoMockerTestsBase<TService> where TService : class
    {
        public BaseServiceTests() 
        {
            var configurationProvider = new MapperConfiguration(options =>
            {
                options.AddProfile(new NoteProfile());
                options.AddProfile(new NotificationProfile());
                options.AddProfile(new TagProfile());
            });

            var mapper = new Mapper(configurationProvider);
            Use<IMapper>(mapper);
        }
    }
}