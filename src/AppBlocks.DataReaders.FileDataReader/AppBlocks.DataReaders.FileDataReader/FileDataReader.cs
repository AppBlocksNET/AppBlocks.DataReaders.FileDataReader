using AppBlocks.Models;
using System;
using System.Data;
using System.IO;
using System.Text.Json;

namespace AppBlocks.DataReaders
{
    public class FileDataReader : IDataReader
    {
        protected StreamReader Stream { get; set; }
        protected object[] Values;
        protected bool Eof { get; set; }
        protected string CurrentRecord { get; set; }
        protected int CurrentIndex { get; set; }

        public string FileName { get; set; }
        public FileDataReader(string fileName)
        {
            FileName = fileName;
            Stream = new StreamReader(fileName);
            //Values = new object[FieldCount];
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

            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetSchemaTable():{exception}");
            }
            return null;
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
            if (!FileName.EndsWith(".json"))
            {
                CurrentRecord = Stream.ReadLine();
            } else
            {
                var contents = Stream.ReadToEnd();
                Values = JsonSerializer.Deserialize<Item[]>(contents);
                return true;
            }

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

        public object this[string name] => this[name];

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
            return Values[i] != null && (Values[i].ToString() == "1" || Values[i].ToString() == "true");
        }

        public byte GetByte(int i)
        {
            try
            {
                if (Values[i] != null) return Convert.ToByte(Values[i]);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetByte({i}):{exception}");
            }
            return byte.MinValue;
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetBytes({i}):{exception}");
            }
            return int.MinValue;
        }

        public char GetChar(int i)
        {
            try
            {
                if (Values[i] != null) return Convert.ToChar(Values[i]);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetChar({i}):{exception}");
            }
            return char.MinValue;
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetChars({name}):{exception}");
            }
            return long.MinValue;
        }

        public DateTime GetDateTime(int i)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetDateTime({name}):{exception}");
            }
            return DateTime.MinValue;
        }

        public decimal GetDecimal(int i)
        {
            try
            {
                if (Values[i] != null) return Convert.ToDecimal(Values[i]);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetDecimal({i}):{exception}");
            }
            return decimal.MinValue;
        }

        public double GetDouble(int i)
        {
            try
            {
                if (Values[i] != null) return Convert.ToDouble(Values[i]);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetDouble({i}):{exception}");
            }
            return double.MinValue;
        }

        public Type GetFieldType(int i)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetFieldType({i}):{exception}");
            }
            return null;
        }

        public float GetFloat(int i)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetFloat({i}):{exception}");
            }
            return float.MinValue;
        }

        public Guid GetGuid(int i)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetGuid({i}):{exception}");
            }
            return Guid.Empty;
        }

        public short GetInt16(int i)
        {
            try
            {
                if (Values[i] != null) return Convert.ToInt16(Values[i]);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetInt16({i}):{exception}");
            }
            return short.MinValue;
        }

        public int GetInt32(int i)
        {
            try
            {
                if (Values[i] != null) return Convert.ToInt32(Values[i]);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetInt32({i}):{exception}");
            }
            return int.MinValue;
        }

        public long GetInt64(int i)
        {
            try
            {
                if (Values[i] != null) return Convert.ToInt64(Values[i]);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetInt64({i}):{exception}");
            }
            return long.MinValue;
        }

        public int GetOrdinal(string name)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.GetOrdinal({name}):{exception}");
            }
            return int.MinValue;
        }

        public bool IsDBNull(int i)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"ERROR in FileDataReader.IsDBNull({i}):{exception}");
            }
            return false;
        }

        public void Dispose()
        {
            Close();
        }

        public object this[int i]
        {
            get { return Values[i]; }
        }
    }
}
