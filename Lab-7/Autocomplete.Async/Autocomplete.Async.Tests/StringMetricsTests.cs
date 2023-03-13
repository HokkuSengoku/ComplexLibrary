namespace Autocomplete.Async.Tests
{
    using Autocomplete.Async;
    using NUnit.Framework;

    [TestFixture]
    public class StringMetricsTests
    {
        [Test]
        public void Similarity_OneOfInputsIsEmpty_ShouldBeZero()
        {
            Assert.That(string.Empty.Similarity(string.Empty), Is.EqualTo(0));
            Assert.That(string.Empty.Similarity(null), Is.EqualTo(0));
            Assert.That(StringMetrics.Similarity(null, string.Empty), Is.EqualTo(0));
            Assert.That("a".Similarity(string.Empty), Is.EqualTo(0));
            Assert.That("a".Similarity(null), Is.EqualTo(0));
            Assert.That(string.Empty.Similarity("a"), Is.EqualTo(0));
            Assert.That(StringMetrics.Similarity(null, "a"), Is.EqualTo(0));
        }

        [Test]
        public void Similarity_SingleCharSimilar_ShouldReturnOne()
        {
            Assert.That("a".Similarity("a"), Is.EqualTo(1));
            Assert.That("a".Similarity("ba"), Is.EqualTo(1));
            Assert.That("ba".Similarity("a"), Is.EqualTo(1));
            Assert.That("monopoly".Similarity("m"), Is.EqualTo(1)); // m -> 1
            Assert.That("monopoly".Similarity("y"), Is.EqualTo(1)); // y -> 1
        }

        [Test]
        public void Similarity_IdenticalInputs_ShouldReturnInputsSize()
        {
            Assert.That("ba".Similarity("ba"), Is.EqualTo(2));
            Assert.That("abracadabra".Similarity("abrAcaDabRa"), Is.EqualTo(11));
        }

        [Test]
        public void Similarity_NoCommonChars_ShouldReturnZero()
        {
            Assert.That("abracadabra".Similarity("system"), Is.EqualTo(0));
        }

        [Test]
        public void Similarity_CommonChars_ShouldCalculate()
        {
            Assert.That("abracadabra".Similarity("cadabra"), Is.EqualTo(7)); // cadabra -> 7
            Assert.That("abracadabra".Similarity("aaaaaaa"), Is.EqualTo(5)); // aaaaa -> 5
            Assert.That("monopoly".Similarity("yloponom"), Is.EqualTo(3)); // ooo | opo | ono -> 3
            Assert.That("quadrocycle".Similarity("helicopter"), Is.EqualTo(2)); // oe | ce -> 2
        }
    }
}