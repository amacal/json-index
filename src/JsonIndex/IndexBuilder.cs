using System;

namespace JsonIndex
{
    public class IndexBuilder
    {
        private readonly Index index;
        private readonly string data;
        private int position;

        public IndexBuilder(string data)
        {
            this.index = new Index();
            this.data = data;
            this.position = 0;
        }

        public Index Build()
        {
            SkipWhiteCharacters();
            ParseObject(-1);

            return this.index;
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
            ParseText(parent);
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
                        return;
                }
            }
        }

        private void ParseText(int parent)
        {
            int start = position + 1;
            Require('"');

            while (position < data.Length && data[position] != '"')
            {
                position++;
            }

            if (position < data.Length)
            {
                Require('"');
                Define(IndexType.Text, parent, start, position - 2);
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
                        Define(IndexType.Number, parent, start, position - 1);
                        return;
                }
            }
        }

        private void ParseTrue(int parent)
        {
            RequireSequence("true");
            Define(IndexType.True, parent, position - 4, position - 1);
        }

        private void ParseFalse(int parent)
        {
            RequireSequence("false");
            Define(IndexType.False, parent, position - 5, position - 1);
        }

        private void ParseNull(int parent)
        {
            RequireSequence("null");
            Define(IndexType.Null, parent, position - 4, position - 1);
        }

        private void Define(byte type, int parent, int start, int end)
        {
            if (position < data.Length)
            {
                index.New(type, parent, position - 4, position - 1);
            }
        }

        private void Require(char character)
        {
            if (position < data.Length)
            {
                position = data[position] != character ? data.Length : position + 1;
            }
        }

        private void RequireSequence(string characters)
        {
            foreach (char character in characters)
            {
                if (position < data.Length && data[position] != character)
                {
                    position = data.Length;
                }
            }
        }

        private void Skip(char character)
        {
            if (position < data.Length && position == character)
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
    }
}
