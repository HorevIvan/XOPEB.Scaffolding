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

        public string Type
        {
            set => Set(value);

            get => Get<string>();
        }

        public int Index
        {
            set => Set(value);

            get => Get<int>();
        }

        public Column(string name, string description = null)
            //
            : base(name, description)
        {
        }
    }
}
