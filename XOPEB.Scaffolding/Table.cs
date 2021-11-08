using System;
using System.Collections.Generic;
using System.Linq;

namespace XOPEB.Scaffolding
{
    public class Table : DatabaseObject
    {
        public IEnumerable<Column> Columns { get; }

        public Table(string name, IEnumerable<Column> columns, string description = null)
            //
            : base(name, description)
        {
            Columns = columns.ToArray();
        }
    }
}
