using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace FTS.Precatorio.Domain.Converters
{
    public class DynamoDBGuidConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;

            if (primitive == null || !(primitive.Value is String))
                throw new ArgumentOutOfRangeException();

            if (string.IsNullOrEmpty((string)primitive.Value))
                return null;

            return Guid.Parse(primitive.Value.ToString());
        }

        public DynamoDBEntry ToEntry(object value)
        {
            var guid = value as Nullable<Guid>;
            var data = guid.ToString();

            DynamoDBEntry entry = new Primitive { Value = data };

            return entry;
        }
    }
}