using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class Player
    {
        private int experience;
        public string Name { get; set; }
        public int Gold { get; set; }
        public int Lvl { get; set; }
        public int Experience
        {
            set
            {
                experience = value;
                if (value >= 100) 
                {
                    Lvl = (int)(Math.Log(value / 100, 2) + 1);//100(0)200(1)400(2) 2*
                }
                else
                {
                    Lvl = 0;
                }
                    
                
            }
            get
            {
                return experience;
            }
        }
        public int Record { get; set; }
    }
}
