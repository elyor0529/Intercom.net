using System;
using System.Diagnostics;
using System.Security.Principal;
using Newtonsoft.Json;

namespace Intercom
{
    public class EpochDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var t = long.Parse((string)reader.Value);
            return EpochTime.ConvertEpochToDateTime(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = ((DateTime) value).ConvertDateTimeToEpoch().ToString();
            writer.WriteRawValue(t);
        }
    }

    public static class EpochTime
    {
        private static DateTime _epochStartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ConvertEpochToDateTime(long seconds)
        {
            return _epochStartDateTime.AddSeconds(seconds);
        }

        public static long ConvertDateTimeToEpoch(this DateTime datetime)
        {
            if (datetime < _epochStartDateTime) return 0;

            return Convert.ToInt64(datetime.Subtract(_epochStartDateTime).TotalSeconds);
        }
    }
}