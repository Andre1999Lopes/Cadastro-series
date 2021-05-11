using System;

namespace cadastro_series
{
    class Program
    {
        static SerieRepository Repository = new SerieRepository();
        static void Main(string[] args)
        {
            string userOption = UserOption();
            while (userOption != "X")
            {
                switch (userOption)
                {
                    case "1":
                        ListSeries();
                        break;

                    case "2":
                        InsertSerie();
                        break;

                    case "3":
                        UpdateSerie();
                        break;

                    case "4":
                        DeleteSerie();
                        break;

                    case "5":
                        ShowSerie();
                        break;
                    
                    case "C":
                        Console.Clear();
                        break;
                    
                    case "X":
                        continue;

                    default:
                        Console.WriteLine("Digite uma opção válida");
                        break;
                }
            userOption = UserOption();
            }
        }

        static void ListSeries()
        {
            var list = Repository.List();
            
            if (list.Count == 0)
            {
                Console.WriteLine("\nNenhuma série foi cadastrada.\n");
                return;
            }

            foreach (var serie in list)
            {
                Console.WriteLine($"ID {serie.ReturnId()}: {serie.ReturnTitle()} - {serie.IsDeleted()}");
            }
        }

        static void InsertSerie()
        {
            try
            {
                Console.Clear();
                int genre;
                do
                {
                    foreach (int i in Enum.GetValues(typeof(Genre)))
                    {
                        Console.WriteLine($"{i} - {Enum.GetName(typeof(Genre), i)}");
                    }
                    Console.Write("\nEscolha o gênero da série de acordo com as opções acima: ");
                    genre = int.Parse(Console.ReadLine());
                    if(genre <=0 || genre >= 14) Console.WriteLine("Digite uma opção válida.\n");
                } while(genre <=0 || genre >= 14);

                string title;
                do
                {
                    Console.Write("Digite o título da série: ");
                    title = Console.ReadLine();
                } while(title == "");

                string description;
                do
                {
                    Console.WriteLine("Digite a descrição da série:");
                    description = Console.ReadLine();
                } while(description == "");

                Console.Write("Digite o ano de lançamento da série: ");
                var year = int.Parse(Console.ReadLine());

                var newSerie = new Serie(Repository.NextId(), (Genre)genre, title, description, year);
                Repository.Insert(newSerie);
                Console.WriteLine("Sua série foi adicionada!");
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("A série não está no banco de séries.");
            }
            catch(FormatException)
            {
                Console.WriteLine("Digite apenas números.");
            }
        }

        static void UpdateSerie()
        {
            try
            {
                Console.Clear();
                Console.Write("Digite o ID da série: ");
                var serieId = int.Parse(Console.ReadLine());
                Serie serie = Repository.ReturnById(serieId);
                Console.WriteLine("Caso não queira alterar o dado atual, apenas aperte 'enter'.");
                
                Console.WriteLine($"Digite o título da série. Título anterior: {serie.ReturnTitle()}");
                string title = Console.ReadLine();
                string newTitle = title == "" ? serie.ReturnTitle() : title;
                
                int genre;
                do
                {
                    foreach (int i in Enum.GetValues(typeof(Genre)))
                    {
                        Console.WriteLine($"{i} - {Enum.GetName(typeof(Genre), i)}");
                    }
                    Console.WriteLine("14 - Continuar com o gênero anterior.");
                    Console.WriteLine($"Escolha o gênero da série de acordo com as opções acima. Gênero anterior: {serie.ReturnGenre()}");
                    genre = int.Parse(Console.ReadLine());
                    if (genre <= 0 || genre >= 15) Console.WriteLine("\nDigite uma opção válida.\n");
                } while(genre <= 0 || genre >= 15);
                Genre newGenre = genre == 14 ? serie.ReturnGenre() : (Genre)genre;
                Console.WriteLine(newGenre);

                Console.WriteLine("Digite a descrição da série.");
                Console.WriteLine($"Descrição anterior: {serie.ReturnDescription()}");
                string description = Console.ReadLine();
                string newDescription = description == "" ? serie.ReturnDescription() : description;


                Console.WriteLine($"Digite o ano de lançamento da série. Ano anterior: {serie.ReturnYear()}");
                var year = Console.ReadLine();
                int newYear = year == "" ? serie.ReturnYear() : int.Parse(year);

                var updatedSerie = new Serie(
                    serieId,
                    (Genre)newGenre,
                    newTitle,
                    newDescription,
                    newYear
                );
                Repository.Update(serieId, updatedSerie);
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("A série não está no banco de séries.");
            }
            catch(FormatException)
            {
                Console.WriteLine("Digite apenas números.");
            }
        }

        static void DeleteSerie()
        {
            try
            {
                Console.Write("Digite o ID Da série: ");
                var serieId = int.Parse(Console.ReadLine());
                Serie serie = Repository.ReturnById(serieId);
                if (serie.IsDeleted())
                {
                    Console.WriteLine("A série não está no banco de séries.");
                    return;
                }
                Repository.Delete(serieId);
                Console.WriteLine("Série excluída com sucesso.");
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("A série não está no banco de séries.");
            }
            catch(FormatException)
            {
                Console.WriteLine("Digite apenas números.");
            }
        }

        static void ShowSerie()
        {
            try
            {
                Console.Write("Digite o ID da série: ");
                var serieId = int.Parse(Console.ReadLine());
                Serie serie = Repository.ReturnById(serieId);

                Console.Write(serie);
            }
            catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine("A série não está no banco de séries.");
            }
            catch(FormatException)
            {
                Console.WriteLine("Digite apenas números.");
            }
        }

        static string UserOption()
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("\tBanco de séries");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar uma série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair\n");

            string userOption = Console.ReadLine().ToUpper();
            return userOption;
        }
    }
}
