﻿using System;

namespace Kingsland.MofParser.Lexing
{

    public class StringLexerStream : LexerStreamBase
    {

        public StringLexerStream(string source)
        {
            this.Source = source;
            this.Position = 0;
            this.LineNumber = 0;
            this.Column = 0;
        }

        #region Properties

        private string Source
        {
            get;
            set;
        }

        /// <summary>
        /// Used for tracking line breaks
        /// </summary>
        private char LastChar
        { 
            get; 
            set;
        }

        public override int Length
        {
            get
            {
                return (this.Source == null) ? 0 : this.Source.Length;
            }
        }

        #endregion

        #region Peek Methods

        /// <summary>
        /// Reads the next character off of the input stream, but does not advance the current position.
        /// </summary>
        /// <returns></returns>
        public override char Peek()
        {
            if (this.Eof)
            {
                throw new InvalidOperationException("Unexpected end of file encountered.");
            }
            return this.Source[this.Position];
        }

        #endregion

    }

}
