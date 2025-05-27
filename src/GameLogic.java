import java.util.*;

public class GameLogic {
    static Scanner scn = new Scanner(System.in);
    public Player player = new Player();
    public Player[] players = new Player[2];
    public int currentPlayer = 0;
    public int rounds;
    String results = "";
    String chamberResults = "";

    public Display display = new Display();
    public Shotgun shotgun = new Shotgun();

    public void createPlayers() {
        players[0] = new Player();
        players[0].playerID = 1;
        players[1] = new Player();
        players[1].playerID = 2;
    }

    public void gameOverCheck() {
        try {
            if (players[currentPlayer].health <= 0) {
                throw new GameOver();
            }
        } catch (GameOver e) {
            Display.clearScreen();
            System.out.println("Player " + (players[currentPlayer].playerID) + " loses");
            System.out.println("//Live rounds fired: " + shotgun.firedRounds + "//");
            System.out.println(
                    "//Player " + (players[otherPlayer()].playerID) + " survived until Round " + (rounds + 1) + "//");
            System.out.println("Player 1 has used " + players[0].itemUse + " item(s)");
            System.out.println("Player 2 has used " + players[1].itemUse + " item(s)");
            System.exit(0);
        }
        try {
            if (players[otherPlayer()].health <= 0) {
                throw new GameOver();
            }
        } catch (GameOver e) {
            Display.clearScreen();
            System.out.println("//Player " + (players[otherPlayer()].playerID) + " loses//");
            System.out.println("//Live rounds fired: " + shotgun.firedRounds + "//");
            System.out.println(
                    "//Player " + (players[currentPlayer].playerID) + " survived until Round " + (rounds + 1) + "//");
            System.out.println("Player 1 has used " + players[0].itemUse + " item(s)");
            System.out.println("Player 2 has used " + players[1].itemUse + " item(s)");
            System.exit(0);
        }
    }

    public void gameplay(String cmd) throws GameOver { // Each turn handling basic
        switch (cmd) {
            case "cmd" -> {
                results = display.viewCommands();
            }
            case "m" -> {
                // shoot yourself
                results = shotgun.fire(players[currentPlayer]);
                if (shotgun.isLiveFired) {
                    switchPlayer();
                }
                gameOverCheck();
            }
            case "y" -> {
                // shoot other player
                results = shotgun.fire(players[otherPlayer()]);
                switchPlayer();
                gameOverCheck();
            }
            default -> {
                results = "Invalid input, please try again or use cmd to view commands if you are unsure";
            }
        }
    }

    public void gameplayPro(String cmd) throws GameOver { // Each turn handling pro
        Items items = new Items();
        switch (cmd) {
            case "cmd" -> {
                results = display.viewCommands();
            }
            case "m" -> {
                // shoot yourself
                results = shotgun.fire(players[currentPlayer]);
                if (shotgun.isLiveFired) {
                    switchPlayer();
                }
                gameOverCheck();
            }
            case "y" -> {
                // shoot other player
                results = shotgun.fire(players[otherPlayer()]);
                if (!players[otherPlayer()].handcuffed) {
                    switchPlayer();
                } else {
                    players[otherPlayer()].handcuffed = false;
                }
                gameOverCheck();
            }
            case "info" -> {
                results = display.itemInfo();
            }
            case "b" -> {
                if (!itemCheck(players[currentPlayer].beer)) {
                    results = noItems();
                } else {
                    players[currentPlayer].beer--;
                    players[currentPlayer].itemUse++;
                    results = display.beer + "\n" + items.beer(shotgun);
                }
            }
            case "h" -> {
                if (!itemCheck(players[currentPlayer].handcuff)) {
                    results = noItems();
                } else {
                    players[currentPlayer].handcuff--;
                    players[currentPlayer].itemUse++;
                    results = display.handcuff + "\n" + items.handcuff(players[otherPlayer()]);

                }
            }
            case "c" -> {
                if (!itemCheck(players[currentPlayer].cig)) {
                    results = noItems();
                } else {
                    players[currentPlayer].cig--;
                    players[currentPlayer].itemUse++;
                    results = display.cig + "\n" + items.cig(players[currentPlayer]);
                }
            }
            case "s" -> {
                if (!itemCheck(players[currentPlayer].saw)) {
                    results = noItems();
                } else {
                    players[currentPlayer].saw--;
                    players[currentPlayer].itemUse++;
                    results = items.saw(shotgun);
                }
            }
            case "g" -> {
                if (!itemCheck(players[currentPlayer].glass)) {
                    results = noItems();
                } else {
                    players[currentPlayer].glass--;
                    players[currentPlayer].itemUse++;
                    results = "Player " + players[currentPlayer].playerID + " managed to peek inside the chamber...";
                    chamberResults = items.glass(shotgun);
                }
            }
            default -> {
                results = "Invalid input, please try again or use cmd to view commands if you are unsure";
            }

        }
    }

    public int otherPlayer() {
        if (currentPlayer == 1) {
            return 0;
        } else {
            return 1;
        }
    }

    public void switchPlayer() {
        if (currentPlayer == 0) {
            currentPlayer = 1;
        } else if (currentPlayer == 1) {
            currentPlayer = 0;
        }
    }

    public void itemDrop() {
        for (Player i : players) {
            i.generateItems();
        }
    }

    public void viewItems() {
        System.out.println("//Player " + players[currentPlayer].playerID + " has:");
        System.out.println(players[currentPlayer].beer + " beer(s)");
        System.out.println(players[currentPlayer].handcuff + " handcuff(s)");
        System.out.println(players[currentPlayer].cig + " cig(s)");
        System.out.println(players[currentPlayer].saw + " saw(s)");
        System.out.println(players[currentPlayer].glass + " glass(es)//");
    }

    public String noItems() {
        return "Player " + players[currentPlayer].playerID + " does not have that item";
    }

    public Boolean itemCheck(int item) {
        return (item > 0);
    }
}
