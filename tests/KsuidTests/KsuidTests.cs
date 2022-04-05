using System;
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
        public void TestShouldGetTimestamp()
        {
            var datetime = new DateTime(2017, 05, 17, 01, 49, 21, DateTimeKind.Utc);
            Assert.Equal((uint)94985761, Ksuid.GetTimestamp(datetime));
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
            Assert.Equal("D7B6FE8CD7CFF211704D8E7B9421210B", BitConverter.ToString(ksuid.GetPayload()).Replace("-",""));
        }

        [Fact]
        public void TestShouldCreateFromByteArray()
        {
            var ksuid = Ksuid.FromByteArray(new byte[]
                {5, 169, 94, 33, 215, 182, 254, 140, 215, 207, 242, 17, 112, 77, 142, 123, 148, 33, 33, 11});
            Assert.Equal("0o5Fs0EELR0fUjHjbCnEtdUwQe3", ksuid.ToString());
            Assert.Equal((uint)94985761, ksuid.GetTimestamp());
            Assert.Equal((uint)1494985761, ksuid.GetUnixTimestamp());
            Assert.Equal("D7B6FE8CD7CFF211704D8E7B9421210B", BitConverter.ToString(ksuid.GetPayload()).Replace("-",""));
        }

        [Fact]
        public void TestShouldGenerateDifferentPayload()
        {
            var ksuid1 = new Ksuid();
            var ksuid2 = new Ksuid();
            
            // this is due to the .NET Framework Random implementation,
            // which takes the seed as time-based and will produce same values
            // when two instances of System.Random are created within a short duration
            Assert.NotEqual(ksuid1.GetPayload(), ksuid2.GetPayload());
        }
    }
}