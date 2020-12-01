using System;

using Positions;

namespace Tokens
{
    public class Token
    {
        public enum TokenType
        {
            TokenInt,
            TokenDecimal,
            TokenPlus,
            TokenMinus,
            TokenMul,
            TokenDiv,
            TokenLPar,
            TokenRPar,
            TokenError,
            TokenEOF,
            TokenValue
        }

        public dynamic value;
        public TokenType type;
        public Position start;
        public Position end;

        public Token(TokenType type, dynamic value, Position start = null, Position end = null)
        {
            this.value = value;
            this.type = type;
            if(start != null) this.start = start.Copy();
            if(end != null) this.end = end;
            else
            {
                this.end = this.start.Copy();
                this.end.Advance(null);
            }
        }

        public Token(TokenType type, Position start = null, Position end = null)
        {
            this.type = type;
            if(start != null) this.start = start.Copy();
            if(end != null) this.end = end;
            else
            {
                this.end = this.start.Copy();
                this.end.Advance(null);
            }
        }

        public override string ToString()
        {
            return value != null ? $"{Enum.GetName(typeof(TokenType), this.type)}:{this.value}" : Enum.GetName(typeof(TokenType), this.type);
        }
    }
}
