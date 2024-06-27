using System.Text;

namespace StudentOption.Data;

public class SelectCommandBuilder : ICloneable
{
    private readonly HashSet<string> _fromTables = [];
    private readonly List<(string table, string entry)> _selectEntries = [];
    private readonly HashSet<((string table, string entry) entry1, (string table, string entry) entry2)> _whereEquals = [];
    private readonly HashSet<(string table, string entry, string param)> _whereEqualsParam = [];
    private (string table, string entry, bool isAsc) _orderBy = (string.Empty, string.Empty, false);
    private bool _orderBySpecified = false;

    public SelectCommandBuilder()
    {
        _fromTables = [];
        _selectEntries = [];
        _whereEquals = [];
        _whereEqualsParam = [];
        _orderBy = (string.Empty, string.Empty, false);
        _orderBySpecified = false;
    }

    private SelectCommandBuilder(
        HashSet<string> fromTables,
        List<(string table, string entry)> selectEntries,
        HashSet<((string table, string entry) entry1, (string table, string entry) entry2)> whereEquals,
        HashSet<(string table, string entry, string param)> whereEqualsParam,
        (string table, string entry, bool isAsc) orderBy,
        bool orderBySpecified)
    {
        _fromTables = fromTables;
        _selectEntries = selectEntries;
        _whereEquals = whereEquals;
        _whereEqualsParam = whereEqualsParam;
        _orderBy = orderBy;
        _orderBySpecified = orderBySpecified;
    }

    private static SelectCommandBuilder DeepCopy(
        HashSet<string> fromTables,
        List<(string table, string entry)> selectEntries,
        HashSet<((string table, string entry) entry1, (string table, string entry) entry2)> whereEquals,
        HashSet<(string table, string entry, string param)> whereEqualsParam,
        (string table, string entry, bool isAsc) orderBy,
        bool orderBySpecified)
    {
        return new(new(fromTables), new(selectEntries), new(whereEquals), new(whereEqualsParam), orderBy, orderBySpecified);
    }

    private void PAddSelectEntry(string table, string entry)
    {
        _fromTables.Add(table);
        _selectEntries.Add((table, entry));
    }
    public SelectCommandBuilder AddSelectEntry(string table, string entry)
    {
        var newBuilder = (SelectCommandBuilder)Clone();
        newBuilder.PAddSelectEntry(table, entry);
        return newBuilder;
    }
    private void PAddWhereEqual(string table1, string entry1, string table2, string entry2)
    {
        _fromTables.Add(table1);
        _fromTables.Add(table2);
        _whereEquals.Add(((table1, entry1), (table2, entry2)));
    }
    public SelectCommandBuilder AddWhereEqual(string table1, string entry1, string table2, string entry2)
    {
        var newBuilder = (SelectCommandBuilder)Clone();
        newBuilder.PAddWhereEqual(table1, entry1, table2, entry2);
        return newBuilder;
    }

    private void PAddWhereEqual(string table, string entry, string param)
    {
        _fromTables.Add(table);
        _whereEqualsParam.Add((table, entry, param));
    }
    public SelectCommandBuilder AddWhereEqual(string table, string entry, string param)
    {
        var newBuilder = (SelectCommandBuilder)Clone();
        newBuilder.PAddWhereEqual(table, entry, param);
        return newBuilder;
    }
    private void PChangeOrderBy(string table, string entry, bool isAsc)
    {
        _orderBy = (table, entry, isAsc);
        _orderBySpecified = true;
    }
    public SelectCommandBuilder ChangeOrderBy(string table, string entry, bool isAsc)
    {

        var newBuilder = (SelectCommandBuilder)Clone();
        newBuilder.PChangeOrderBy(table, entry, isAsc);
        return newBuilder;
    }
    public override string ToString()
    {
        StringBuilder sb = new();

        List<string> selects = [];
        _selectEntries.ForEach(e => selects.Add($"dbo.[{e.table}].[{e.entry}]"));
        sb.AppendLine($"SELECT {string.Join(", ", selects)}");

        List<string> froms = [];
        foreach (string tables in _fromTables)
        {
            froms.Add($"dbo.[{tables}]");
        }
        sb.AppendLine($"FROM {string.Join(", ", _fromTables)}");

        List<string> wheres = [];
        foreach (((string table1, string entry1), (string table2, string entry2)) in _whereEquals)
        {
            wheres.Add($"dbo.[{table1}].[{entry1}] = dbo.[{table2}].[{entry2}]");
        }
        foreach ((string table, string entry, string param) in _whereEqualsParam)
        {
            wheres.Add($"dbo.[{table}].[{entry}] = {param}");
        }
        if (wheres.Count != 0)
        {
            sb.AppendLine($"WHERE {string.Join(" AND ", wheres)}");
        }

        if (_orderBySpecified)
        {
            sb.AppendLine($"ORDER BY dbo.[{_orderBy.table}].[{_orderBy.entry}] {(_orderBy.isAsc ? "ASC" : "DESC")}");
        }

        return sb.ToString();
    }
    public object Clone()
    {
        return DeepCopy(_fromTables, _selectEntries, _whereEquals, _whereEqualsParam, _orderBy, _orderBySpecified);
    }
}