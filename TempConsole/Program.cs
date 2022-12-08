using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace TempConsole
{
    class Program
    {
		static void Main(string[] args)
		{
			Stopwatch st = Stopwatch.StartNew();
			//var data = Data.Get();
			for (var i = 0; i < 1*10000; i++)
			{
				//var a = TryParseValue<string>(null);
				//var b = TryParseValue<int>(null);
				var c = TryParseValue<int>("");
				//var d = TryParseValue<string>("");

				int t = i % 10000;
				if (i < 10000)
				{
					t = i;
				}
				if (t == 0)
				{
					Console.WriteLine(i);
				}

			}
			st.Stop();
			Console.Write(st.ElapsedMilliseconds);
			Console.ReadLine();
		}

		public static T TryParseValue<T>(object value, T defaultValue = default(T))
		{
			return ValueHelper.TryParseValue<T>(value, defaultValue);
		}


	}

	public class Data
	{
		public static Dictionary<string, object> Get()
		{
			var data = new Dictionary<string, object>();
			for (var i = 0; i < 100; i++)
			{
				int t = i % 5;
				if (i < 5)
                {
					t = i;
                }
				if (t == 0)
				{
					data.Add($"{t}-{i}",1);				   
				}
				if (t == 1)
				{
					data.Add($"{t}-{i}", "abc");
				}
				if (t == 2)
				{
					data.Add($"{t}-{i}", DateTime.Now);
				}
				if (t == 3)
				{
					data.Add($"{t}-{i}", null);
				}
				if (t == 4)
				{
					data.Add($"{t}-{i}", "");
				}
			}
			return data;
		}	   
	}

	public class ValueHelper
	{
		private static Type LongType = typeof(long);

		private static Type BoolType = typeof(bool);

		private static char[] rDigits = new char[36]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z'
		};

		private const string BASE_CHAR = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public static T TryGetValue<T>(IDictionary<string, object> dictionary, string key)
		{
			return ValueHelper.TryGetValue(dictionary, key, default(T));
		}

		public static bool Contains<T>(IEnumerable<T> srcs, IEnumerable<T> checks)
		{
			foreach (T check in checks)
			{
				if (srcs.Contains(check))
				{
					return true;
				}
			}
			return false;
		}

		public static T ContainsValue<T>(IEnumerable<T> srcs, IEnumerable<T> checks, T noContainsValue)
		{
			foreach (T check in checks)
			{
				if (srcs.Contains(check))
				{
					return check;
				}
			}
			return noContainsValue;
		}

		public static bool Contains<T>(IEnumerable<T> srcs, IEnumerable<T> checks, IEqualityComparer<T> comparer)
		{
			foreach (T check in checks)
			{
				if (srcs.Contains(check, comparer))
				{
					return true;
				}
			}
			return false;
		}

		public static int Random(int min, int max)
		{
			Random random = new Random(DateTime.Now.GetHashCode());
			return random.Next(min, max);
		}

		public static T ContainsValue<T>(IEnumerable<T> srcs, IEnumerable<T> checks, IEqualityComparer<T> comparer, T noContainsValue)
		{
			foreach (T check in checks)
			{
				if (srcs.Contains(check, comparer))
				{
					return check;
				}
			}
			return noContainsValue;
		}

		public static T TryParseValue<T>(object value, T defaultValue = default(T))
		{
			try
			{
				if (value is T)
				{
					return (T)value;
				}
				if (ValueHelper.CheckValueIsDBNull(value))
				{
					return defaultValue;
				}
				Type typeFromHandle = typeof(T);

                if (value.GetType() == typeof(string)
                                && string.IsNullOrEmpty((string)value))
                {
                    return typeFromHandle == typeof(string) ? (T)value : defaultValue;
                }

                if (typeFromHandle.IsEnum)
				{
					return ValueHelper.GetEnum<T>(value);
				}
				if (value is byte[] && typeFromHandle == ValueHelper.LongType)
				{
					byte[] source = value as byte[];
					value = BitConverter.ToInt64(source.Reverse().ToArray(), 0);
					return (T)value;
				}
				if (typeFromHandle.IsGenericType)
				{
					return (T)Convert.ChangeType(value, typeFromHandle.GetGenericArguments()[0]);
				}
				return (T)Convert.ChangeType(value, typeFromHandle);
			}
			catch (Exception ex)
			{
				return defaultValue;
			}
		}

		public static object TryParseValue(Type toType, object value)
		{
			try
			{
				if (ValueHelper.CheckValueIsDBNull(value))
				{
					return value;
				}
				Type type = value.GetType();
				if (type == toType)
				{
					return value;
				}
				if (toType.IsEnum)
				{
					return ValueHelper.GetEnum(toType, value);
				}
				if (value is byte[] && toType == typeof(long))
				{
					byte[] source = value as byte[];
					value = BitConverter.ToInt64(source.Reverse().ToArray(), 0);
					return value;
				}
				if (toType.IsGenericType)
				{
					return Convert.ChangeType(value, toType.GetGenericArguments()[0]);
				}
				return Convert.ChangeType(value, toType);
			}
			catch
			{
				return ValueHelper.Default(toType);
			}
		}

		private static long x2h(string value, int fromBase)
		{
			value = value.Trim();
			if (string.IsNullOrEmpty(value))
			{
				return 0L;
			}
			string text = new string(ValueHelper.rDigits, 0, fromBase);
			long num = 0L;
			value = value.ToUpper();
			for (int i = 0; i < value.Length; i++)
			{
				if (!text.Contains(value[i].ToString()))
				{
					throw new ArgumentException(string.Format("The argument \"{0}\" is not in {1} system.", value[i], fromBase));
				}
				try
				{
					num += (long)Math.Pow((double)fromBase, (double)i) * ValueHelper.getcharindex(ValueHelper.rDigits, value[value.Length - i - 1]);
				}
				catch
				{
					throw new OverflowException("运算溢出.");
				}
			}
			return num;
		}

		private static int getcharindex(char[] arr, char value)
		{
			for (int i = 0; i < arr.Length; i++)
			{
				if (arr[i] == value)
				{
					return i;
				}
			}
			return 0;
		}

		private static string h2x(long value, int toBase)
		{
			int num = 0;
			long num2 = Math.Abs(value);
			char[] array = new char[63];
			for (num = 0; num <= 64; num++)
			{
				if (num2 == 0)
				{
					break;
				}
				array[array.Length - num - 1] = ValueHelper.rDigits[num2 % toBase];
				num2 /= toBase;
			}
			return new string(array, array.Length - num, num);
		}

		public static string X2X(string value, int fromBase, int toBase)
		{
			string str = "";
			if (value.IndexOf("-") == 0)
			{
				str = "-";
				value = value.Substring(1);
			}
			if (string.IsNullOrEmpty(value.Trim()))
			{
				return string.Empty;
			}
			if (fromBase < 2 || fromBase > 36)
			{
				throw new ArgumentException(string.Format("The fromBase radix \"{0}\" is not in the range 2..36.", fromBase));
			}
			if (toBase < 2 || toBase > 36)
			{
				throw new ArgumentException(string.Format("The toBase radix \"{0}\" is not in the range 2..36.", toBase));
			}
			long value2 = ValueHelper.x2h(value, fromBase);
			string str2 = ValueHelper.h2x(value2, toBase);
			return str + str2;
		}

		public static T TryGetValue<T>(IDictionary<string, object> dictionary, string key, T defaultValue)
		{
			if (!dictionary.ContainsKey(key))
			{
				return defaultValue;
			}
			object value = dictionary[key];
			return ValueHelper.TryParseValue(value, defaultValue);
		}

		public static T GetEnum<T>(object obj)
		{
			if (obj is string)
			{
				return (T)Enum.Parse(typeof(T), obj.ToString(), true);
			}
			if (ValueHelper.CheckValueIsDBNull(obj))
			{
				return default(T);
			}
			if (!(obj is int))
			{
				obj = Convert.ToInt32(obj);
			}
			Type typeFromHandle = typeof(T);
			if (Enum.IsDefined(typeFromHandle, obj))
			{
				return (T)obj;
			}
			return default(T);
		}

		public static object GetEnum(Type toType, object obj)
		{
			if (obj is string)
			{
				return Enum.Parse(toType, obj.ToString(), true);
			}
			if (ValueHelper.CheckValueIsDBNull(obj))
			{
				return ValueHelper.Default(toType);
			}
			if (Enum.IsDefined(toType, obj))
			{
				return Enum.ToObject(toType, obj);
			}
			return ValueHelper.Default(toType);
		}

		public static object Default(Type toType)
		{
			return toType.IsValueType ? Activator.CreateInstance(toType) : null;
		}

		public static object ValueOrDBNull<T>(T? value) where T : struct, IConvertible
		{
			if (value.HasValue)
			{
				return value.Value;
			}
			return DBNull.Value;
		}

		public static string GetStringOrDefault(object value, string defaultValue)
		{
			if (ValueHelper.CheckValueIsDBNull(value))
			{
				return defaultValue;
			}
			return value.ToString();
		}

		public static T? TryGetNullable<T>(IDictionary<string, object> dictionary, string key) where T : struct, IConvertible
		{
			if (!dictionary.ContainsKey(key))
			{
				return null;
			}
			object obj = dictionary[key];
			if (obj is T)
			{
				return (T)obj;
			}
			Type typeFromHandle = typeof(T);
			if (ValueHelper.CheckValueIsDBNull(obj))
			{
				return null;
			}
			if (typeFromHandle.IsEnum)
			{
				return (T)Enum.Parse(typeFromHandle, obj.ToString(), true);
			}
			return (T)Convert.ChangeType(obj, typeFromHandle);
		}

		public static T? GetNullable<T>(IDictionary<string, object> dictionary, string key) where T : struct, IConvertible
		{
			if (!dictionary.ContainsKey(key))
			{
				return null;
			}
			Type typeFromHandle = typeof(T);
			object obj = dictionary[key];
			if (ValueHelper.CheckValueIsDBNull(obj))
			{
				return null;
			}
			return (T)obj;
		}

		public static object ValueOfIndex(IDictionary<string, object> dictionary, int index)
		{
			int num = 0;
			foreach (KeyValuePair<string, object> item in dictionary)
			{
				if (num == index)
				{
					return item.Value;
				}
				num++;
			}		
			return null;
		}

		public static bool CheckValueIsDBNull(object value)
		{
			return value == null || value.Equals(DBNull.Value);
		}

		public static object GetSqlParameter(object value)
		{
			return value;
		}

		public static string Convert10To36(long num)
		{
			string text = "";
			while (num > 0)
			{
				long value = num % "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length;
				text = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[Convert.ToInt32(value)].ToString() + text;
				num /= "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length;
			}
			return text;
		}

		public static long Convert36To10(string strNo)
		{
			long num = 0L;
			for (int i = 0; i < strNo.Length; i++)
			{
				num += "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(strNo[i]) * (int)Math.Pow((double)"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length, (double)(strNo.Length - i - 1));
			}
			return num;
		}

		public static List<T> ParseConvertList<T>(object obj)
		{
			if (obj == null)
			{
				return new List<T>();
			}

			if (obj is List<string> lString)
			{
				return lString.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is List<int> lInt)
			{
				return lInt.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is List<decimal> lDecimal)
			{
				return lDecimal.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is List<DateTime> lDateTime)
			{
				return lDateTime.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is List<object> lObject)
			{
				return lObject.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is List<long> lLong)
			{
				return lLong.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is List<bool> lBool)
			{
				return lBool.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is List<byte> lByte)
			{
				return lByte.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}

			if (obj is string[] aString)
			{
				return aString.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is int[] aInt)
			{
				return aInt.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is decimal[] aDecimal)
			{
				return aDecimal.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is DateTime[] aDateTime)
			{
				return aDateTime.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is object[] aObject)
			{
				return aObject.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is long[] aLong)
			{
				return aLong.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is bool[] aBool)
			{
				return aBool.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			if (obj is byte[] aByte)
			{
				return aByte.Select(o => ValueHelper.TryParseValue<T>(o)).ToList();
			}
			return null;
		}
	}
}
