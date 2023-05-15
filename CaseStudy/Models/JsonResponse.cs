using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Models
{
    public class JsonResponse
    {
        public string locale { get; set; }
        public string description { get; set; }
        public BoundingPoly boundingPoly { get; set; }
        public float centerPoint { get; set; }
        public float area_Y0 { get; set; }
        public float area_Y4 { get; set; }

    }
}
