using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BSUIR_Lab_4
{
    internal class Car
    {
        public string Model { get; init; }
        public string Manufacturer { get; init; }
        public string SerialNumber { get; init; } 
        private Engine engine; //двигаетль
        private Owner? owner; //владелец
        private Garage? garage; // гараж
        private int place = -1;  // место в гараже

        public Engine CarEngine
        {
            get { return engine; }
        }
        public Owner? UserOwner
        {
            get { return owner; }
            set
            {   
                if (owner== value || owner==null)
                {
                    if (owner== null)
                    {
                        owner = value;
                    }
                    if(value is not null && !value.HasCar(this)) 
                    {                        
                        value.AddCar(this);
                    }

                }
                else
                {
                    throw new ArgumentException($"У {Model}: {SerialNumber} уже есть владелец.");
                }
                 
            }
        }
        public Garage? UserGarage
        {
            get { return  garage; }
            set
            {
               if(garage ==null && GaragePlace==-1 && value != null) 
                {
                    if(value.SearchCar(this) != -1)
                    {
                        GaragePlace = value.SearchCar(this);
                        garage = value;
                    }
                    else
                    {
                        GaragePlace = value.AddCar(this);
                        garage = value;
                    }
                } // Связывает авто и гараж
               
               if(garage != null && GaragePlace != -1 && value is null)
                {  
                    if (GaragePlace == garage.SearchCar(this))
                    {
                        garage.RemoveCar(this);
                    }
                    garage = null;
                    GaragePlace = -1;
               } // Разывает связь авто с гаражом
               if (garage != null && value != null && value != garage)
                {
                    RemoveGarage();
                    PutGarage(value);
                } // Связывает авто с новым гаражом при присвоении нового гаража не через методы.
            }
        } 
        public int GaragePlace
        {
            get { return place; }
            private set { place = value; }
        }

        public Car()
        {
            Model = "Unknown";
            Manufacturer = "Unknown";
            SerialNumber = "Unknown";
            engine = new Engine();
            owner = null;
        }
        public Car(string model, string manufaterer, string serialNumber) :this()
        {
            this.Model = model;
            this.Manufacturer = manufaterer;
            this.SerialNumber = serialNumber;
        }
        public Car(string modelCar, string manufacturerCar, string serialNumberCar, // Параметры для авто
            string modelEngine, string manufatererEngine, string engineType, int power, double cylinderVolume, int cylinderCount) // парам для CarEngine
            : this(modelCar, manufacturerCar, serialNumberCar)
        {
            engine = new Engine(modelEngine, manufatererEngine, engineType, power, cylinderVolume, cylinderCount);
        }

        public override string ToString()
        {
            string underlining = new string('-', 50) + '\n';
            string border = '\n' + new string('=', 50) + "\n\n";

            string info = $"Модель: {Model}.\nПроизводитель: {Manufacturer}.\nСерийный номер: {SerialNumber}\n" + underlining;
            info += "Данные двигателя:" + CarEngine.ToString() + underlining;
            info += info + $"Гараж: {(UserGarage != null ? UserGarage.name : "-")}. Место: {(GaragePlace != -1 ? GaragePlace : "-")}\n" + underlining;
            info = owner != null ? info + $"Владелец: {owner.name} {owner.surname}\n" +border: info + border;

            return info;
        }

        // Взаимодействие с собственником.
        public bool HasOwner()
        {
            // Проверяет,есть-ли у авто владелец
            return UserOwner != null;
        }
        public void AddOwner(Owner driver)
        {
            // Закрепляет за авто владельца

            UserOwner=driver;
        }
        public void RemoveOwner()
        {
            // удаляет ссылку на владельца авто.
           if(UserOwner != null && UserOwner.HasCar(this))
            {
                UserOwner.RemoveCar(this);
            }
           owner = null;
        }
        public Owner? GetOwner()
        {
            //возвращает водителя,если он есть. В ином случае - null.
            return UserOwner;
        }

        //Взаимодейстивие с гаражом.
        public int PutGarage(Garage parking)
        {
            // Ставит авто в гараж.
            UserGarage = parking;
            return GaragePlace;
        }
        public void RemoveGarage()
        {
            // Удаляет авто из гаража
            if( UserGarage != null)
            {
                UserGarage = null;           
            }
        }
        public bool InGarage()
        {
            // Проверяет ,находится-ли авто в гараже.
            return UserGarage != null && GaragePlace != -1;
        }
    }
}
