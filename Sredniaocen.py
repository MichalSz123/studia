print("Podaj liczbę ocen:")
liczba_ocen = int(input())

suma = 0.0

# Pętla do wprowadzania ocen
for i in range(liczba_ocen):
    print(f"Podaj ocenę {i + 1}:")

    wejscie = input()
    try:
        ocena = float(wejscie.replace(",", "."))
        suma += ocena
    except ValueError:
        print("Błędny format oceny!")

# Obliczanie średniej
srednia = suma / liczba_ocen

# Wyświetlanie wyniku z zaokrągleniem do 2 miejsc po przecinku
print(f"Średnia: {srednia:.2f}")

if srednia >= 3.0:
    print("Uczeń zdał.")
else:
    print("Uczeń nie zdał.")
    