﻿namespace JeremyAnsel.Xwa.Workspace
{
    public sealed class XwaFlightModelListEntry
    {
        public const int ValueMaxLength = 255;

        private string _value = string.Empty;

        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value[..Math.Min(value.Length, ValueMaxLength)];
            }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
