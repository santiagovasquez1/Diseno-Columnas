using System;

namespace DisenoColumnas.Clases
{
    [Serializable]
    public class MAT_CONCRETE
    {
        public string Name { get; set; }
        public float FC { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is MAT_CONCRETE)
            {
                MAT_CONCRETE temp = (MAT_CONCRETE)obj;
                if (FC == temp.FC)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool operator ==(MAT_CONCRETE s1, MAT_CONCRETE s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator !=(MAT_CONCRETE s1, MAT_CONCRETE s2)
        {
            return !s1.Equals(s2);
        }
    }
}