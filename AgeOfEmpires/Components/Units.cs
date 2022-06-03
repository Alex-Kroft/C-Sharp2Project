using MonoGame.Extended.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeOfEmpires.Components
{
    class Units
    {
        HashSet<Entity> units = new HashSet<Entity>();

        public Units() { }

        public void addUnitToSet(Entity entity) {
            units.Add(entity);
        }

        public void removeUnitFromSet(Entity entity) {
            units.Remove(entity);
        }
    }
}
