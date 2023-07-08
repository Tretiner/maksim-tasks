using maxim_tasks;

namespace maxim_tasks_tests.Extensions;

internal class TestRequireEnglishLowercase
{
    [Test]
    [TestCase("valid")]
    public void should_not_throw_RequireEnglishLowercaseException(string text)
    {
        Assert.DoesNotThrow(() =>
        {
            text.RequireEnglishLowercase();
        });
    }

    [Test]
    [TestCase("inVALid")]
    [TestCase("1234")]
    [TestCase("eNgLiSh123!@#")]
    [TestCase("инвалид")]
    public void should_throw_RequireEnglishLowercaseException(string text)
    {
        Assert.Throws<RequireEnglishLowercaseException>(() =>
        {
            text.RequireEnglishLowercase();
        });
    }
}
