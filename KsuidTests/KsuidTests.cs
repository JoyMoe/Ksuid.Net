using System;
using Base62;
using KSUID;
using Xunit;

namespace KsuidTests
{
    public class KsuidTests
    {
        [Fact]
        public void TestShouldBeTrue()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestShouldConstruct()
        {
            var ksuid = new Ksuid();
            Assert.IsType<Ksuid>(ksuid);
        }

        [Fact]
        public void TestShouldBe20Bytes()
        {
            var ksuid = new Ksuid();
            Assert.Equal(20, ksuid.ToByteArray().Length);
        }

        [Fact]
        public void TestShouldBe27Characters()
        {
            var ksuid = new Ksuid();
            Assert.Equal(27, ksuid.ToString().Length);
        }

        [Fact]
        public void TestShouldCreateFromString()
        {
            var ksuid = Ksuid.FromString("0o5Fs0EELR0fUjHjbCnEtdUwQe3");
            Assert.Equal("0o5Fs0EELR0fUjHjbCnEtdUwQe3", ksuid.ToString());
            Assert.Equal((uint)94985761, ksuid.GetTimestamp());
            Assert.Equal((uint)1494985761, ksuid.GetUnixTimestamp());
            Assert.Equal("d7b6fe8cd7cff211704d8e7b9421210b", BitConverter.ToString(ksuid.GetPayload()).Replace("-",""));

            var datetime = new DateTime(ksuid.GetUnixTimestamp());
            Assert.Equal("2017-05-17 01:49:21", datetime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [Fact]
        public void TestShouldCreateFromByteArray()
        {
            var ksuid = Ksuid.FromByteArray("0o5Fs0EELR0fUjHjbCnEtdUwQe3".FromBase62());
            Assert.Equal("0o5Fs0EELR0fUjHjbCnEtdUwQe3", ksuid.ToString());
            Assert.Equal((uint)94985761, ksuid.GetTimestamp());
            Assert.Equal((uint)1494985761, ksuid.GetUnixTimestamp());
            Assert.Equal("d7b6fe8cd7cff211704d8e7b9421210b", BitConverter.ToString(ksuid.GetPayload()).Replace("-",""));

            var datetime = new DateTime(ksuid.GetUnixTimestamp());
            Assert.Equal("2017-05-17 01:49:21", datetime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}