using ig.estimador.domain.Exceptions;
using ig.estimador.domain.ValueObjects;
using NUnit.Framework;

namespace ig.estimador.domain.UnitTests.ValueObjects
{
    [TestFixture]
    public class AdAccountTests
    {
        [Test]
        public void ShouldHaveCorrectDomainAndName()
        {
            const string accountString = "INTERGRUPO\\JYEPES";

            var account = AdAccount.For(accountString);

            Assert.AreEqual("INTERGRUPO", account.Domain);
            Assert.AreEqual("JYEPES", account.Name);
        }

        [Test]
        public void ToStringReturnsCorrectFormat()
        {
            const string accountString = "INTERGRUPO\\JYEPES";

            var account = AdAccount.For(accountString);

            var result = account.ToString();

            Assert.AreEqual(accountString, result);
        }

        [Test]
        public void ImplicitConversionToStringResultsInCorrectString()
        {
            const string accountString = "INTERGRUPO\\JYEPES";

            var account = AdAccount.For(accountString);

            string result = account;

            Assert.AreEqual(accountString, result);
        }

        [Test]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            const string accountString = "INTERGRUPO\\JYEPES";

            var account = (AdAccount)accountString;

            Assert.AreEqual("INTERGRUPO", account.Domain);
            Assert.AreEqual("JYEPES", account.Name);
        }

        [Test]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            const string accountString = "INTERGRUPOJYEPES";
           Assert.Throws<AdAccountInvalidException>(() => AdAccount.For(accountString));
        }
    }
}
