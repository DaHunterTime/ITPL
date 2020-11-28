using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Tokens;
using Nodes;
using Errors;

namespace Parsing
{
    public class ParseResult
    {
        public bool error = false;
        public Node node;

        public Token Register(Token token)
        {
            return token;
        }
        
        public Node Register(Node result)
        {
            return result;
        }

        public Node Register(ParseResult result)
        {
            this.error = result.error ? true : false;
            this.node = result.node;
            return this.node;
        }

        public ParseResult Success(Node node)
        {
            this.node = node;
            return this;
        }

        public ParseResult Failure(Node error)
        {
            this.error = true;
            this.node = error;
            return this;
        }
    }
    
    public class Parser
    {
        public List<Token> tokens;
        public int tokenIndex = -1;
        public Token currentToken;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            this.Advance();
        }

        public Token Advance()
        {
            this.tokenIndex++;

            if(this.tokenIndex < this.tokens.Count)
            {
                this.currentToken = this.tokens[this.tokenIndex];
            }

            return this.currentToken;
        }

        public ParseResult Parse()
        {
            ParseResult result = this.Expr();

            if(!result.error && this.currentToken.type != Token.TokenType.TokenEOF)
            {
                var error = new SyntaxError("invalid syntax", this.currentToken.start, this.currentToken.end);
                var errorToken = new Token(Token.TokenType.TokenError, error, error.start, error.end);
                return result.Failure(new ErrorNode(errorToken));
            }

            return result;
        }

        public ParseResult Factor()
        {
            var result = new ParseResult();
            Token token = this.currentToken;
            Token.TokenType[] numberTypes = {Token.TokenType.TokenInt, Token.TokenType.TokenDecimal};

            if(numberTypes.Contains(this.currentToken.type))
            {
                result.Register(this.Advance());
                return result.Success(new NumberNode(token));
            }

            var error = new SyntaxError("expected a number", token.start, token.end);
            var errorToken = new Token(Token.TokenType.TokenError, error, error.start, error.end);
            return result.Failure(new ErrorNode(errorToken));
        }

        public ParseResult Term()
        {
            return this.BinaryOp("Factor", new Token.TokenType[] {Token.TokenType.TokenMul, Token.TokenType.TokenDiv});
        }

        public ParseResult Expr()
        {
            return this.BinaryOp("Term", new Token.TokenType[] {Token.TokenType.TokenPlus, Token.TokenType.TokenMinus});
        }

        public ParseResult BinaryOp(string methodName, Token.TokenType[] ops)
        {
            MethodInfo method = this.GetType().GetMethod(methodName);
            var result = new ParseResult();
            Node left = result.Register((ParseResult)method.Invoke(this, null));
            if(result.error) return result;
            Token opToken;
            Node right;

            while(ops.Contains(this.currentToken.type))
            {
                opToken = this.currentToken;
                result.Register(this.Advance());
                right = result.Register((ParseResult)method.Invoke(this, null));
                if(result.error) return result;
                left = new BinOpNode(opToken, left, right);
            }

            return result.Success(left);
        }
    }
}
