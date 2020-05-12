using System.IO;
using System.Threading.Tasks;

namespace Walmart.Sdk.Base.Util
{
	//TODO Remove, used only for testing
	class LogToFile
	{
		internal static async Task WriteLogString(string correlationId, string content, string fileType, string ext)
		{
			try
			{
				using (StreamWriter sw = File.AppendText($"C:\\Temp\\Walmart\\{correlationId}-{fileType}.{ext}"))
				{
					await sw.WriteLineAsync(content);
				}
			}
			catch { }
		}

	}
}
