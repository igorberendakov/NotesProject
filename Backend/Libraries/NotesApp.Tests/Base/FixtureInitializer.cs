using AutoFixture;

namespace UnitTests.Base
{
    public static class FixtureInitializer
    {
        public static Fixture InitializeFixture()
        {
            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Freeze<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

            return fixture;
        }
    }
}
