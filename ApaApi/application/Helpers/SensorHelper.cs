using System.Linq;

namespace application.Helpers
{
    public static class SensorHelper
    {
        public static string IdFromBytes(byte[] bytes)
        {
            var id = new string(bytes.Take(8).Select(b => (char)b).ToArray());
            return id;
        }
    }
}
