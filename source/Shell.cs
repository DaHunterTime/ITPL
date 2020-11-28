using System;
using System.Collections.Generic;

using Tokens;
using Lexing;

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

                List<Token> result = Run("<stdin>", text);

                if(result.Count == 1 && result[0].type == Token.TokenType.TokenError) Console.WriteLine(result[0].value);
                else Console.WriteLine("[" + String.Join<Token>(", ", result.ToArray()) + "]");
            }
        }

        protected static void CloseEvent(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("\nClosing interactive shell...");
        }

        static List<Token> Run(string fileName, string text)
        {
            var lexer = new Lexer(fileName, text);
            return lexer.MakeToken();
        }
    }
}
