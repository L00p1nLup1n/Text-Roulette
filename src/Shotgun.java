import java.util.Random;
import java.util.Scanner;

public class Shotgun extends Player {
    public Shotgun(int playerID) {
        super(playerID);
        // TODO Auto-generated constructor stub
    }

    static Scanner scn = new Scanner(System.in);
    private Random rand = new Random();
    private boolean[] chambers; // false means blank, true is live
    private int bullets;
    private int liveRounds;
    private int blanks;
    private int chamberIndex = 0;
    private int difficulty = 0;

    public int getBullets() {
        return bullets;
    }

    public void createPlayers() {
        players[0] = new Player(1);
        players[1] = new Player(2);
    }

    public void setBullets(int bullets) {
        this.bullets = bullets;
    }

    public void load(int bullets) {
        difficulty += 2;
        this.bullets = bullets;
        chambers = new boolean[bullets];
        blanks = 0;
        chamberIndex = 0;
        liveRounds = 0;
        for (int i = 0; i < bullets; i++) {
            int type = rand.nextInt(11 + difficulty);
            if (type % 3 == 0) {
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
    }

    public void getCurrentChamber() {
        System.out.println(this.blanks + " blank(s)");
        System.out.println(this.liveRounds + " live round(s)");
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
            System.exit(0);
        }
        try {
            if (players[otherPlayer()].health <= 0) {
                throw new GameOver();
            }
        } catch (GameOver e) {
            System.out.println("Player " + (players[otherPlayer()].playerID) + " suck big dick");
            System.exit(0);
        }
    }

    public void viewItems() {
        if (players[currentPlayer].itemUse < 0) {
            players[currentPlayer].itemUse = 0;
        }
        System.out.println(
                "Player " + players[currentPlayer].playerID + " has " + players[currentPlayer].itemUse
                        + " item(s)");
    }

    public void noItems() {
        System.out.println("Player " + players[currentPlayer].playerID + " is out of items");
    }

    public Boolean itemCheck() {
        if (players[currentPlayer].itemUse <= 0) {
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
                case "health":
                    clearScreen();
                    getPlayerStatus();
                    break;
                case "chamber":
                    clearScreen();
                    getCurrentChamber();
                    break;
                case "me": // shoot yourself
                    if (this.fire()) {
                        clearScreen();
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
                    if (this.fire()) {
                        clearScreen();
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

                    } else {
                        clearScreen();
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
            }
        }
    }

    public void shotgunBehaviorWithItems() throws GameOver {
        while (this.chamberIndex <= this.bullets - 1) {
            if (liveRounds == 0) {
                break;
            }
            System.out.println(getTurn());
            System.out.print("Player " + players[currentPlayer].playerID + " input: ");
            String cmd = scn.nextLine();
            switch (cmd) {
                case "items":
                    clearScreen();
                    viewItems();
                    break;
                case "health":
                    clearScreen();
                    getPlayerStatus();
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
                    if (this.fire()) {
                        clearScreen();
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

                    } else {
                        clearScreen();
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
                    players[currentPlayer].itemUse--;
                    if (itemCheck()) {
                        if (chambers[chamberIndex]) {
                            System.out.println("Ejected a live round");
                            liveRounds--;
                        } else {
                            System.out.println("Ejected a blank");
                            blanks--;
                        }
                        chamberIndex++;
                    } else {
                        noItems();
                    }
                    break;
                case "glass": // view chamber
                    clearScreen();
                    if (itemCheck()) {
                        if (chambers[chamberIndex]) {
                            System.out.println("The round in the chamber is live");
                        } else {
                            System.out.println("The round in the chamber is blank");
                        }
                    } else {
                        noItems();
                    }
                    players[currentPlayer].itemUse--;
                    break;
                case "cig": // heal 1 health
                    clearScreen();
                    if (itemCheck()) {
                        players[currentPlayer].health++;
                        getPlayerStatus();
                    } else {
                        noItems();
                    }
                    players[currentPlayer].itemUse--;
                    break;
                case "saw": // gain double damage
                    clearScreen();
                    if (itemCheck()) {
                        players[currentPlayer].doubleDamage = true;
                        System.out.println("Shotgun is sawn-off, deal 2 damage for 1 turn");
                    } else {
                        noItems();
                    }
                    players[currentPlayer].itemUse--;
                    break;
                case "handcuff": // skip other player's turn
                    clearScreen();
                    if (itemCheck()) {
                        players[otherPlayer()].handcuffed = true;
                        System.out.println(
                                "Player " + players[otherPlayer()].playerID + " has been handcuffed, turn is skipped");
                    } else {
                        noItems();
                    }
                    players[currentPlayer].itemUse--;
                    break;
            }
        }
    }

    public void welcomeText() {
        clearScreen();
        System.out.println("//Welcome to Text Roulette//");
        System.out.println("//This game is played with 2 players//");
        System.out.println("//There are blanks and live rounds, loaded in random order//");
        System.out.println("//Type 'me' to shoot yourself//");
        System.out.println("//Type 'you' to shoot the other player//");
        System.out.println("//Shooting yourself with a blank will skip the other player's turn//");
        System.out.println("//Type 'check' to view which rounds are left//");
        System.out.println("//Type 'health' to view both players' health//");
        System.out.println("//The first player will be Player 1//");
    }

    public void gameLogic() throws GameOver {
        for (int i = 0; i >= 0; i++) {

            System.out.println("---ROUND " + (i + 1) + "---");
            if (i == 2) {
                System.out.println("//From this round, each player will receive 1 item//");
                System.out.println("//Items can be carry over to next round, up to 4//");
                System.out.println("//Type 'item' to view how many items are left //");
                System.out.println("//Type the item's name to use//");
                System.out.println("//'beer': ejects a round //");
                System.out.println("//'glass': views the current round in the chamber//");
                System.out.println("//'cig': heals for 1 health//");
                System.out.println("//'saw': next shotgun blast will deal 2 damage//");
                System.out.println("//'handcuff': skips the other player turn//");
            }
            setBullets(2 + i);
            load(getBullets());
            getPlayerStatus();
            getCurrentChamber();
            if (i < 2) {
                shotgunBehavior();
            }
            if (i >= 2) {
                if (players[0].itemUse <= 4) {
                    if (players[0].itemUse < 0) {
                        players[0].itemUse = 0;
                    }
                    players[0].itemUse++;
                }
                if (players[1].itemUse <= 4) {
                    if (players[1].itemUse < 0) {
                        players[1].itemUse = 0;
                    }
                    players[1].itemUse++;
                }
                shotgunBehaviorWithItems();
            }
        }
    }

    public static void clearScreen() {
        System.out.print("\033[H\033[2J");
        System.out.flush();
    }
}
