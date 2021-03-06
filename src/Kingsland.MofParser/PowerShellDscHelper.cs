﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Objects;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser
{

    public static class PowerShellDscHelper
    {

        public static List<Instance> ParseMofFileInstances(string filename)
        {

            // read the text from the mof file
            var sourceText = File.ReadAllText(filename);

            // turn the text into a stream of characters for lexing
            var stream = new StringLexerStream(sourceText);

            // lex the characters into a sequence of tokens
            var tokens = Lexer.Lex(stream);

            // parse the tokens into an ast tree
            var ast = Parser.Parse(tokens);

            // scan the ast for any "instance" definitions and convert them
            var instances = ((MofSpecificationAst)ast).Productions
                                                      .Where(p => (p is ComplexValueAst))
                                                      .Cast<ComplexValueAst>()
                                                      .Where(ctv => ctv.IsInstance)
                                                      .Select(Instance.FromAstNode)
                                                      .ToList();
            
            // return the result
            return instances.ToList();

        }

    }

}
