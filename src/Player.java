public class Player {
    int playerID;
    int health = 4;
    int currentPlayer;
    int itemUse = 0;
    Boolean doubleDamage = false;
    Boolean handcuffed = false;
    Player[] players = new Player[2];

    public Player(int playerID) {
        this.playerID = playerID;
    }

    public void getHealth() {
        System.out.print("Player " + playerID + ": ");
        for (int i = 0; i < health; i++) {
            System.out.print("|");
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

    public void getPlayerStatus() {
        players[0].getHealth();
        System.out.println();
        players[1].getHealth();
        System.out.println();
    }

}
