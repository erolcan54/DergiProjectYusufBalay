using System.Text;

namespace UI.Utilities
{
    public static class ByteToString
    {
        public static string DecodeString(byte[] bytes)
        {
            String my_string = Encoding.Unicode.GetString(bytes, 0, bytes.Length);
            return my_string;
        }
    }
}
