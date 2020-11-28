using System;

using Positions;

namespace Utilities
{
    public static class Constant
    {
        public const string Digits = "1234567890";
    }

    public static class Function
    {
        public static string ArrowToError(string text, Position start, Position end)
        {
            string result = "";
            int indexStart = Math.Max(text.LastIndexOf("\n", start.index), 0);
            int indexEnd = text.IndexOf("\n", indexStart + 1);
            indexEnd = indexEnd < 0 ? text.Length : indexEnd;
            int lineCount = end.line - start.line + 1;
            string line;
            int columnStart, columnEnd;

            for(int i = 0; i < lineCount; i++)
            {
                line = text.Substring(indexStart, indexEnd - indexStart);
                columnStart = i == 0 ? start.column : 0;
                columnEnd = i == lineCount - 1 ? end.column : line.Length - 1;
                result += $"{line}\n";

                for(int j = 0; j < columnStart; j++)
                {
                    result += " ";
                }

                for(int k = 0; k < columnEnd - columnStart; k++)
                {
                    result += "^";
                }

                indexStart = indexEnd;
                indexEnd = indexStart < text.Length ? text.IndexOf("\n", indexStart + 1) : text.Length;
            }

            return result.Replace("\t", "");
        }
    }
}
