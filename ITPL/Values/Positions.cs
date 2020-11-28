namespace Positions
{
    public class Position
    {
        public int index;
        public int line;
        public int column;
        public string fileName;
        public string fileText;

        public Position(int index, int line, int column, string fileName, string fileText)
        {
            this.index = index;
            this.line = line;
            this.column = column;
            this.fileName = fileName;
            this.fileText = fileText;
        }

        public Position Advance(char? currentChar)
        {
            this.index++;
            this.column++;

            if(currentChar == '\n')
            {
                this.line++;
                this.column = 0;
            }

            return this;
        }

        public Position Copy()
        {
            return new Position(this.index, this.line, this.column, this.fileName, this.fileText);
        }
    }
}
