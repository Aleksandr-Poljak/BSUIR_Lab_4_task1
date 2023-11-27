using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR_Lab_4
{
    internal class Garage
    {
        public string name;
        public string City { get; init; }
        public string Street { get; init; }
        private int houseNumber;
        private int numberPlaces = 1;
        private Car?[] cars;

        public int HouseNumber
        {
            get { return houseNumber; }
            init 
            {
                if (value > 0)
                {
                    houseNumber = value;
                }
                else 
                {
                    throw new Exception("Номер дома должен быть больше 0.");
                }
            }
        }
        public int NumberPlaces
        {
            get { return numberPlaces; }
            init
            {
            if(numberPlaces > 0)
                {
                    numberPlaces = value;
                }
                else
                {
                    throw new Exception("Количество мест должно быть больше 0.");
                }
            }
        } // количество парковочных мест
        public int NumberAvailablePlaces
        {
            get { return NumberPlaces - NumberCars; }
        } // Количество свободных парковочных мест.
        public int NumberCars { get; private set; } = 0; //Количество машин в гараже

        public Garage()
        {
            name = "Unknown";
            City = "Unknown";
            Street = "Unknown";
            houseNumber = 0;
            cars = new Car[1];
        }
        public Garage(string name, string city, string street, int houseNumber, int numberPlaces)
        {
            this.name = name;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            NumberPlaces = numberPlaces;
            cars = new Car[NumberPlaces];
            
        }

        public int AddCar(Car car)
        {
            // Добавление car в гараж ,на первое свободное место.Если есть свободные места.
            int carPlace = SearchCar(car);

            if (carPlace > -1 && car.UserGarage == this)
            {
                throw new Exception($"{car.Model}: {car.SerialNumber} уже стоит в этом гараже.");
            }
            if (car.UserGarage != null && car.GaragePlace != -1)
            {
                throw new Exception($"{car.Model}: {car.SerialNumber} уже стоит в другом  гараже на месте {carPlace}.");
            }
            if (!HasPlaces())
            {
                throw new Exception("Отсувствуют парковочные места.");
            }

            if(carPlace == -1 && car.UserGarage == null && car.GaragePlace == -1)
            {
                for (int i = 0; i < cars.Length; i++)
                {
                    if (cars[i] == null)
                    {                   
                        cars[i] = car;
                        NumberCars++;
                        car.PutGarage(this);
                        return i;
                    }
                }     
            }
            return -1;
        }
        public void RemoveCar(Car car)
        {
            // Удаление авто из гаража
            if (cars.Contains(car))
            {
                for(int i =0; i < cars.Length; i++)
                {
                    if (cars[i] == car)
                    {
                        cars[i] = null;
                        NumberCars--;
                        car.RemoveGarage();
                        break;
                    }
                }
            }
            else
            {
                throw new Exception($"{car.Model}: {car.SerialNumber} нет на этой парковке.");
            }

        }
        public Car RemoveCar(int number)
        {
            // Удаление авто из гаража
            Car? result = SearchCar(number);

            if (result != null)
            {
                RemoveCar(result);
                return result;
            }
            else
            {
                throw new Exception($"На парковочном месте {number} нет авто.");
            }
        }
  
        public bool HasPlaces()
        {
            if(NumberAvailablePlaces> 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int SearchCar(Car car)
        {   // Возвращает место авто в гараже
            if (cars.Contains(car))
            {
                return Array.IndexOf(cars, car);
            }
            return -1;

        }
        public Car? SearchCar(int number)
        {
            // Возвращет авто по указанному в аргументе индексу
            if (number > cars.Length-1)
            {
                throw new IndexOutOfRangeException("Нет такого парковочного места");
            }
            else
            {
                return cars[number];
            }
        }
        public override string ToString()
        {
            string info = $"Название: {name}.\nАдрес: Город {City}, улица {Street} дом {HouseNumber}." +
                $"\nКоличество мест: {NumberPlaces}.\nКоличество свободных мест: {NumberAvailablePlaces}.\n";
            return info;
        }
    }
}
