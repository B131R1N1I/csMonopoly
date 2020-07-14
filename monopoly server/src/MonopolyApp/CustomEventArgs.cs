using System;

namespace MonopolyApp
{
    public class TypeEventArgs : EventArgs
    {
        public TypeEventArgs(ActionJsonObject actionJson) : base()
        {
            this.actionJson = actionJson;
        }
        public readonly ActionJsonObject actionJson;
    }
}