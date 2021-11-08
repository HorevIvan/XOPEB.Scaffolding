using System;

namespace XOPEB.Scaffolding
{
    public class Column : DatabaseObject
    {
        public byte Scale
        {
            set => Set(value);

            get => Get<byte>();
        }

        public byte Precision
        {
            set => Set(value);

            get => Get<byte>();
        }

        public bool Nullable
        {
            set => Set(value);

            get => Get<bool>();
        }

        public Column(string name, bool nullable, byte scale = 0, byte precision = 0, string description = null)
            //
            : base(name, description)
        {
            Nullable = nullable;

            Scale = scale;

            Precision = precision;
        }
    }
}
