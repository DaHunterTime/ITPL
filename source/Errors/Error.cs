using Positions;

namespace Errors
{
    public class Error
    {
        public string name;
        public string details;
        public Position start;
        public Position end;

        public Error(string name, string details, Position start, Position end)
        {
            this.name = name;
            this.details = details;
            this.start = start;
            this.end = end;
        }

        public override string ToString()
        {
            string result = "Traceback [ITPL]:";
            result += $"\n  File '{this.start.fileName}', line {this.start.line + 1}";
            result += $"\n{this.name}: {this.details}";
            return result;
        }
    }

    public class UnknownCharError : Error
    {
        public UnknownCharError(string details, Position start, Position end): base("UnkonwnCharError", details, start, end)
        {
            this.details = $"unknown character '{this.details}' found";
        }
    }
}
