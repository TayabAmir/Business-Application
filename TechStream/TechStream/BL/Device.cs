using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStream.BL
{
    public class Device
    {
        private string Type;
        private string Company;
        private string Model;
        private double ModelPrice;
        public Device(string type, string company, string model, double modelPrice)
        {
            Type = type;
            Company = company;
            Model = model;
            ModelPrice = modelPrice;
        }
        public string GetDeviceType() {  return Type; }
        public string GetCompany() {  return Company; }
        public string GetModel() { return Model; }
        public double GetModelPrice() {  return ModelPrice; }
        public void SetType(string type) { Type = type; }
        public void SetCompany(string company) {  Company = company; }
        public void SetModel(string model) {  Model = model; }
        public void SetPrice(double price) { ModelPrice = price; }
    }
}
