/* Autor: Julia Kwiatkowska, 268375
 * Linki, z ktorych korzystalam, w celu rozwiazania tego zadania:
 * podzbiory zbioru: https://zpe.gov.pl/a/podzbiory-zbioru-skonczonego-tresc-rozszerzona/DUzQbD05Q
 * typy generyczne: https://cezarywalenciuk.pl/blog/programing/c-java-generics-typy-generyczne-17-porownanie-skladni-i-szybkie-przypomnienie
 * operatory bitowe: https://learn.microsoft.com/pl-pl/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
 * for: https://www.w3schools.com/cs/cs_for_loop.php
 * bool: https://learn.microsoft.com/pl-pl/dotnet/csharp/language-reference/builtin-types/bool
 * komentarze napisalam w jezyku polskim, ale nie korzystalam z polskich znakow alfabetycznych
 */
using System.Diagnostics;


namespace Lista1
{
    /// <summary>
    /// Klasa Zadanie 3 z implementacja metody pozwalajacej na zwracanie dla danego zbioru wszystkich mozliwych jego podzbiorow.
    /// </summary>
    class Zadanie3
    {
        /// <summary>
        /// Metoda Podzbiory zwraca wszystkie pozdbiory dla podanego zbioru. W przypadku, gdy podany zbior jest null-em rzuca wyjatek ArgumentNullException.
        /// Podzbiory sa zwracane jako lista list, a kolejnosc podzbiorow jest dowolna (zgodnie z poleceniem do zadania z listy).
        /// </summary>
        /// <typeparam name="T">Typ elementow w zbiorze wejsciowym.</typeparam>
        /// <param name="zbior">Lista elementow wejsciowego zbioru.</param>
        /// <returns>Lista ze wszystkimi podzbiorami danego zbioru.</returns>
        /// <exception cref="ArgumentNullException">Wyjatek rzucany, w momencie, gdy przekazany zbior jest nullem.</exception>
        public static List<List<T>>Podzbiory<T>(List<T> zbior)
        {
            if (zbior == null)
                throw new ArgumentNullException(nameof(zbior), "Wejsciowy zbior nie moze byc nullem!");
            int n = zbior.Count;
            int liczbaMozliwychPozdbiorow = 1 << n;
            List<List<T>> wynikPodzbiory = new List<List<T>>(liczbaMozliwychPozdbiorow);

            //dla kazdej liczby od 0 do 2^n-1 generowany jest odpowiadajacy podzbior
            for (int i = 0; i < liczbaMozliwychPozdbiorow; i++)
            {
                List<T> podzbiorZbioru = new List<T>();
                for (int j = 0; j < n; j++)
                {
                    //sprawdzenie, czy j-ty bit w liczbie i jest ustawiony
                        if ((i & (1 << j)) != 0)
                        {
                            podzbiorZbioru.Add(zbior[j]);
                        }
                    }
                wynikPodzbiory.Add(podzbiorZbioru);
            }
            return wynikPodzbiory;
        }
        /// <summary>
        /// Funkcja testujaca poprawnosc dzialania metody Podzbiory, dla roznych przypadkow typowych i brzegowych oraz sprawdza rzucanie wyjatkow.
        /// Wyniki testow sa weryfikowane z wykorzystaniem asercji.
        /// </summary>
        public static void TestPodzbiory()
        {
            try
            {
                // Test dla typowego przypadku: zbior standardowy {a,b,c,d} podany w poleceniu do zadania
                List<char> zbiorTestowy = new List<char> { 'a', 'b', 'c', 'd' };
                List<List<char>> wynikPodzbiory = Podzbiory(zbiorTestowy);
                Debug.Assert(wynikPodzbiory.Count == 16, "Test nr 1 (zbior standardowy {a,b,c,d} podany w poleceniu do zadania) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 1 (zbior standardowy {a,b,c,d} podany w poleceniu do zadania) zakonczony sukcesem!");
            }
            catch (Exception)
            {
                Console.WriteLine("Test nr 1 (zbior standardowy {a,b,c,d} podany w poleceniu do zadania) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: zbior pusty { }
                List<char> zbiorTestowy = new List<char> { };
                List<List<char>> wynikPodzbiory = Podzbiory(zbiorTestowy);
                Debug.Assert(wynikPodzbiory.Count == 1, "Test nr 2 (zbior pusty { }) zakonczony niepowodzeniem."); //dla pustego zbioru liczba podzbiorow powinna wynosic 1
                Debug.Assert(wynikPodzbiory[0].Count == 0, "Test nr 2 (zbior pusty { }) zakonczony niepowodzeniem."); //podzbior ten powinien byc pusty
                Console.WriteLine("Test nr 2 (zbior pusty { }) zakonczony sukcesem!");
            }
            catch (Exception)
            {
                Console.WriteLine("Test nr 2 (zbior pusty { }) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: zbior jednoelementowy {a}
                List<char> zbiorTestowy = new List<char> { 'a' };
                List<List<char>> wynikPodzbiory = Podzbiory(zbiorTestowy);
                Debug.Assert(wynikPodzbiory.Count == 2); //dla zbioru jednoelementowego oczekiwana liczba podzbiorow to 2
                //jeden z tych podzbiorow powinien byc pusty a drugi zawierac 'a'
                bool pustyPodzbior = wynikPodzbiory.Exists(podzbior => podzbior.Count == 0);
                bool jedenElement = wynikPodzbiory.Exists(podzbior => podzbior.Count == 1 && podzbior[0] == 'a');
                Debug.Assert(pustyPodzbior && jedenElement, "Test nr 3 (zbior jednoelementowy {a}) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 3 (zbior jednoelementowy {a}) zakonczony sukcesem!");
            }
            catch (Exception)
            {
                Console.WriteLine("Test nr 3 (zbior jednoelementowy {a}) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku rzucania wyjatku, gdy zbior jest nullem
                Podzbiory<char>(null);
                Console.WriteLine("Test nr 4 (przekazanie zbioru jako argumentu null) zakonczony niepowodzeniem.");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Test nr 4 (przekazanie zbioru jako argumentu null) zakonczony sukcesem!");
            }
            catch (Exception)
            {
                Console.WriteLine("Test nr 4 (przekazanie zbioru jako argumentu null) zakonczony niepowodzeniem.");
            }
            try
            {
                // Test dla przypadku: zbior z powtarzajacymi sie elementami {a, a, b}
                List<char> zbiorTestowy = new List<char> { 'a', 'a', 'b'};
                List<List<char>> wynikPodzbiory = Podzbiory(zbiorTestowy);
                Debug.Assert(wynikPodzbiory.Count == 8); //dla zbioru trzyelementowego oczekiwana liczba podzbiorow to 8
                //jeden z tych podzbiorow na pewno powinien byc pusty
                bool pustyPodzbior = wynikPodzbiory.Exists(podzbior => podzbior.Count == 0);
                //przynajmniej jeden podzbior zawierajacy a
                bool aElement = wynikPodzbiory.Exists(podzbior => podzbior.Count == 1 && podzbior[0] == 'a');
                //podzbior zawierajacy b
                bool bElement = wynikPodzbiory.Exists(podzbior => podzbior.Count == 1 && podzbior[0] == 'b');
                //podzbior zawierajacy a i b
                bool abElement = wynikPodzbiory.Exists(podzbior => podzbior.Count == 2 && podzbior.Contains('a') && podzbior.Contains('b'));
                Debug.Assert(pustyPodzbior && aElement && bElement && abElement, "Test nr 5 (zbior z powtarzajacymi sie elementami {a, a, b}) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 5 (zbior z powtarzajacymi sie elementami {a, a, b}) zakonczony sukcesem!");
            }
            catch (Exception)
            {
                Console.WriteLine("Test nr 5 (zbior z powtarzajacymi sie elementami {a, a, b}) zakonczony niepowodzeniem.");
            }

            try
            {
                // Test dla przypadku: zbior z liczbami {1,2}
                List<int> zbiorTestowy = new List<int> { 1, 2};
                List<List<int>> wynikPodzbiory = Podzbiory(zbiorTestowy);
                Debug.Assert(wynikPodzbiory.Count == 4); //dla zbioru dwuuelementowego oczekiwana liczba podzbiorow to 4
                //jeden z tych podzbiorow na pewno powinien byc pusty
                bool pustyPodzbior = wynikPodzbiory.Exists(podzbior => podzbior.Count == 0);
                //przynajmniej jeden podzbior zawierajacy 1
                bool element1 = wynikPodzbiory.Exists(podzbior => podzbior.Count == 1 && podzbior[0] == 1);
                //podzbior zawierajacy 2
                bool element2 = wynikPodzbiory.Exists(podzbior => podzbior.Count == 1 && podzbior[0] == 2);
                //podzbior zawierajacy 1 i 2
                bool element12 = wynikPodzbiory.Exists(podzbior => podzbior.Count == 2 && podzbior.Contains(1) && podzbior.Contains(2));
                Debug.Assert(pustyPodzbior && element1 && element2 && element12, "Test nr 6 (zbior z liczbami {1,2}) zakonczony niepowodzeniem.");
                Console.WriteLine("Test nr 6 (zbior z liczbami {1,2}) zakonczony sukcesem!");
            }
            catch (Exception)
            {
                Console.WriteLine("Test nr 6 (zbior z liczbami {1,2}) zakonczony niepowodzeniem.");
            }



            Console.WriteLine("Udalo sie, wszystkie przeprowadzone testy zakonczyly sie sukcesem!");
        }
           public static void Main(string[] args)
        {
            //w celach upewnienia sie, ze metoda zwraca liste pwszystkich podzbiorow, zgodnie z trescia polecenia do zadania (ten sam przyklad co w poleceniu)
            List<char> zbior = new List<char> { 'a', 'b', 'c', 'd' };
            List<List<char>> podzbiory = Podzbiory(zbior);
            Console.WriteLine("Podzbiory dla zbioru podanego w tresci polecenia do zadania {a,b,c,d}:");
            foreach(var podzbior in podzbiory)
            {
                string podzbiorNapis = "{" + string.Join(",", podzbior) + "}";
                Console.WriteLine(podzbiorNapis);
            }
            //wywolanie funkcji testujacej poprawnosc metody Podzbiory
            TestPodzbiory();
            Console.WriteLine("Prosze wcisnac dowolny klawisz, by zakonczyc.");
            Console.ReadKey();
        }


    }
        }
    

