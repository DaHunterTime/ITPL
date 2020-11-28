using System;
using System.Collections.Generic;

using Tokens;
using Utilities;
using Errors;
using Positions;

namespace Lexing
{
    public class Lexer
    {
        public string fileName;
        public string text;
        public Position pos;
        public char? currentChar;

        public Lexer(string fileName, string text)
        {
            this.fileName = fileName;
            this.text = text;
            this.pos = new Position(-1, 0, -1, fileName, text);
            this.Advance();
        }

        public void Advance()
        {
            this.pos.Advance(this.currentChar);
            this.currentChar = this.pos.index < this.text.Length ? this.text[this.pos.index] : null;
        }

        public List<Token> MakeToken()
        {
            List<Token> tokens = new List<Token>();

            while(this.currentChar != null)
            {
                switch(this.currentChar)
                {
                    case '+':
                        tokens.Add(new Token(Token.TokenType.TokenPlus, start:this.pos));
                        this.Advance();
                        break;
                    case '-':
                        tokens.Add(new Token(Token.TokenType.TokenMinus, start:this.pos));
                        this.Advance();
                        break;
                    case '*':
                        tokens.Add(new Token(Token.TokenType.TokenMul, start:this.pos));
                        this.Advance();
                        break;
                    case '/':
                        tokens.Add(new Token(Token.TokenType.TokenDiv, start:this.pos));
                        this.Advance();
                        break;
                    case '(':
                        tokens.Add(new Token(Token.TokenType.TokenLPar, start:this.pos));
                        this.Advance();
                        break;
                    case ')':
                        tokens.Add(new Token(Token.TokenType.TokenRPar, start:this.pos));
                        this.Advance();
                        break;
                    default:
                        if(" \t".Contains(Convert.ToChar(this.currentChar)))
                        {
                            this.Advance();
                        }
                        else if(Constant.Digits.Contains(Convert.ToChar(this.currentChar)))
                        {
                            tokens.Add(this.MakeNumber());
                        }
                        else
                        {
                            Position start = this.pos.Copy();
                            string previousChar = Convert.ToString(this.currentChar);
                            this.Advance();
                            return new List<Token> {new Token(Token.TokenType.TokenError, new UnknownCharError(previousChar, start, this.pos))};
                        }

                        break;
                }
            }

            tokens.Add(new Token(Token.TokenType.TokenEOF, start:this.pos));
            return tokens;
        }

        public Token MakeNumber()
        {
            string numberStr = "";
            int dotCount = 0;
            Position start = this.pos.Copy();

            while(this.currentChar != null && (Constant.Digits + '.').Contains(Convert.ToChar(this.currentChar)))
            {
                if(this.currentChar == '.')
                {
                    if(dotCount == 1) break;
                    dotCount++;
                    numberStr += '.';
                }
                else
                {
                    numberStr += this.currentChar;
                }

                this.Advance();
            }

            if(dotCount == 0) return new Token(Token.TokenType.TokenInt, Int32.Parse(numberStr), start, this.pos);
            else return new Token(Token.TokenType.TokenDecimal, Double.Parse(numberStr), start, this.pos);
        }
    }
}