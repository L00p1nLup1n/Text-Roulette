public class Player {
    protected int beer = 0;
    protected int handcuff = 0;
    protected int cig = 0;
    protected int saw = 0;
    protected int glass = 0;
    protected int itemUse = beer + cig + handcuff + saw + glass;
    protected int playerID;
    protected int health = 10;
    protected int currentPlayer;
    protected Boolean doubleDamage = false;
    protected Boolean handcuffed = false;
    protected Player[] players = new Player[2];

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
