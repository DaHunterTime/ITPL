﻿using System;
using System.Collections.Generic;

using Tokens;
using Lexing;
using Parsing;
using Nodes;
using Interpreting;
using Types;

namespace InteractiveShell
{
    class Shell
    {
        static void Main()
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(CloseEvent);

            while(true)
            {
                Console.Write(">>> ");
                string text = Console.ReadLine();

                Node result = Run("<stdin>", text);
                Console.WriteLine(result);
            }
        }

        protected static void CloseEvent(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("\nClosing interactive shell...");
        }

        static Node Run(string fileName, string text)
        {
            var lexer = new Lexer(fileName, text);
            List<Token> tokens = lexer.MakeToken();

            if(tokens.Count == 1 && tokens[0].type == Token.TokenType.TokenError) return new ErrorNode(tokens[0]);

            var parser = new Parser(tokens);
            ParseResult syntaxTree = parser.Parse();

            if(syntaxTree.error) return syntaxTree.node;

            var interpreter = new Interpreter();
            var result = new ValueNode(interpreter.Visit(syntaxTree.node));

            return result;
        }
    }
}
