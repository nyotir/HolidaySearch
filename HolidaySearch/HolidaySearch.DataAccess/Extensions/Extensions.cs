using System.Reflection;

namespace HolidaySearch.DataAccess.Extensions
{
    public static class Extensions
    {
        public static string? GetEmbeddedJson(string resourceName)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                $"HolidaySearch.DataAccess.Data.{resourceName}");
            if (stream != null)
            {
                TextReader tr = new StreamReader(stream);
                return tr.ReadToEnd();
            }

            return null;
        }
    }
}
