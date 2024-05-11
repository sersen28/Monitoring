namespace SerialModule.Utility
{
	public class SerialUtility
	{
		public static string ByteArrayToString(byte[] buffer, bool displayHex)
		{
			var result = string.Empty;
			foreach (var b in buffer)
			{
				if (displayHex)
					result += "0x";
				result += b.ToString("x2") + " ";
			}
			return result;
		}
	}
}
