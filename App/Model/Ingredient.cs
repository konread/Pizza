using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ingredient
    {
        public int Id_Ingredient { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
