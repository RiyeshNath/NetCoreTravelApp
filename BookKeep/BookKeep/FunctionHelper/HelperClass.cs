using System;
namespace BookKeep.FunctionHelper
{
    public class HelperClass
    {
        public string TranslateAccent(string accentedStr)
        {
            byte[] tempBytes= System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            return asciiStr;
        }
    }
}
