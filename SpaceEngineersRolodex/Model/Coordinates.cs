using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sms;

namespace SpaceEngineersRolodex.Model
{
    class Coordinates
    {
        public string Name;
        public string FactionTag;
        //public string FactionName;
        public string Description;

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }


        public Coordinates(string name, float x, float y, float z)
        {
            this.Name = name;
            this.X = x;
            this.Y = y;
            this.Z = z;

        }

        public Coordinates(string name, string tag, float x, float y, float z)
        {
            this.Name = name;
            this.FactionTag = tag;
            this.X = x;
            this.Y = y;
            this.Z = z;

        }

        public Coordinates(string name, string tag, string description, float x, float y, float z)
        {
            this.Name = name;
            this.FactionTag = tag;
            this.Description = description;
            this.X = x;
            this.Y = y;
            this.Z = z;

        }




    }
}
