# Text-Roulette

A TUI (Text User Interface) implementation of Buckshot Roulette, built with C# and [Terminal.Gui](https://github.com/gui-cs/Terminal.Gui).

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) (Targeting `net10.0`)

## Quick Start

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/L00p1nLup1n/Text-Roulette.git
    cd Text-Roulette
    ```

2.  **Run the game:**
    ```bash
    dotnet run
    ```

## How to Play

The game follows the standard Buckshot Roulette rules. You will take turns with another player (local/hotseat) to survive the rounds.

### Items
- **Beer:** Rack the slide (skip current shell).
- **Glass:** Peek at the current shell in the chamber.
- **Cigarette:** Regain health.
- **Saw:** Double the damage for the next shot.
- **Handcuffs:** Skip the opponent's next turn.

## Project Structure

- `code/`: Contains the core application logic.
  - `Controllers/`: Input handling and game flow control.
  - `Models/`: Game logic, player state, and items.
  - `Views/`: TUI layout and rendering using Terminal.Gui.
- `assets/`: ASCII art resources used for the visual interface.
