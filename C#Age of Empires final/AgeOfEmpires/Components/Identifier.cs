using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class Identifier
    {
        private String _identifier;

        public Identifier(String identity)
        {
            this._identifier = identity;
        }

        public void setIdentity(string indetity)
        {
            this._identifier = indetity;
        }

        public String getIdentity()
        {
            return this._identifier;
        }
    }
}
