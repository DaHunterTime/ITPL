using Tokens;
using Types;
using Positions;

namespace Nodes
{
    public class Node
    {
        public Token token;
        public Position start;
        public Position end;

        public Node(Token token)
        {
            this.token = token;
            this.start = token.start;
            this.end = token.end;
        }

        public override string ToString()
        {
            return this.token.ToString();
        }
    }
    
    public class NumberNode : Node
    {
        public NumberNode(Token token) : base(token)
        {
            
        }
    }

    public class BinOpNode : Node
    {
        public Node leftNode;
        public Node rightNode;

        public BinOpNode(Token opToken, Node left, Node right) : base(opToken)
        {
            this.leftNode = left;
            this.rightNode = right;
            this.start = left.start;
            this.end = right.end;
        }

        public override string ToString()
        {
            return $"({this.leftNode}, {this.token}, {this.rightNode})";
        }
    }

    public class UnaryOpNode : Node
    {
        public Node node;

        public UnaryOpNode(Token opToken, Node node) : base(opToken)
        {
            this.node = node;
            this.end = node.end;
        }

        public override string ToString()
        {
            return $"({this.token}, {this.node})";
        }
    }

    public class ErrorNode : Node
    {
        public ErrorNode(Token errorToken) : base(errorToken)
        {

        }

        public override string ToString()
        {
            return this.token.value.ToString();
        }
    }

    public class ValueNode : Node
    {
        public ObjectBase value;

        public ValueNode(ObjectBase value) : base(new Token(Token.TokenType.TokenValue, value.start, value.end))
        {
            this.value = value;
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
