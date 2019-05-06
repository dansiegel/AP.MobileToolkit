using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests
{
    public abstract class TestBase
    {
        protected ITestOutputHelper _testOutputHelper { get; }

        public TestBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
#if !DISABLE_FORMS_INIT
            Xamarin.Forms.Mocks.MockForms.Init();
#endif
        }
    }
}