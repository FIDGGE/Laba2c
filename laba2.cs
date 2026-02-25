using System;
using System.Collections.Generic;

namespace ZooApp
{
  // базовый класс животного
  class Animal
  {
    public string Name { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string Diet { get; set; }

    // конструктор
    public Animal(string name, int age, string habitat, string diet)
    {
      Name = name;
      Age = age;
      Habitat = habitat;
      Diet = diet;
    }

    // виртуальный метод для получения информации
    public virtual string GetInfo()
    {
      return $"{Name} {Age} {Habitat} {Diet}";
    }
  }

  // млекопитающее
  class Mammal : Animal
  {
    public bool HasFur { get; set; }

    // конструктор млекопитающего
    public Mammal(string name, int age, string habitat, string diet, bool hasFur)
      : base(name, age, habitat, diet)
    {
      HasFur = hasFur;
    }

    // переопределенный метод
    public override string GetInfo()
    {
      string furStatus = HasFur ? "да" : "нет";
      return $"{base.GetInfo()} млекопитающее шерсть:{furStatus}";
    }
  }

  // птица
  class Bird : Animal
  {
    public double WingSpan { get; set; }

    // конструктор птицы
    public Bird(string name, int age, string habitat, string diet, double wingSpan)
      : base(name, age, habitat, diet)
    {
      WingSpan = wingSpan;
    }

    // переопределенный метод
    public override string GetInfo()
    {
      return $"{base.GetInfo()} птица крылья:{WingSpan}м";
    }
  }

  // рыба
  class Fish : Animal
  {
    public string WaterType { get; set; }

    // конструктор рыбы
    public Fish(string name, int age, string habitat, string diet, string waterType)
      : base(name, age, habitat, diet)
    {
      WaterType = waterType;
    }

    // переопределенный метод
    public override string GetInfo()
    {
      return $"{base.GetInfo()} рыба вода:{WaterType}";
    }
  }

  // пресмыкающееся
  class Reptile : Animal
  {
    public bool IsVenomous { get; set; }

    // конструктор пресмыкающегося
    public Reptile(string name, int age, string habitat, string diet, bool isVenomous)
      : base(name, age, habitat, diet)
    {
      IsVenomous = isVenomous;
    }

    // переопределенный метод
    public override string GetInfo()
    {
      string venomStatus = IsVenomous ? "да" : "нет";
      return $"{base.GetInfo()} рептилия яд:{venomStatus}";
    }
  }

  // земноводное
  class Amphibian : Animal
  {
    public string SkinMoisture { get; set; }

    // конструктор земноводного
    public Amphibian(string name, int age, string habitat, string diet, string skinMoisture)
      : base(name, age, habitat, diet)
    {
      SkinMoisture = skinMoisture;
    }

    // переопределенный метод
    public override string GetInfo()
    {
      return $"{base.GetInfo()} земноводное кожа:{SkinMoisture}";
    }
  }

  // менеджер животных (singleton)
  class AnimalManager
  {
    private static AnimalManager _instance;
    private List<Animal> animals = new List<Animal>();

    // приватный конструктор
    private AnimalManager() { }

    // свойство для получения экземпляра
    public static AnimalManager Instance
    {
      get
      {
        if (_instance == null)
          _instance = new AnimalManager();
        return _instance;
      }
    }

    // добавление животного
    public void AddAnimal(Animal newAnimal)
    {
      animals.Add(newAnimal);
      Console.WriteLine($"{newAnimal.Name} добавлен");
    }

    // показать всех животных
    public void ShowAllAnimals()
    {
      if (animals.Count == 0)
        Console.WriteLine("нет животных");
      else
        for (int animalIndex = 0; animalIndex < animals.Count; animalIndex++)
          Console.WriteLine($"{animalIndex + 1}. {animals[animalIndex].GetInfo()}");
    }

    // показать животное по индексу
    public void ShowAnimalByIndex(int animalIndex)
    {
      if (animalIndex >= 0 && animalIndex < animals.Count)
        Console.WriteLine(animals[animalIndex].GetInfo());
      else
        Console.WriteLine("животное не найдено");
    }

    // показать животное по имени
    public void ShowAnimalByName(string animalName)
    {
      foreach (var currentAnimal in animals)
        if (currentAnimal.Name == animalName)
        {
          Console.WriteLine(currentAnimal.GetInfo());
          return;
        }
      Console.WriteLine($"{animalName} не найден");
    }
  }

  // главная программа
  class Program
  {
    static void Main()
    {
      // получаем экземпляр менеджера
      AnimalManager manager = AnimalManager.Instance;

      // добавляем тестовых животных
      manager.AddAnimal(new Mammal("Барсик", 5, "лес", "хищник", true));
      manager.AddAnimal(new Bird("Кеша", 2, "тропики", "всеядное", 0.3));
      manager.AddAnimal(new Fish("Немо", 1, "океан", "всеядное", "морская"));

      // бесконечный цикл меню
      while (true)
      {
        Console.WriteLine("\n1. все животные");
        Console.WriteLine("2. найти по номеру");
        Console.WriteLine("3. найти по имени");
        Console.WriteLine("4. добавить животное");
        Console.WriteLine("5. выход");
        Console.Write("выберите действие: ");

        string userChoice = Console.ReadLine();

        if (userChoice == "1")
        {
          manager.ShowAllAnimals();
        }
        else if (userChoice == "2")
        {
          Console.Write("введите номер: ");
          if (int.TryParse(Console.ReadLine(), out int animalNumber))
            manager.ShowAnimalByIndex(animalNumber - 1);
        }
        else if (userChoice == "3")
        {
          Console.Write("введите имя: ");
          string searchName = Console.ReadLine();
          manager.ShowAnimalByName(searchName);
        }
        else if (userChoice == "4")
        {
          // выбор типа животного
          Console.WriteLine("тип животного:");
          Console.WriteLine("1. млекопитающее");
          Console.WriteLine("2. птица");
          Console.WriteLine("3. рыба");
          Console.WriteLine("4. рептилия");
          Console.WriteLine("5. земноводное");
          Console.Write("выберите тип: ");

          string animalTypeChoice = Console.ReadLine();

          // общие данные
          Console.Write("введите имя: ");
          string newAnimalName = Console.ReadLine();

          Console.Write("введите возраст: ");
          int newAnimalAge = int.Parse(Console.ReadLine());

          Console.Write("введите среду обитания: ");
          string newAnimalHabitat = Console.ReadLine();

          Console.Write("введите тип питания: ");
          string newAnimalDiet = Console.ReadLine();

          // создание животного выбранного типа
          if (animalTypeChoice == "1")
          {
            Console.Write("есть шерсть? (да/нет): ");
            bool hasFur = Console.ReadLine() == "да";
            manager.AddAnimal(new Mammal(newAnimalName, newAnimalAge, newAnimalHabitat, newAnimalDiet, hasFur));
          }
          else if (animalTypeChoice == "2")
          {
            Console.Write("размах крыльев (м): ");
            double wingSize = double.Parse(Console.ReadLine());
            manager.AddAnimal(new Bird(newAnimalName, newAnimalAge, newAnimalHabitat, newAnimalDiet, wingSize));
          }
          else if (animalTypeChoice == "3")
          {
            Console.Write("тип воды: ");
            string water = Console.ReadLine();
            manager.AddAnimal(new Fish(newAnimalName, newAnimalAge, newAnimalHabitat, newAnimalDiet, water));
          }
          else if (animalTypeChoice == "4")
          {
            Console.Write("ядовитое? (да/нет): ");
            bool venomous = Console.ReadLine() == "да";
            manager.AddAnimal(new Reptile(newAnimalName, newAnimalAge, newAnimalHabitat, newAnimalDiet, venomous));
          }
          else if (animalTypeChoice == "5")
          {
            Console.Write("влажность кожи: ");
            string skin = Console.ReadLine();
            manager.AddAnimal(new Amphibian(newAnimalName, newAnimalAge, newAnimalHabitat, newAnimalDiet, skin));
          }
        }
        else if (userChoice == "5")
        {
          break; // выход из программы
        }
      }
    }
  }
}
