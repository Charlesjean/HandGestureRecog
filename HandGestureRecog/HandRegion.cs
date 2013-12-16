using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace HandGestureRecog
{
    class HandRegion : IComparable
    {
        
        public Rectangle rect;
        public Rectangle innerRect;
        double colorEnergy;
        double motionEnergy;
       public HandRegion(Rectangle _rect, Rectangle _innerRect, double _cenergy, double _menergy)
        {
            rect = _rect;
            colorEnergy = _cenergy;
            motionEnergy = _menergy;
            innerRect = _innerRect;

        }
        public void Copy(HandRegion region)
       {
           this.rect = region.rect;
           this.innerRect = region.innerRect;
           this.colorEnergy = region.colorEnergy;
           this.motionEnergy = region.motionEnergy;
       }

        public double GetEnergy()
        {
            return this.colorEnergy + this.motionEnergy;
        }
        public int CompareTo(object obj)
       {
            if(obj is HandRegion)
            {
                HandRegion region = obj as HandRegion;
                return (this.colorEnergy + this.motionEnergy).CompareTo(region.colorEnergy + region.motionEnergy);
            }
            throw new NotImplementedException("obj is not a Student!");
       }
    }
}
