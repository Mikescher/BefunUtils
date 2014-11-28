using System;
using System.IO;
using System.Text;

namespace BefunRep.FileHandling
{
	/// <summary>
	/// Binary Format
	/// 
	/// - erste 32 Byte Header (HEADER_SIZE)
	///	  -> 4 Byte codeLength
	///	  -> 8 Byte startValue
	///	  -> 8 Byte endValue
	/// 
	/// - Dann immer wieder Blöcke der Größe $codelength 
	///   -> In diesen stehen die Representations in ASCII
	///   -> jeweils der eihe nach von startVaue bis endValue
	/// 
	/// </summary>
	public class BinarySafe : RepresentationSafe
	{
		private const int INITIAL_CODE_LEN = 32; // Wird von Base9 erst bei über 4.782.969 gesprengt
		private readonly long INITIAL_VALUE_START;
		private readonly long INITIAL_VALUE_END;

		private const int HEADER_SIZE = 32;

		private readonly string filepath;
		private FileStream fstream;

		private int codeLength;

		private long valueStart;
		private long valueEnd;

		public BinarySafe(string path, long min, long max)
		{
			this.filepath = path;

			this.INITIAL_VALUE_START = min;
			this.INITIAL_VALUE_END = max;
		}

		public override string get(long key)
		{
			if (key < valueStart || key >= valueEnd)
				return null;

			fstream.Seek(HEADER_SIZE + codeLength * (key - valueStart), SeekOrigin.Begin);

			byte[] read = new byte[codeLength];
			fstream.Read(read, 0, codeLength);

			if (read[0] == 0)
				return null;

			StringBuilder b = new StringBuilder();
			for (int i = 0; i < codeLength; i++)
			{
				if (read[i] == 0)
					break;

				b.Append((char)read[i]);
			}

			return b.ToString();
		}

		public override void put(long key, string representation)
		{
			if (key >= valueEnd)
				updateEndSize(key);

			if (key < valueStart)
				updateStartSize(key);

			if (representation.Length > codeLength)
				updateCodeLength(representation.Length);

			fstream.Seek(HEADER_SIZE + codeLength * (key - valueStart), SeekOrigin.Begin);

			byte[] write = new byte[codeLength];

			for (int i = 0; i < codeLength; i++)
			{
				if (i < representation.Length)
					write[i] = (byte)representation[i];
				else
					write[i] = 0;
			}

			fstream.Write(write, 0, codeLength);
		}

		public override void start()
		{
			if (File.Exists(filepath))
			{
				fstream = new FileStream(filepath, FileMode.Open);

				fstream.Seek(0, SeekOrigin.Begin);
				byte[] arr = new byte[HEADER_SIZE];
				fstream.Read(arr, 0, HEADER_SIZE);
				codeLength = BitConverter.ToInt32(arr, 0);
				valueStart = BitConverter.ToInt64(arr, 4);
				valueEnd = BitConverter.ToInt64(arr, 12);

				if (INITIAL_VALUE_END > valueEnd)
					updateEndSize(INITIAL_VALUE_END, 0);
				if (INITIAL_VALUE_START < valueStart)
					updateStartSize(INITIAL_VALUE_START, 0);
			}
			else
			{
				fstream = new FileStream(filepath, FileMode.CreateNew);

				codeLength = INITIAL_CODE_LEN;
				valueStart = INITIAL_VALUE_START;
				valueEnd = INITIAL_VALUE_END;

				fstream.SetLength(HEADER_SIZE + (valueEnd - valueStart) * codeLength);

				writeHeader();
			}
		}

		public override void stop()
		{
			fstream.Close();
		}

		private void updateEndSize(long key, int buffer = 32) // 32 values buffer
		{
			long new_valueEnd = key + buffer;

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Update Safe Size Right (from {1} to {2})", DateTime.Now, valueEnd, new_valueEnd));

			valueEnd = new_valueEnd;
			fstream.SetLength(HEADER_SIZE + (valueEnd - valueStart) * codeLength);

			writeHeader();
		}

		private void updateStartSize(long key, int buffer = 1240) // 10 kB Buffer
		{
			long new_valueStart = key - buffer;

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Update Safe Size Left (from {1} to {2})", DateTime.Now, valueStart, new_valueStart));

			fstream.SetLength(HEADER_SIZE + (valueEnd - new_valueStart) * codeLength);
			moveRight(valueEnd - valueStart, valueStart - new_valueStart);
			valueStart = new_valueStart;

			writeHeader();
		}

		private void updateCodeLength(int len)
		{
			int new_codeLength = Math.Max(codeLength + (len - codeLength) * 2, codeLength * 2);

			Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] Update Safe Code Length (from {1} to {2})", DateTime.Now, codeLength, new_codeLength));

			fstream.SetLength(HEADER_SIZE + (valueEnd - valueStart) * new_codeLength);

			byte[] empty = new byte[codeLength];
			empty.Fill<byte>(0);

			byte[] arr = new byte[codeLength];

			for (long i = (valueEnd - valueStart) - 1; i >= valueStart; i--)
			{
				fstream.Seek(HEADER_SIZE + i * codeLength, SeekOrigin.Begin);
				fstream.Read(arr, 0, codeLength);

				fstream.Seek(HEADER_SIZE + i * codeLength, SeekOrigin.Begin);
				fstream.Write(empty, 0, codeLength);

				fstream.Seek(HEADER_SIZE + i * new_codeLength, SeekOrigin.Begin);
				fstream.Write(arr, 0, codeLength);
			}

			codeLength = new_codeLength;

			writeHeader();
		}

		private void writeHeader()
		{
			byte[] arr = new byte[HEADER_SIZE];
			arr.Fill<byte>(0);

			Array.Copy(BitConverter.GetBytes(codeLength), 0, arr, 0, 4);
			Array.Copy(BitConverter.GetBytes(valueStart), 0, arr, 4, 8);
			Array.Copy(BitConverter.GetBytes(valueEnd), 0, arr, 12, 8);

			fstream.Seek(0, SeekOrigin.Begin);
			fstream.Write(arr, 0, 4 + 8 + 8);
		}

		private void moveRight(long count, long offset)
		{
			byte[] empty = new byte[codeLength];
			empty.Fill<byte>(0);

			byte[] arr = new byte[codeLength];

			for (long i = count - 1; i >= 0; i--)
			{
				fstream.Seek(HEADER_SIZE + i * codeLength, SeekOrigin.Begin);
				fstream.Read(arr, 0, codeLength);

				fstream.Seek(HEADER_SIZE + i * codeLength, SeekOrigin.Begin);
				fstream.Write(empty, 0, codeLength);

				fstream.Seek(HEADER_SIZE + (i + offset) * codeLength, SeekOrigin.Begin);
				fstream.Write(arr, 0, codeLength);
			}
		}

		public override long getLowestValue()
		{
			return valueStart;
		}

		public override long getHighestValue()
		{
			return valueEnd;
		}

	}
}
