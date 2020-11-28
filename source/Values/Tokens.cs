using System;

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
            TokenError
        }

        public dynamic value;
        public TokenType type;

        public Token(TokenType type, dynamic value)
        {
            this.value = value;
            this.type = type;
        }

        public Token(TokenType type)
        {
            this.type = type;
        }

        public override string ToString()
        {
            return value != null ? $"{Enum.GetName(typeof(TokenType), this.type)}:{this.value}" : Enum.GetName(typeof(TokenType), this.type);
        }
    }
}
