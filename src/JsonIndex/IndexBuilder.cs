using System;
using System.Globalization;

namespace JsonIndex
{
    internal class IndexBuilder
    {
        private readonly Index index;
        private readonly string data;
        private int position;

        public IndexBuilder(string data, IndexSettings settings)
        {
            this.index = new Index(data, settings);
            this.data = data;
            this.position = 0;
        }

        public Index Build()
        {
            SkipByteOrderMark();
            SkipWhiteCharacters();
            ParseObjectOrArray(-1);

            return this.index;
        }

        private void ParseObjectOrArray(int parent)
        {
            if (position < data.Length)
            {
                switch (data[position])
                {
                    case '{':
                        ParseObject(parent);
                        break;

                    case '[':
                        ParseArray(parent);
                        break;

                    default:
                        throw new IndexException(String.Format("The parser required object or array, but found unknown character. position={0}; character={1}", position, data[position]));
                }
            }
        }

        private void ParseObject(int parent)
        {
            int entry = index.New(IndexType.Object, parent, position, -1);

            Require('{');
            SkipWhiteCharacters();

            while (position < data.Length && data[position] != '}')
            {
                ParseProperty(entry);
                SkipWhiteCharacters();
                Skip(',');
                SkipWhiteCharacters();
            }

            if (position >= data.Length)
            {
                throw new IndexException("The parser required '}', but found end of data.");
            }

            if (position < data.Length)
            {
                Require('}');
                index.End(entry, position - 1);
            }
        }

        private void ParseProperty(int parent)
        {
            ParsePropertyName(parent);
            SkipWhiteCharacters();
            Require(':');
            SkipWhiteCharacters();
            ParsePropertyValue(parent);
        }

        private void ParsePropertyName(int parent)
        {
            ParseText(parent, IndexType.Property);
        }

        private void ParsePropertyValue(int parent)
        {
            ParseValue(parent);
        }

        private void ParseArray(int parent)
        {
            int entry = index.New(IndexType.Array, parent, position, -1);

            Require('[');
            SkipWhiteCharacters();

            while (position < data.Length && data[position] != ']')
            {
                ParseArrayItem(entry);
                SkipWhiteCharacters();
                Skip(',');
                SkipWhiteCharacters();
            }

            if (position >= data.Length)
            {
                throw new IndexException("The parser required ']', but found end of data.");
            }

            if (position < data.Length)
            {
                Require(']');
                index.End(entry, position - 1);
            }
        }

        private void ParseArrayItem(int parent)
        {
            ParseValue(parent);
        }

        private void ParseValue(int parent)
        {
            if (position < data.Length)
            {
                switch (data[position])
                {
                    case '{':
                        ParseObject(parent);
                        break;

                    case '[':
                        ParseArray(parent);
                        break;

                    case '"':
                        ParseText(parent);
                        break;

                    case 't':
                        ParseTrue(parent);
                        break;

                    case 'f':
                        ParseFalse(parent);
                        break;

                    case 'n':
                        ParseNull(parent);
                        break;

                    case '-':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        ParseNumber(parent);
                        break;

                    default:
                        throw new IndexException(String.Format("The parser required value, but found unknown character. position={0}; character={1}", position, data[position]));
                }
            }
        }

        private void ParseText(int parent, byte type = IndexType.Text)
        {
            int start = position + 1;
            Require('"');

            while (position < data.Length && data[position] != '"')
            {
                if (data[position] == '\\')
                {
                    position++;
                }

                position++;
            }

            if (position >= data.Length)
            {
                throw new IndexException("The parser required '\"', but found end of data.");
            }

            if (position < data.Length)
            {
                Require('"');
                Define(type, parent, start, position - 2);
            }
        }

        private void ParseNumber(int parent)
        {
            int start = position;

            while (position < data.Length)
            {
                switch (data[position])
                {
                    case '+':
                    case '-':
                    case '.':
                    case 'e':
                    case 'E':
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        position++;
                        break;

                    default:

                        if (this.index.Settings.IndexNumber == true)
                        {
                            Define(IndexType.Number, parent, start, position - 1);
                        }

                        return;
                }
            }
        }

        private void ParseTrue(int parent)
        {
            RequireSequence("true");

            if (this.index.Settings.IndexTrue == true)
            {
                Define(IndexType.True, parent, position - 4, position - 1);
            }
        }

        private void ParseFalse(int parent)
        {
            RequireSequence("false");

            if (this.index.Settings.IndexFalse == true)
            {
                Define(IndexType.False, parent, position - 5, position - 1);
            }
        }

        private void ParseNull(int parent)
        {
            RequireSequence("null");

            if (this.index.Settings.IndexTrue == true)
            {
                Define(IndexType.Null, parent, position - 4, position - 1);
            }
        }

        private void Define(byte type, int parent, int start, int end)
        {
            if (position < data.Length)
            {
                index.New(type, parent, start, end);
            }
        }

        private void Require(char character)
        {
            if (position < data.Length)
            {
                if (data[position] != character)
                {
                    throw new IndexException(String.Format("The parser required character {0}. position={1}; character={2}", character, position, data[position]));
                }

                position++;
            }
        }

        private void RequireSequence(string characters)
        {
            foreach (char character in characters)
            {
                if (position < data.Length && data[position] != character)
                {
                    throw new IndexException(String.Format("The parser required sequence '{0}'. position={1}; character={2}", characters, position, data[position]));
                }

                position++;
            }
        }

        private void Skip(char character)
        {
            if (position < data.Length && data[position] == character)
            {
                position++;
            }
        }

        private void SkipWhiteCharacters()
        {
            while (position < data.Length && Char.IsWhiteSpace(data[position]) == true)
            {
                position++;
            }
        }

        private void SkipByteOrderMark()
        {
            while (position < data.Length && Char.GetUnicodeCategory(data[position]) == UnicodeCategory.Format)
            {
                position++;
            }
        }
    }
}