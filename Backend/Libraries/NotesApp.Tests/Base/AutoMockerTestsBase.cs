namespace UnitTests.Base
{
    public abstract class AutoMockerTestsBase<TTarget> : AutoMockerUnitTestsBase where TTarget : class
    {
        public AutoMockerTestsBase() : base()
        {
        }

        private TTarget _target = null!;
        public TTarget Target => _target ??= _mocker.CreateInstance<TTarget>();
    }
}
