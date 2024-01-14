import java.io.IOException;
import java.util.Random;
import java.util.Scanner;

public class Shotgun extends Player {

    static Scanner scn = new Scanner(System.in);
    private Random rand = new Random();
    private boolean[] chambers; // false means blank, true is live
    private int bullets;
    private int liveRounds;
    private int blanks;
    private int chamberIndex = 0;
    private int firedRounds = 0;
    private int rounds;
    private int roundStartBlanks;
    private int roundStartLives;
    private int difficulty = 0;

    public int getBullets() {
        return bullets;
    }

    public void setBullets(int bullets) {
        this.bullets = bullets;
    }

    public void load(int bullets) {
        this.bullets = bullets;
        chambers = new boolean[bullets];
        blanks = 0;
        chamberIndex = 0;
        liveRounds = 0;
        for (int i = 0; i < bullets; i++) {
            int type = rand.nextInt(4 - difficulty);
            if (type == 0) {
                chambers[i] = true;
                liveRounds++;
            } else {
                chambers[i] = false;
                blanks++;
            }
        }
        if (liveRounds == 0 || blanks == 0) {
            load(bullets);
        }
        roundStartBlanks = this.blanks;
        roundStartLives = this.liveRounds;
    }

    public void getCurrentChamber() {
        System.out.println(roundStartBlanks + " blank(s)");
        System.out.println(roundStartLives + " live round(s)");
    }

    public Boolean fire() {
        Boolean fire = chambers[chamberIndex];
        chamberIndex++;
        return fire;
    }

    public String getTurn() {
        if (liveRounds >= 0) {
            return "//This is Player " + players[currentPlayer].playerID + " turn//";
        } else
            return "\n";
    }

    public void gameOverCheck() {
        try {
            if (players[currentPlayer].health <= 0) {
                throw new GameOver();
            }
        } catch (GameOver e) {
            System.out.println("Player " + (players[currentPlayer].playerID) + " suck big dick");
            System.out.println("//Live rounds fired: " + firedRounds + "//");
            System.out.println(
                    "//Player " + (players[otherPlayer()].playerID) + " survived until Round " + (rounds + 1) + "//");
            System.exit(0);
        }
        try {
            if (players[otherPlayer()].health <= 0) {
                throw new GameOver();
            }
        } catch (GameOver e) {
            System.out.println("//Player " + (players[otherPlayer()].playerID) + " suck big dick//");
            System.out.println("//Live rounds fired: " + firedRounds + "//");
            System.out.println(
                    "//Player " + (players[currentPlayer].playerID) + " survived until Round " + (rounds + 1) + "//");
            System.exit(0);
        }
    }

    public void viewItems() {
        if (players[currentPlayer].itemUse < 0) {
            players[currentPlayer].itemUse = 0;
        }
        System.out.println("//Player " + players[currentPlayer].playerID + " has:");
        System.out.println(players[currentPlayer].beer + " beer(s)");
        System.out.println(players[currentPlayer].handcuff + " handcuff(s)");
        System.out.println(players[currentPlayer].cig + " cig(s)");
        System.out.println(players[currentPlayer].saw + " saw(s)");
        System.out.println(players[currentPlayer].glass + " glass(es)//");
    }

    public void noItems() {
        System.out.println("//Player " + players[currentPlayer].playerID + " does not have that item//");
    }

    public Boolean itemCheck(int item) {
        if (item <= 0) {
            return false;
        } else {
            return true;
        }
    }

    public void shotgunBehavior() throws GameOver {
        while (this.chamberIndex <= this.bullets - 1) {
            if (liveRounds == 0) {
                break;
            }
            System.out.println(getTurn());
            System.out.print("Player " + players[currentPlayer].playerID + " input: ");
            String cmd = scn.nextLine();
            switch (cmd) {
                case "cmd": {
                    clearScreen();
                    viewCommands();
                    break;
                }
                case "chamber":
                    clearScreen();
                    getCurrentChamber();
                    break;
                case "me": // shoot yourself
                    clearScreen();
                    if (this.fire()) {
                        System.out.println("Bang!");
                        players[currentPlayer].health--;
                        liveRounds--;
                        switchPlayer();
                        firedRounds++;
                    } else {
                        System.out.println("Click!");
                        players[currentPlayer].doubleDamage = false;
                        blanks--;
                    }
                    gameOverCheck();
                    break;
                case "you": // shoot other player
                    clearScreen();
                    if (this.fire()) {
                        System.out.println("Bang!");
                        players[otherPlayer()].health--;
                        liveRounds--;
                        firedRounds++;
                    } else {
                        System.out.println("Click!");
                        blanks--;
                    }
                    gameOverCheck();
                    if (!players[otherPlayer()].handcuffed) {
                        switchPlayer();
                    } else {
                        players[otherPlayer()].handcuffed = false;
                    }
                    break;
            }
            getPlayerStatus();
        }
    }

    public void shotgunBehaviorWithItems() throws GameOver {
        while (this.chamberIndex <= this.bullets - 1) {
            if (liveRounds == 0) {
                break;
            }
            viewItems();
            System.out.println(getTurn());
            System.out.print("Player " + players[currentPlayer].playerID + " input: ");
            String cmd = scn.nextLine();
            switch (cmd) {
                case "cmd": {
                    clearScreen();
                    viewCommands();
                    break;
                }
                case "info":
                    clearScreen();
                    itemInfo();
                    break;
                case "chamber":
                    clearScreen();
                    getCurrentChamber();
                    break;
                case "me": // shoot yourself
                    clearScreen();
                    if (this.fire()) {
                        if (!players[currentPlayer].doubleDamage) {
                            System.out.println("Bang!");
                            players[currentPlayer].health--;
                        }
                        if (players[currentPlayer].doubleDamage) {
                            System.out.println("BANG!!!");
                            players[currentPlayer].doubleDamage = false;
                            players[currentPlayer].health -= 2;
                        }
                        liveRounds--;
                        if (!players[otherPlayer()].handcuffed) {
                            switchPlayer();
                        } else {
                            players[otherPlayer()].handcuffed = false;
                        }
                        firedRounds++;
                    } else {
                        clearScreen();
                        System.out.println("Click!");
                        players[currentPlayer].doubleDamage = false;
                        blanks--;
                    }
                    gameOverCheck();
                    getPlayerStatus();
                    break;
                case "you": // shoot other player
                    clearScreen();
                    if (this.fire()) {
                        if (!players[currentPlayer].doubleDamage) {
                            System.out.println("Bang!");
                            players[otherPlayer()].health--;
                        }
                        if (players[currentPlayer].doubleDamage) {
                            System.out.println("BANG!!!");
                            players[otherPlayer()].health -= 2;
                            players[currentPlayer].doubleDamage = false;
                        }
                        liveRounds--;
                        firedRounds++;

                    } else {
                        System.out.println("Click!");
                        players[currentPlayer].doubleDamage = false;
                        blanks--;
                    }
                    gameOverCheck();
                    getPlayerStatus();
                    if (!players[otherPlayer()].handcuffed) {
                        switchPlayer();
                    } else {
                        players[otherPlayer()].handcuffed = false;
                    }
                    break;
                case "beer": // rack a round
                    clearScreen();
                    if (itemCheck(players[currentPlayer].beer)) {
                        if (chambers[chamberIndex]) {
                            System.out.println("Ejected a live shell");
                            liveRounds--;
                        } else {
                            System.out.println("Ejected a blank shell");
                            blanks--;
                        }
                        chamberIndex++;
                        players[currentPlayer].beer--;
                    } else {
                        noItems();
                    }
                    break;
                case "glass": // view chamber
                    clearScreen();
                    if (itemCheck(players[currentPlayer].glass)) {
                        if (chambers[chamberIndex]) {
                            System.out.println("The shell in the chamber is live");
                        } else {
                            System.out.println("The shell in the chamber is blank");
                        }
                        players[currentPlayer].glass--;
                    } else {
                        noItems();
                    }
                    break;
                case "cig": // heal 1 health
                    clearScreen();
                    if (itemCheck(players[currentPlayer].cig)) {
                        System.out.println("Lung cancer may kill you, but a shotgun blast will");
                        players[currentPlayer].health++;
                        getPlayerStatus();
                        players[currentPlayer].cig--;
                    } else {
                        noItems();
                    }
                    break;
                case "saw": // gain double damage
                    clearScreen();
                    if (itemCheck(players[currentPlayer].saw)) {
                        players[currentPlayer].doubleDamage = true;
                        System.out.println("Shotgun is sawn-off, deal 2 damage for 1 turn");
                        players[currentPlayer].saw--;
                    } else {
                        noItems();
                    }
                    break;
                case "handcuff": // skip other player's turn
                    clearScreen();
                    if (itemCheck(players[currentPlayer].handcuff)) {
                        players[otherPlayer()].handcuffed = true;
                        System.out.println(
                                "Player " + players[otherPlayer()].playerID + " has been handcuffed, turn is skipped");
                        players[currentPlayer].handcuff--;
                    } else {
                        noItems();
                    }
                    break;
            }
        }
    }

    public void viewCommands() {
        System.out.println("//Type 'chamber' to view number of live and blank shells at round start//");
        System.out.println("//Type 'me' to shoot yourself//");
        System.out.println("//Type 'you' to shoot the other player//");
    }

    public static void welcomeText() {
        System.out.println("//Welcome to Text Roulette//");
        System.out.println("//This game is played with 2 players//");
        System.out.println("//There are blanks and live shells, loaded in random order//");
        System.out.println("//Type 'me' to shoot yourself//");
        System.out.println("//Type 'you' to shoot the other player//");
        System.out.println("//Shooting yourself with a blank will skip the other player's turn//");
        System.out.println("//Type 'chamber' to view number of live and blank shells at round start//");
        System.out.println("//Type 'cmd' to view list of basic commands//");
        System.out.println("//The first player will be Player 1//");
    }

    public void itemInfo() {
        System.out.println("//'beer': ejects a shell //");
        System.out.println("//'glass': views the current shell in the chamber//");
        System.out.println("//'cig': heals for 1 health//");
        System.out.println("//'saw': next shotgun blast will deal 2 damage//");
        System.out.println("//'handcuff': skips the other player turn//");
    }

    public void gameLogic() throws GameOver {
        for (rounds = 0; rounds >= 0; rounds++) {
            System.out.println("---ROUND " + (rounds + 1) + "---");
            if (rounds == 2) {
                System.out.println("//From this round, each player will receive 2 random items every round start//");
                System.out.println("//Items can be carried over to next round, up to 6//");
                System.out.println("//Type 'info' to view items' description again//");
                System.out.println("//Type the item's name to use//");
                System.out.println("//'beer': ejects a shell //");
                System.out.println("//'glass': views the current round in the chamber//");
                System.out.println("//'cig': heals for 1 health//");
                System.out.println("//'saw': next shotgun blast will deal 2 damage//");
                System.out.println("//'handcuff': skips the other player turn//");
            }
            setBullets(3 + rounds);
            if (getBullets() > 8) {
                setBullets(8);
                difficulty = 2;
            }
            load(getBullets());
            getPlayerStatus();
            getCurrentChamber();
            if (rounds < 2) {
                shotgunBehavior();
            }
            if (rounds >= 2) {
                for (int j = 0; j < 2; j++) {
                    if (players[0].itemUse >= 6) {
                        break;
                    }
                    generateItemsP1();
                }

                for (int j = 0; j < 2; j++) {
                    if (players[1].itemUse >= 6) {
                        break;
                    }
                    generateItemsP2();
                }
                shotgunBehaviorWithItems();
            }
        }
    }

    public void generateItemsP1() {
        int item1 = rand.nextInt(5);
        switch (item1) {
            case 0:
                players[0].beer++;
                break;
            case 1:
                players[0].glass++;
                break;
            case 2:
                players[0].cig++;
                break;
            case 3:
                players[0].saw++;
                break;
            case 4:
                players[0].handcuff++;
                break;
        }
    }

    public void generateItemsP2() {
        int item2 = rand.nextInt(5);
        switch (item2) {
            case 0:
                players[1].beer++;
                break;
            case 1:
                players[1].glass++;
                break;
            case 2:
                players[1].cig++;
                break;
            case 3:
                players[1].saw++;
                break;
            case 4:
                players[1].handcuff++;
                break;
        }
    }

    public static void clearScreen() {
        try {

            if (System.getProperty("os.name").contains("Windows"))
                new ProcessBuilder("cmd", "/c",
                        "cls").inheritIO().start().waitFor();
            else
                Runtime.getRuntime().exec("clear");
        } catch (IOException | InterruptedException ex) {
        }

    }

}
