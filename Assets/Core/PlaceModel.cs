using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace Weather
{
    public class PlaceModel
    {
        public string ID { set; get; }
        public string Name { set; get; }

        public override bool Equals(object obj)
        {
            PlaceModel pm = (PlaceModel)obj;
            if (this.ID == pm.ID && this.Name == pm.Name)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return (this.ID + this.Name).GetHashCode();
        }
    }
}