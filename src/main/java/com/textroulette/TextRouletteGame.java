package com.textroulette;
import java.util.Scanner;

public class TextRouletteGame {

    public static void main(String[] args) throws Exception {
        Display frame = new Display();
        @SuppressWarnings("resource")
        Scanner scanner = new Scanner(System.in);
        GameLogic gameLogic = new GameLogic();
        Display.clearScreen();
        frame.welcomeText();
        while (true) {
            String input = scanner.next();
            switch (input) {
                case ("1") -> {
                    Display.clearScreen();
                    gameLogic.createPlayers();
                    frame.printGameplay(gameLogic);
                    break;
                }
                case ("2") -> {
                    Display.clearScreen();
                    gameLogic.createPlayers();
                    frame.printGameplayPro(gameLogic);
                    break;
                }
                case ("q") -> {
                    return;
                }
                default -> {
                    Display.clearScreen();
                    frame.welcomeText();
                    System.out.println("Invalid input!");
                    break;
                }
            }
        }
    }
}
