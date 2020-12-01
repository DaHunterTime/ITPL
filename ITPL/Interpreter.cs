using System;
using System.Reflection;

using Nodes;
using Types;
using Tokens;

namespace Interpreting
{
    public class Interpreter
    {
        public ObjectBase Visit(Node node)
        {   
            string typeName = node.GetType().Name;
            MethodInfo method = this.GetType().GetMethod("Visit" + typeName);
            ObjectBase value = null;

            if(method != null)
            {
                value = (ObjectBase)method.Invoke(this, new object[] {node});
            }
            else
            {
                Console.WriteLine($"No visit method found for {node}");
                Environment.Exit(0);
            }

            return value;
        }

        public ObjectBase VisitNumberNode(NumberNode node)
        {
            if(node.token.value.GetType() == typeof(Int32))
            {
                var intNumber = new Integer(node.token.value);
                intNumber.SetPosition(node.start, node.end);
                return intNumber;
            }

            var number = new DecimalValue(node.token.value);
            number.SetPosition(node.start, node.end);
            return number;
        }

        public ObjectBase VisitBinOpNode(BinOpNode node)
        {
            var left = this.Visit(node.leftNode);
            var right = this.Visit(node.rightNode);
            ObjectBase result;

            switch(node.token.type)
            {
                case Token.TokenType.TokenPlus:
                    result = left.AddTo(right);
                    break;
                case Token.TokenType.TokenMinus:
                    result = left.SubBy(right);
                    break;
                case Token.TokenType.TokenMul:
                    result = left.MulBy(right);
                    break;
                case Token.TokenType.TokenDiv:
                    result = left.DivBy(right);
                    break;
                default:
                    result = null;
                    break;
            }

            result.SetPosition(node.start, node.end);
            return result;
        }

        public ObjectBase VisitUnaryOpNode(UnaryOpNode node)
        {
            var number = this.Visit(node.node);

            if(node.token.type == Token.TokenType.TokenMinus)
            {
                number = number.MulBy(new Integer(-1));
            }

            number.SetPosition(node.start, node.end);
            return number;
        }
    }
}