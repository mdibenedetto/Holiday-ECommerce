using System;
namespace dream_holiday.Models
{
    public class TemplateModel
    {
        public String prop1 = "Prop 1 test";

        public TemplateModel()
        {

        }

        public TemplateModel(String prop1)
        {
            this.prop1 = prop1;
        }

        public TemplateModel(String prop1, int prop2)
        {
            this.prop1 = prop1;
        }
    }
}
