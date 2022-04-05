using System;
using Base62;
using JetBrains.Annotations;

namespace KSUID
{
    public class Ksuid
    {
        private const int EncodedSize = 27;
        private const int PayloadSize = 16;
        private const int TimestampSize = 4;
        private const uint Epoch = 1400000000;
        private static readonly DateTime EpochDateTime = new DateTime(2014, 05, 13, 16, 53, 20, DateTimeKind.Utc);
        private static readonly Random RandomGen = new Random();
        private readonly byte[] _payload;
        private readonly uint _timestamp;

        public Ksuid()
        {
            var byteArray = new byte[PayloadSize];
            lock (RandomGen)
            { 
                RandomGen.NextBytes(byteArray);
            }
            _payload = byteArray;

            _timestamp = Convert.ToUInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - Epoch);
        }

        public Ksuid([NotNull] byte[] payload)
        {
            _payload = payload;
            _timestamp = Convert.ToUInt32(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - Epoch);
        }

        public Ksuid([NotNull] byte[] payload, uint timestamp)
        {
            _payload = payload;
            _timestamp = timestamp;
        }

        public static uint GetTimestamp(DateTime value)
        {
            return Convert.ToUInt32((value - EpochDateTime).TotalSeconds);
        }

        public static Ksuid Generate()
        {
            return new Ksuid();
        }

        public static Ksuid FromString([NotNull] string text)
        {
            return FromByteArray(text.FromBase62());
        }

        public static Ksuid FromByteArray([NotNull] byte[] bytes)
        {
            var timestamp = new byte[TimestampSize];
            Array.Copy(bytes, 0, timestamp, 0, TimestampSize);
            if (BitConverter.IsLittleEndian) {
                Array.Reverse(timestamp);
            }

            var buffer = new byte[PayloadSize];
            Array.Copy(bytes, TimestampSize, buffer, 0, PayloadSize);

            return new Ksuid(buffer, BitConverter.ToUInt32(timestamp, 0));
        }

        public byte[] ToByteArray()
        {
            var timestamp = BitConverter.GetBytes(_timestamp);
            if (BitConverter.IsLittleEndian) {
                Array.Reverse(timestamp);
            }

            var buffer = new byte[timestamp.Length + _payload.Length];
            Array.Copy(timestamp, 0, buffer, 0, timestamp.Length);
            Array.Copy(_payload, 0, buffer, timestamp.Length, _payload.Length);

            return buffer;
        }

        public override string ToString()
        {
            return ToByteArray().ToBase62().PadLeft(EncodedSize, '0');
        }

        public byte[] GetPayload()
        {
            return _payload;
        }

        public uint GetTimestamp()
        {
            return _timestamp;
        }

        public uint GetUnixTimestamp()
        {
            return _timestamp + Epoch;
        }
    }
}
