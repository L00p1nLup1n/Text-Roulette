public class Player {
    int playerID;
    int health = 10;
    int beer = 0;
    int handcuff = 0;
    int cig = 0;
    int saw = 0;
    int glass = 0;
    int itemUse = beer + cig + handcuff + saw + glass;
    int currentPlayer = 0;
    Boolean doubleDamage = false;
    Boolean handcuffed = false;
    Player[] players = new Player[2];

    public void createPlayers() {
        players[0] = new Player();
        players[0].playerID = 1;
        players[1] = new Player();
        players[1].playerID = 2;
    }

    public void getHealth() {
        System.out.print("Player " + playerID + ": ");
        for (int i = 0; i < health; i++) {
            System.out.print("|");
        }
    }

    public void getPlayerStatus() {
        players[0].getHealth();
        System.out.println();
        players[1].getHealth();
        System.out.println();
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
}
