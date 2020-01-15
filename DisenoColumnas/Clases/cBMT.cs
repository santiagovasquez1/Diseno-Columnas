using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace DisenoColumnas.Clases
{
    [Serializable]
    public enum eBMT
    {
        Bottom,Medium,Top
    }
    [Serializable]
    public class cBMT    
    {      
        public cBMT(eBMT Tipo_,string story)
        {
            Tipo = Tipo_;
            Story = story;
        }
        public eBMT Tipo { get; set; }

        public List<PointF> Coord { get; set; }
        public string Story { get; set; }

        public override string ToString()
        {
            return Tipo.ToString() + "-" + Story;
        }
    }
}
