using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace FTS.Precatorio.Domain.Converters
{
    public class DynamoDBEnumConverter<TEnum> : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;

            if (primitive == null || !(primitive.Value is String))
                throw new ArgumentOutOfRangeException();

            if (string.IsNullOrEmpty((string)primitive.Value))
                return null;

            return (TEnum)Enum.Parse(typeof(TEnum), primitive.Value.ToString());
        }

        public DynamoDBEntry ToEntry(object value)
        {
            var data = value.ToString();

            DynamoDBEntry entry = new Primitive { Value = data };

            return entry;
        }
    }
}
