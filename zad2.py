class Player:
    def __init__(self, name, symbol):
        self.name = name
        self.symbol = symbol

class Board:
    def __init__(self):
        # Plansza to 9 pustych miejsc (indeksy 0-8)
        self.grid = [' '] * 9

    def display(self):
        print("\n")
        print(f" {self.grid[0]} | {self.grid[1]} | {self.grid[2]} ")
        print("---+---+---")
        print(f" {self.grid[3]} | {self.grid[4]} | {self.grid[5]} ")
        print("---+---+---")
        print(f" {self.grid[6]} | {self.grid[7]} | {self.grid[8]} ")
        print("\n")

    def place_symbol(self, position, symbol):
        # Sprawdzamy, czy pole jest puste
        if self.grid[position] == ' ':
            self.grid[position] = symbol
            return True
        return False

    def check_winner(self):
        # Definiujemy wygrywające kombinacje indeksów
        win_conditions = [
            (0, 1, 2), (3, 4, 5), (6, 7, 8),  # Wiersze
            (0, 3, 6), (1, 4, 7), (2, 5, 8),  # Kolumny
            (0, 4, 8), (2, 4, 6)              # Przekątne
        ]
        for a, b, c in win_conditions:
            if self.grid[a] == self.grid[b] == self.grid[c] != ' ':
                return True
        return False

    def is_full(self):
        return ' ' not in self.grid


class Game:
    def __init__(self):
        # Kompozycja: Gra tworzy planszę i graczy
        self.board = Board()
        self.player1 = Player("Gracz 1", "X")
        self.player2 = Player("Gracz 2", "O")
        self.current_player = self.player1

    def switch_turn(self):
        if self.current_player == self.player1:
            self.current_player = self.player2
        else:
            self.current_player = self.player1

    def start(self):
        print("Rozpoczynamy grę w Kółko i Krzyżyk! (Wpisz 1-9)")
        
        while True:
            self.board.display()
            print(f"Ruch wykonuje: {self.current_player.name} ({self.current_player.symbol})")
            
            try:
                # Odejmujemy 1, aby dopasować wybór (1-9) do indeksów tablicy (0-8)
                choice = int(input("Wybierz pole (1-9): ")) - 1
                
                if choice < 0 or choice > 8:
                    print("Błąd: Wybierz liczbę z zakresu 1-9.")
                    continue

                # Przekazanie decyzji gracza do planszy
                if self.board.place_symbol(choice, self.current_player.symbol):
                    # Sprawdzenie warunków końca gry
                    if self.board.check_winner():
                        self.board.display()
                        print(f"Gratulacje! {self.current_player.name} wygrywa!")
                        break
                    elif self.board.is_full():
                        self.board.display()
                        print("Koniec gry. Mamy remis!")
                        break
                    
                    # Jeśli gra trwa, zmieniamy turę
                    self.switch_turn()
                else:
                    print("To pole jest już zajęte! Wybierz inne.")
                    
            except ValueError:
                print("Błąd: Musisz podać liczbę.")

# Uruchomienie gry
if __name__ == "__main__":
    game = Game()
    game.start()
