using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR_Lab_4
{
    internal class Owner
    {
        public string name;
        public string surname;
        public int age;
        private Car[] cars;

        public int Age
        {
            get { return age; } set
            {
                if(value >0 && age < 120)
                {
                    age = value;
                }
                else
                {
                    throw (new Exception("Not valid age."));
                }
            }
        }
        public Car[] Cars
        {
            get { return cars; }
        }

        public Owner()
        {
            name = "Unknown";
            surname = "Unknown";
            cars = new Car[0];
        }
        public Owner(string name, string surname, int age) :this()
        {
            this.name = name;
            this.surname = surname;
            Age = age;
        }

        public bool HasCar(Car car)
        {
            // Проверяет ,есть ли у владельца авто.
            return Cars.Contains(car);
        }
        public bool HasCar(string serialNumber)
        {
            // Проверяет по серийному номеру ,есть ли у владельца авто.
            foreach(Car car in Cars)
            {
                if (car.SerialNumber == serialNumber)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddCar(Car car)
        {
            // Регистрирует владельцу  автомобиль.
            if(car.UserOwner == null || car.UserOwner == this)
            {
                if (car.UserOwner == null)
                {
                    car.UserOwner = this;
                }
                
                if(!HasCar(car))
                {
                    cars = cars.Append(car).ToArray();
                }
            }
            else
            {
                throw new ArgumentException($"У {car.Model}: {car.SerialNumber} уже есть владелец.");
            }

        }
        public void AddCars(params Car[] cars)
        {;
            // Регистрирует владельцу автомобили.
           foreach(Car car in cars)
            {
                AddCar(car);
            }
        }
        public void RemoveCar(Car car)
        {
            // Удаляет автомобиль 

            if (Cars.Contains(car))
            {
                Car[] bufferCars = new Car[0];
                foreach(Car userCar in Cars)
                {
                    if (userCar != car)
                    {
                        bufferCars=bufferCars.Append(userCar).ToArray();
                    }
                }
                cars = bufferCars;
            }

            if (car.UserOwner == this)
            {
                car.RemoveOwner();
            }
        }
        public string AllCarsInfo()
        {
            // Выводит строку для печати с сведениями о всех зарегистрированых авто.
            string info = "Зарегистрированные авто: ";

            foreach (Car car in Cars)
            {   
                info += $"{car.Model}:{car.SerialNumber}, ";
            }
            info = info.EndsWith(", ")?info.Remove(info.Length-2):info; //удаление ", " в конце строки
            return info;
        }
        public Car[] AllCars()
        {
            return Cars;
        }
    }
}
