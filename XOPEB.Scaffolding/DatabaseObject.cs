namespace XOPEB.Scaffolding
{
    public class DatabaseObject : Meta
    {
        public string Name
        {
            set => Set(value);

            get => Get<string>();
        }

        public string Description
        {
            set => Set(value);

            get => Get<string>();
        }

        public DatabaseObject(string name, string description = null)
        {
            Name = name;

            Description = description;
        }
    }
}
