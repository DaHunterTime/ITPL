using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Tokens;
using Nodes;

namespace Parsing
{
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

        public Node Parse()
        {
            Node result = this.Expr();
            return result;
        }

        public Node Factor()
        {
            Token token = this.currentToken;
            Token.TokenType[] numberTypes = {Token.TokenType.TokenInt, Token.TokenType.TokenDecimal};

            if(numberTypes.Contains(this.currentToken.type))
            {
                this.Advance();
                return new NumberNode(token);
            }

            return null;
        }

        public Node Term()
        {
            return this.BinaryOp("Factor", new Token.TokenType[] {Token.TokenType.TokenMul, Token.TokenType.TokenDiv});
        }

        public Node Expr()
        {
            return this.BinaryOp("Factor", new Token.TokenType[] {Token.TokenType.TokenPlus, Token.TokenType.TokenMinus});
        }

        public Node BinaryOp(string methodName, Token.TokenType[] ops)
        {
            MethodInfo method = this.GetType().GetMethod(methodName);
            Node left = (Node)method.Invoke(this, null);
            Token opToken;
            Node right;

            while(ops.Contains(this.currentToken.type))
            {
                opToken = this.currentToken;
                this.Advance();
                right = (Node)method.Invoke(this, null);
                left = new BinOpNode(opToken, left, right);
            }

            return left;
        }
    }
}
