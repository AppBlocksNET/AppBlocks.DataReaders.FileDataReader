using System;
using System.Data;
using System.IO;

namespace AppBlocks.DataReaders
{
    public class FileDataReader : IDataReader
    {
        protected StreamReader Stream { get; set; }
        protected object[] Values;
        protected bool Eof { get; set; }
        protected string CurrentRecord { get; set; }
        protected int CurrentIndex { get; set; }

        public FileDataReader(string fileName)
        {
            Stream = new StreamReader(fileName);
            Values = new object[FieldCount];
        }

        public void Close()
        {
            Array.Clear(Values, 0, Values.Length);
            Stream.Close();
            Stream.Dispose();
        }

        public int Depth
        {
            get { return 0; }
        }

        public DataTable GetSchemaTable()
        {// avoid to implement several methods if your scenario do not demand it

            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { return Eof; }
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            CurrentRecord = Stream.ReadLine();
            Eof = CurrentRecord == null;

            if (!Eof)
            {
                Fill(Values);
                CurrentIndex++;
            }

            return !Eof;
        }

        private void Fill(object[] values)
        { // by default, the first position of the array holds the value that will be  
          // inserted at the first column of the table, and so on
          // lets assume here that the primary key is auto-generated
            values[0] = null;
            values[1] = CurrentRecord.Substring(0, 12).Trim();
            values[2] = CurrentRecord.Substring(12, 40).Trim();
        } // if the file is csv we could do a Split instead of Substring operations

        public int RecordsAffected
        {
            get { return -1; }
        }

        public int FieldCount
        {
            get { return 3; }
        }

        public object this[string name] => throw new NotImplementedException();

        public IDataReader GetData(int i)
        {
            if (i == 0)
                return this;

            return null;
        }

        public string GetDataTypeName(int i)
        {
            return "String";
        }

        public string GetName(int i)
        {
            return Values[i].ToString();
        }

        public string GetString(int i)
        {
            return Values[i].ToString();
        }

        public object GetValue(int i)
        {
            return Values[i];
        }

        public int GetValues(object[] values)
        {
            Fill(values);

            Array.Copy(values, Values, this.FieldCount);

            return this.FieldCount;
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object this[int i]
        {
            get { return Values[i]; }
        }
    }
}
