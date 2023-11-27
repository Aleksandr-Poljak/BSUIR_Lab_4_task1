using BSUIR_Lab_4;

Engine engineDefault = new();
Engine engineDiesel = new("BMW_4", "BMW", "Diesel", 123, 550, 4);
Engine engineStandartDiesel = Engine.GetStdEngine(); // Получение объекта дизельного  двигателя через альтернативный конструктор
Engine engineStandartElectric = Engine.GetStdElectricEngine(); // Получение объекта дизельного  двигателя через альтернативный конструктор

// Статический метод для сравнения мощности двигателей
var maxPower = Engine.GetMaxPower(engineDefault, engineDiesel, engineStandartDiesel, engineStandartElectric);
Console.WriteLine($"{engineDefault}\n{engineDiesel}\n{engineStandartDiesel}\n{engineStandartElectric}\n Max Power engine:\n{maxPower}");
 
// Объекты owner имеют методы для работы с объектами  Car. А так же имеют ораничения на связывания с объеками Car ,если последние уже связаны.
Owner oleg = new Owner("Oleg", "Smirnov", 28);
Owner ivan = new Owner("Ivan", "Ivanov", 34);
Owner alex = new Owner("Alex", "Petrov", 31);
Console.WriteLine($"\tOwners\n{oleg}\n{ivan}\n{alex}\n");
// Объекты Car имеют методы для работы с объектами Garage и Owner а так же некоторые автоматически вычисляймые свойства.
Car carDefault = new Car(); // Объект Car без имен и без объекта двигателя Engine/
Car lada = new Car("Lada Granta", "VAZ", "2104284kd12"); // Объект Car без объекта двигателя Engine
// Объекты Car содержащие в себе объект двигателя Engine. Композиция 1 к 1.
// Параметры для создоваймого объекта Engine внутри конструктора Car начинаются  с 4-го параметра.
Car bmw = new Car("bmw x5", "BMW", "11111B", "BMW_4C", "BMW", "Diesel", 124, 800, 4);
Car audi = new Car("audiA4", "Audi", "111111A", "audi_4C", "audi", "Diesel", 125, 802, 6);
Car kia = new Car("KiaC4", "Kia", "11111k", "kia_c4", "kia", "Diesel", 125, 700, 8);
Console.WriteLine($"\tCars\n{carDefault}\n{lada}\n{bmw}\n{audi}\n{kia}");

// Объекты гаража на 10 и 5 мест. Имеют методы для работы с объектами Car, некоторые информационные методы и автоматически выйчесляймые свойства.
Garage scala = new Garage("Scala", "Minsk", "Odincova", 65, 10);
Garage orion = new Garage("Orion", "Minsk", "Masherova", 12, 5);
Console.WriteLine($"\tGarages:\n{scala}\n{orion}");

// Объекты Owner и  Car связываются по типу один ко многим. У одного валдельца может быть несколько авто.У одного многих авто может быть один владелец.
// Сязывать объекты допустимо как методами объекта Car , так и методами объекта Owner. При связывании через любой объект - изменяются оба объекта.
oleg.AddCar(bmw);
audi.AddOwner(oleg);
kia.AddOwner(ivan);
Console.WriteLine($"{bmw}\n{kia}\n");
// Отвязать обекты Owner и Car  так же можно через любой объект .При это связь удаляется на стороне обоих объектов
oleg.RemoveCar(bmw);
audi.RemoveOwner();
Console.WriteLine($"\tDelete link\t{oleg}\n{audi}");

// Объкекты Garage и Сar связываются по типу один ко многим. Авто может находиться только в одном гараже. Один гараж может вмещать несколько авто.
// Связать объекты Garage и  Car можно как и методами Garage так и методами Car. При связывании через любой объект - изменяются оба объекта.
// Так же можно связывать череp присваивание.
scala.AddCar(audi);
kia.PutGarage(scala);
bmw.UserGarage = orion;
Console.WriteLine($"\tCars in Garages\n{audi}\n{bmw}\n{scala}");

//Отвязать объекты  Car и Garage Можно так же из любого метода.
bmw.RemoveGarage();
scala.RemoveCar(kia);
Console.WriteLine($"\tCars in Garages.deleted links\n{scala}\n{bmw}");








