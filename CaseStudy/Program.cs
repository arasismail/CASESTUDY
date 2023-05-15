using CaseStudy.Models;
using Newtonsoft.Json;

namespace CaseStudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("..\\..\\..\\response.json"))
            {
                string json = sr.ReadToEnd();
                List<JsonResponse> data = JsonConvert.DeserializeObject<List<JsonResponse>>(json);
                data = data.Skip(1).ToList();

                int minX= (data[0].boundingPoly.vertices[0].x >= data[0].boundingPoly.vertices[3].x ? data[0].boundingPoly.vertices[3].x : data[0].boundingPoly.vertices[0].x);
            

                foreach (var item in data)
                {
                    Area area = new Area();

                    item.area_Y0 = item.boundingPoly.vertices[1].y - (((item.boundingPoly.vertices[1].y - item.boundingPoly.vertices[0].y) / (item.boundingPoly.vertices[1].x - item.boundingPoly.vertices[0].x))
                        * (item.boundingPoly.vertices[1].x - minX));

                    item.area_Y4 = item.boundingPoly.vertices[2].y - (((item.boundingPoly.vertices[2].y - item.boundingPoly.vertices[3].y) / (item.boundingPoly.vertices[2].x - item.boundingPoly.vertices[3].x))
                        * (item.boundingPoly.vertices[2].x - minX));

                    item.centerPoint = ((item.area_Y4 - item.area_Y0) / 2) + item.area_Y0;
                                        
                    
                }


                data = data.OrderBy(o => (o.centerPoint)).ToList();
                List<JsonResponse> sortedData= new List<JsonResponse>();  

                foreach (JsonResponse item in data)
                {
                    if(sortedData.Where(x => x.area_Y0 <= item.centerPoint && x.area_Y4 >= item.centerPoint).ToList().Count() == 0)
                    {
                        sortedData.AddRange(data.Where(x => x.area_Y0 <= item.centerPoint && x.area_Y4 >= item.centerPoint).ToList().OrderBy(o => (o.boundingPoly.vertices[0].x)));
                    }
                    
                }

                int line = 1;
                string text = string.Empty;
                float storeAreaY0 = sortedData[0].area_Y0;
                float storeAreaY4 = sortedData[0].area_Y4;

                using (TextWriter tw = new StreamWriter("..\\..\\..\\response.txt"))
                {
                    foreach (JsonResponse item in sortedData)
                    {

                        if (item.centerPoint > storeAreaY0 && item.centerPoint < storeAreaY4)
                        {
                            text += item.description+" ";
                        }
                        else
                        {
                            tw.WriteLine(line + " | " + text);
                            storeAreaY0 = item.area_Y0;
                            storeAreaY4 = item.area_Y4;
                            line++;
                            text = item.description + " ";
                        }
                       
                    }
                    tw.WriteLine(line + " | " + text);
                }

      
                
            }
        }
    }


}