using Tokens;

namespace Nodes
{
    public class Node
    {
        public Token token;

        public Node(Token token)
        {
            this.token = token;
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
}
