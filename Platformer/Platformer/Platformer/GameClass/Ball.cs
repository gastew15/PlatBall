using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platformer.GameClass
{
    class Ball
    {
        public Ball()
        {

        }
        public Boolean isCollision(Platform obj)
        {
            //return platformRect.Intersects(obj.getRectangle());
            return false;
        }
    }
}
