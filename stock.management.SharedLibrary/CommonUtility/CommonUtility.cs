using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.management.SharedLibrary.CommonUtility
{
    public static class CommonUtility
    {
        private static readonly object _lock = new object();
        private static long _lastTimestamp = -1L;
        private static int _sequence = 0;
        private const int MaxSequence = 4095; // 12 bits for sequence

        public static int GenerateUniqueId()
        {
            long timestamp = GetCurrentTimestamp();
            if (timestamp == _lastTimestamp)
            {
                _sequence = (_sequence + 1) & MaxSequence;
                if (_sequence == 0)
                {
                    timestamp = WaitForNextMillis(_lastTimestamp);
                }
            }
            else
            {
                _sequence = 0;
            }

            _lastTimestamp = timestamp;

            // Generate a 6-digit number
            int newId = GenerateRandomId(timestamp, _sequence);
            return newId;
        }

        private static long GetCurrentTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        private static long WaitForNextMillis(long lastTimestamp)
        {
            long timestamp = GetCurrentTimestamp();
            while (timestamp <= lastTimestamp)
            {
                timestamp = GetCurrentTimestamp();
            }
            return timestamp;
        }

        private static int GenerateRandomId(long timestamp, int sequence)
        {
            // Combine timestamp and sequence to generate a 6-digit number
            int baseId = (int)((timestamp % 1000000) + sequence);
            return baseId % 900000 + 100000; // Ensure the ID is within the range 100000 to 999999
        }
    }
}
