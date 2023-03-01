using AutoFixture;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Base
{
    public abstract class AutoMockerUnitTestsBase
    {
        private protected readonly AutoMocker _mocker = new();
        public Fixture Fixture { get; }

        public AutoMockerUnitTestsBase()
        {
            Fixture = FixtureInitializer.InitializeFixture();
        }

        public Mock<T> GetMock<T>() where T : class =>
            _mocker.GetMock<T>();

        public T GetService<T>() where T : class =>
            _mocker.Get<T>();

        public void Use<T>(T instance) where T : class
        {
            _mocker.Use(instance);
        }
    }
}
