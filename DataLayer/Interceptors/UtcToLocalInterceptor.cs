using System.Collections;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataLayer.Interceptors;

public class UtcToLocalInterceptor : DbCommandInterceptor
{
    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        return new ValueTask<DbDataReader>(new UtcToLocalDbDataReader(result));
    }
}

public class UtcToLocalDbDataReader : DbDataReader
{
    private readonly DbDataReader _source;

    public UtcToLocalDbDataReader(DbDataReader source)
    {
        _source = source;
    }

    public override DateTime GetDateTime(int ordinal)
    {
        DateTime datetime = _source.GetDateTime(ordinal);
        return datetime.Kind == DateTimeKind.Utc ? datetime.ToLocalTime() : datetime;
    }

    public override bool GetBoolean(int ordinal) => _source.GetBoolean(ordinal);
    public override byte GetByte(int ordinal) => _source.GetByte(ordinal);
    public override char GetChar(int ordinal) => _source.GetChar(ordinal);
    public override decimal GetDecimal(int ordinal) => _source.GetDecimal(ordinal);
    public override double GetDouble(int ordinal) => _source.GetDouble(ordinal);
    public override float GetFloat(int ordinal) => _source.GetFloat(ordinal);
    public override Guid GetGuid(int ordinal) => _source.GetGuid(ordinal);
    public override short GetInt16(int ordinal) => _source.GetInt16(ordinal);
    public override int GetInt32(int ordinal) => _source.GetInt32(ordinal);
    public override long GetInt64(int ordinal) => _source.GetInt64(ordinal);
    public override string GetString(int ordinal) => _source.GetString(ordinal);
    public override string GetValue(int ordinal) => _source.GetString(ordinal);
    public override int FieldCount => _source.FieldCount;
    public override bool IsDBNull(int ordinal) => _source.IsDBNull(ordinal);
    public override int GetValues(object[] values) => _source.GetValues(values);
    public override int GetOrdinal(string name) => _source.GetOrdinal(name);
    public override bool Read() => _source.Read();
    public override Type GetFieldType(int ordinal) => _source.GetFieldType(ordinal);
    public override string GetName(int ordinal) => _source.GetName(ordinal);
    public override object this[int ordinal] => _source[ordinal];
    public override object this[string name] => _source[name];
    public override int RecordsAffected => _source.RecordsAffected;
    public override long GetBytes(int ordinal, long dataOffset, byte[]? buffer, int bufferOffset, int length) => _source.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
    public override int Depth => _source.Depth;
    public override bool HasRows => _source.HasRows;
    public override bool IsClosed => _source.IsClosed;
    public override bool NextResult() => _source.NextResult();
    public override string GetDataTypeName(int ordinal) => _source.GetDataTypeName(ordinal);
    public override long GetChars(int ordinal, long dataOffset, char[]? buffer, int bufferOffset, int length) => _source.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
    public override IEnumerator GetEnumerator() => _source.GetEnumerator();
}