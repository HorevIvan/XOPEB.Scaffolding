using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOPEB.Scaffolding
{
    public class CSharpGenerator : LanguageGenerator
    {
        public bool UseStringMaxLength { set; get; }

        public string GetDatabaseClasses(IEnumerable<Table> tables)
        {
            var str = new StringBuilder();

            str.AppendLine("using System;");
            str.AppendLine("using System.ComponentModel.DataAnnotations;");

            str.AppendLine("");

            foreach (var table in tables)
            {
                var className = ToSingular(table.Name);

                str.AppendLine($"public partial class {className}");

                str.AppendLine("{");

                foreach (var column in table.Columns)
                {
                    if (column != table.Columns.First()) str.AppendLine("");

                    if (column.Type == "string" && UseStringMaxLength)
                    {
                        str.AppendLine($"\t[MaxLength(MaxLength{column.Name}_)]");
                    }

                    str.AppendLine($"\tpublic {column.Type}{(column.Nullable ? "?" : "")} {column.Name} {{ set; get; }} {(!UseStringMaxLength && column.Type == "string" ? $" // max {column.MaxLength}" : "")}");

                    if (column.Type == "string" && UseStringMaxLength)
                    {
                        str.AppendLine($"\tprivate const int MaxLength{column.Name}_ = {column.MaxLength};");
                        str.AppendLine($"\tpublic static int MaxLength{column.Name} => MaxLength{column.Name}_;");
                    }
                }

                str.AppendLine("}");

                str.AppendLine("");
            }

            return str.ToString();
        }
    }

    public class LanguageGenerator
    {
        public static string ToSingular(string name)
        {
            if (name.EndsWith("ies"))
            {
                return name.Substring(0, name.Length - 3) + "y";
            }

            return name.TrimEnd('s');
        }
    }
}
