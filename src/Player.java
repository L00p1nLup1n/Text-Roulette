import java.util.Random;

public class Player {
    protected int beer = 0;
    protected int handcuff = 0;
    protected int cig = 0;
    protected int saw = 0;
    protected int glass = 0;
    protected int inventory;
    protected int itemUse = 0;
    protected int playerID;
    protected int health = 5;
    protected Boolean handcuffed = false;

    public int getHealth() {
        return this.health;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public void generateItems() {
        Random rand = new Random();
        for (int i = 0; i < 3; i++) {  
            if (inventory == 8) {
                break;
            }
            int item = rand.nextInt(5);
            switch (item) {
                case 0 -> beer++;
                case 1 -> glass++;
                case 2 -> cig++;
                case 3 -> saw++;
                case 4 -> handcuff++;
            }
            inventory = beer + cig + handcuff + saw + glass;
        }
    }

}