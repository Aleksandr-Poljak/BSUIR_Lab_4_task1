using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR_Lab_4
{
    internal class Engine
    {

        public readonly string[] typesEngine = { "Electric", "Diesel", "Gas", "Gasoline"};

        public readonly string model;
        public readonly string manufacturer;

        private int cylinderCount;
        private double cylenderVolume;
        private int power;
        private string engineType;

        public string EngineType
        {
            // Тип двигателя может быть установлен только из списка поддерживаймых двигателей.
            get { return engineType; }
            set 
            {
                if(typesEngine.Contains(value))
                {
                    engineType = value;
                }
                else
                {
                    throw new ArgumentException($"Unsupported motor type. Type must be: {string.Join(", ", typesEngine)}");
                }
            }

        }
        public double CylinderVolume
        {
            // Объем одного цилиндра может быть от 300 куб.см  до 1000 куб.см. Или 0 для электрических двигателей.
            get { return cylenderVolume; }
            set
            {
                if (value == 0 || (value > 300&& value <=1000))
                {
                    cylenderVolume = value;
                }
                else
                {
                    throw new Exception("Impermissible values. The volume must be between 300 and 1000. Or 0 for Electric CarEngine");
                }
            }
        }
        public int CylinderCount
        {
            // Количество цилиндров может быть от 1 до 16 или 0 для электрических двигателей.
            get { return cylinderCount; }
            set 
            { 
                if(value == 0 || (value >=1 && value <=16))
                {
                    cylinderCount = value;
                }
                else
                {
                    throw new Exception("Impermissible values. The volume must be between 1 and 16.Or 0 for Electric CarEngine");
                }
            }
        }
        public int Power
        {
            get { return power; }
            set
            {
                if(value < 1 && value > 300)
                {
                    throw new Exception("Impermissible values. The volume must be between 10 and 300.");
                }
                else
                {
                    power = value;
                }
            }
        }

        public Engine()
        {
            model = "Unknown";
            manufacturer = "Unknown";
            engineType = "Unknown";
        }
        public Engine(string model, string manufacturer, string engineType,int power, double cylinderVolume, int cylinderCount) :this()
        {
            this.model = model;
            this.manufacturer = manufacturer;
            EngineType = engineType;
            CylinderVolume = cylinderVolume;
            Power = power;
            CylinderCount = cylinderCount;
        }

        public double TotalVolume
        {
            // Сумарный объем двигателя
            get { return CylinderVolume * cylinderCount;}
        }

        public static Engine GetStdEngine()
        {
            //Алтернатинвый конструктор. Создает двигатель по умолчанию.
            Engine defaultEngine = new Engine ("Audi_2.0", "Audi", "Gasoline", 95, 400, 4);
            return defaultEngine;
        }
        public static Engine GetStdElectricEngine()
        {
            //Алтернатинвый конструктор. Создает электрический двигатель по умолчанию.
            Engine defaultEngine = new Engine("Tesla_102", "Tesla", "Electric", 102, 0, 0);
            return defaultEngine;
        }
        public static Engine GetMaxPower(params Engine[] arr)
        {
            // Возвращает самый мощный двигатель.
            Engine MaxPowerEngine = arr[0];
            
            for(int i=1; i<arr.Length; i++)
            {
                if (arr[i].Power > MaxPowerEngine.Power)
                {
                    MaxPowerEngine = arr[i];
                }
            }
            return MaxPowerEngine;       
        }
        public override string ToString()
        {
            string border = new string('*', 40);
            string info = $"\nМодель: {model}.\nПроизводитель: {manufacturer}.\nТип двигаетля: {EngineType}.\nОбъем цилиндра: {CylinderVolume} куб.см.\n" +
                $"Количество цилиндров: {CylinderCount}.\nОбщий объем двигателя: {TotalVolume} куб.см.\nМощность: {Power} л.с.\n";
            return info;
        }
    }

}
