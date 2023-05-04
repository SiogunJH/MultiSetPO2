using System.Collections;
using System.Collections.Generic;
using MultiSet;

namespace IMultiSet
{
    /// <summary>
    /// MultiSet, to rozszerzenie koncepcji zbioru, dopuszczające przechowywanie duplikatów elementów 
    /// </summary>
    /// <remarks>
    /// * Reprezentacja wewnętrzna: `Dictionary<T, int>`
    /// * Porządek zapamiętania elementów jest bez znaczenia, zatem {a, b, a} jest tym samym multizbiorem, co {a, a, b}
    /// * W konstruktorze można przekazać informację o sposobie porównywania elementów (`IEqualityComparer<T>`)
    /// </remarks>
    /// <typeparam name="T">dowolny typ, bez ograniczeń</typeparam>

    public interface IMultiSet<T> : ICollection<T>, IEnumerable<T> where T : notnull
    {

        #region === from ICollection<T> ============================================

        //TESTED
        // zwraca liczbę wszystkich elementów multizbioru (łącznie z duplikatami)
        public new int Count { get; }

        //SKIPPED
        // zwraca `true` jeśli multibiór jest tylko do odczytu, `false` w przeciwnym przypadku
        public new bool IsReadOnly { get; }

        //TESTED
        // dodaje element do multizbioru
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        public new void Add (T item);

        //TESTED
        // usuwa element z multizbioru, zwraca `true`, jesli operacja przebiegła pomyślnie, 
        // `false` w przeciwnym przypadku (elementu nie znaleziono)
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        public new bool Remove (T item);

        //TESTED
        // zwraca `true`, jeśli element należy do multizbioru
        public new bool Contains (T item);

        //TESTED
        // usuwa wszystkie elementy z multizbioru
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        public new void Clear ();

        //TESTED
        // kopiuje elementy multizbioru do tablicy, od wskazanego indeksu
        public new void CopyTo (T[] array, int arrayIndex);

        // --- from IEnumerable<T> --------------------------

        //TESTED
        // zwraca iterator multizbioru (wariant generyczny)
        public new IEnumerator<T> GetEnumerator();

        //TESTED
        // zwraca iterator multizbioru (wariant niegeneryczny)
        // C#8 default implementation
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        #endregion -----------------------------------------------------------------

        //TESTED
        // dodaje `numberOfItems` takich samych elementów `item` do multizbioru 
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> Add (T item, int numberOfItems = 1);

        //TESTED
        // usuwa `numberOfItems` elementów `item` z multizbioru,
        // jeśli `numberOfItems` jest większa niż liczba faktycznie przechowywanych elementów
        //   usuwa wszystkie
        // jesli elementu nie znaleziono - nic nie robi, nie zgłasza żadnego wyjątku
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> Remove (T item,  int numberOfItems = 1);

        //TESTED
        // usuwa wszystkie elementy `item`
        // jesli elementu nie znaleziono - nic nie robi, nie zgłasza żadnego wyjątku
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> RemoveAll(T item);

        //TESTED
        // dodaje sekwencję `IEnumerable<T>` do multizbioru
        // zgłasza `ArgumentNullException` jeśli `other` jest `null`
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> UnionWith (IEnumerable<T> other);

        //TESTED
        // modyfikuje bieżący multizbiór tak, aby zawierał tylko elementy wspólne z `other`
        // zgłasza `ArgumentNullException` jeśli `other` jest `null`
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> IntersectWith(IEnumerable<T> other);

        //TESTED
        // modyfikuje bieżący multizbiór tak, aby zawierał tylko te 
        // które nie wystepują w `other`
        // zgłasza `ArgumentNullException` jeśli `other` jest `null`
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> ExceptWith(IEnumerable<T> other);

        //TESTED
        // modyfikuje bieżący multizbiór tak, aby zawierał tylko te elementy
        // które wystepują w `other` lub występują w bieżacym multizbiorze,
        // ale nie wystepują równocześnie w obu
        // zgłasza `ArgumentNullException` jeśli `other` jest `null`
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> SymmetricExceptWith(IEnumerable<T> other);

        //TESTED
        // określa, czy multizbiór jest podzbiorem `other`
        // zgłasza `ArgumentNullException`, jeśli `other` jest `null`
        public bool IsSubsetOf(IEnumerable<T> other);

        //TESTED
        // określa, czy multizbiór jest podzbiorem właściwym `other` (silna inkluzja)
        // zgłasza `ArgumentNullException`, jeśli `other` jest `null`
        public bool IsProperSubsetOf(IEnumerable<T> other);

        //TESTED
        // określa, czy multizbiór jest nadzbiorem `other`
        // zgłasza `ArgumentNullException`, jeśli `other` jest `null`
        public bool IsSupersetOf(IEnumerable<T> other);

        //TESTED
        // określa, czy multizbiór jest nadzbiorem właściwym `other` (silna inkluzja)
        // zgłasza `ArgumentNullException`, jeśli `other` jest `null`
        public bool IsProperSupersetOf(IEnumerable<T> other);

        //TESTED
        // określa, czy multizbiór i `other` mają przynajmniej jeden element wspólny
        // zgłasza `ArgumentNullException`, jeśli `other` jest `null`
        public bool Overlaps(IEnumerable<T> other);

        //TESTED
        // określa, czy multizbiór i `other` mają takie same elementy w tej samej liczności
        // nie zwracając uwagi na kolejność ich zapamiętania
        // zgłasza `ArgumentNullException`, jeśli `other` jest `null`
        public bool MultiSetEquals(IEnumerable<T> other);

        //TESTED
        // określa, czy multizbiór jest pusty     
        public bool IsEmpty { get; }

        //SKIPPED
        // zwraca obiekt wykorzystywany do określania równości elementów kolekcji
        public IEqualityComparer<T> Comparer { get; }
        // -------------------------

        //TESTED
        // indexer, zwraca, dla zadanego `item`, liczbę jego powtórzeń w multizbiorze
        public int this[T item] { get; }

        //SKIPPED
        // zwraca MultiSet jako Dictionary
        public IReadOnlyDictionary<T, int> AsDictionary();

        //SKIPPED
        // zwraca MultiSet jako Set, usuwając duplikaty
        public IReadOnlySet<T> AsSet();


        // -------------------------
        // konstruktory, metody statyczne i operatory -> do zaimplementowania (nie da się zadeklarować w interfejsie)

        //TESTED
        // zwraca pusty multizbiór
        public static IMultiSet<T> Empty { get; }

        /*
        //TESTED
        // Konstruktor, tworzy pusty multizbiór
        public MultiSet();

        //SKIPPED
        // Konstruktor, tworzy pusty multizbiór, w którym równość elementów zdefiniowana jest
        // za pomocą obiektu `comparer`
        public MultiSet(IEqualityComparer<T> comparer)

        //TESTED
        // Konstruktor, tworzy multizbiór wczytując wszystkie elementy z `sequence`
        public MultiSet(IEnumerable<T> sequence)

        //SKIPPED
        // Konstruktor, tworzy multizbiór wczytując wszystkie elementy z `sequence`
        // Równośc elementów zdefiniowana jest za pomocą obiektu `comparer`
        public MultiSet(IEnumerable<T> sequence, IEqualityComparer<T> comparer)

        //TESTED
        // tworzy nowy multizbiór jako sumę multizbiorów `first` i `second`
        // zwraca `ArgumentNullException`, jeśli którykolwiek z parametrów jest `null`
        public static IMultiSet<T> operator +(IMultiSet<T> first, IMultiSet<T> second);

        //TESTED
        // tworzy nowy multizbiór jako różnicę multizbiorów: od `first` odejmuje `second`
        // zwraca `ArgumentNullException`, jeśli którykolwiek z parametrów jest `null`
        public static IMultiSet<T> operator -(IMultiSet<T> first, IMultiSet<T> second);

        //TESTED
        // tworzy nowy multizbiór jako część wspólną multizbiorów `first` oraz `second`
        // zwraca `ArgumentNullException`, jeśli którykolwiek z parametrów jest `null`
        public static IMultiSet<T> operator *(IMultiSet<T> first, IMultiSet<T> second);
        */
    }
}