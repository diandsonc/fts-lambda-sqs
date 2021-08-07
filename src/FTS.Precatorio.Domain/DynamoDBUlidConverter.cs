using System;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace FTS.Precatorio.Domain
{
    public class DynamoDBUlidConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            var ulid = value as Nullable<Ulid>;

            if (ulid == null) throw new ArgumentOutOfRangeException();

            string data = ulid.ToString();

            DynamoDBEntry entry = new Primitive { Value = data };

            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;

            if (primitive == null || !(primitive.Value is String) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();

            return Ulid.Parse(primitive.Value.ToString());
        }
    }
}