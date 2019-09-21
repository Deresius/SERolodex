using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceEngineersRolodex.Model
{
    class CoordinatesRolodex
    {
        public List<Coordinates> coordinateCollection;

        public CoordinatesRolodex()
        {
            this.coordinateCollection = new List<Coordinates>();
        }

        public void AddCoordinates(Coordinates toAdd)
        {
            coordinateCollection.Add(toAdd);
        }
    }
}
