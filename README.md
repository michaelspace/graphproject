# Floyd-Warshall algorithm implementation

Implementacja [algorytmu Floyda-Warshalla](https://pl.wikipedia.org/wiki/Algorytm_Floyda-Warshalla) do znajdowania najkrótszych ścieżek pomiędzy wszystkimi parami wierzchołków w grafie ważonym, stworzona przy pomocy WPF C#.
<br>
![Picture](https://github.com/michaelspace/graphproject/blob/master/pic.png)
<br>
Instrukcja:<br>
1) Należy wprowadzić nazwę wierzchołka. W przeciwnym razie zostanie wylosowana nazwa miasta.<br>
2) Po kliknięciu przycisku dodawania wierzchołka kliknąć na wybrane miejsce w obszarze grafu.<br>
3) Aby dodać krawędzie należy wprowadzić wagę, a następnie kliknąć przycisk dodawania krawędzi, kolejno klikając na dwa wierzchołki, pomiędzy którymi ma zostać utworzone połączenie.<br>
4) Dodane wierzchołki można przesuwać.<br>
5) Graf może zostać zresetowany.<br>
6) Aby zobaczyć najkrótsze koszty dojscia dla każdej pary wierzchołków, należy kliknąć przycisk do wygenerowania macierzy kosztów.<br>
7) By móc zobaczyć ścieżkę między daną parą wierzchołków, należy kliknąć na wybrany koszt. Na grafie kolorem czerwonym zostanie zaznaczona najkrótsza ścieżka między wybraną parą.

<br>
Uwagi:<br>
1) Limit wierzchołków wynosi 14.<br>
2) W celu uniknięcia ujemnych cykli wagi zostały ograniczone do wartości dodatnich.<br>
3) Aplikacja jest niedużym uczelnianym projektem.<br>

![Picture](https://github.com/michaelspace/graphproject/blob/master/pic2.png)
