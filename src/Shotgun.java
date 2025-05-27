import java.util.*;

public class Shotgun {

    private final Random rand = new Random();
    public Stack<Boolean> chambers = new Stack<>(); // false means blank, true is live
    public int bullets = 0;
    public int liveRounds;
    public int blanks;
    public int firedRounds = 0;
    public int difficulty = 0;
    public Boolean isSawnOff = false;
    public Boolean isLiveFired;

    public int getBullets() {
        return bullets;
    }

    public void setBullets(int bullets) {
        this.bullets = bullets;
    }

    public void load() {
        blanks = 0;
        liveRounds = 0;
        chambers.push(true);
        liveRounds++;
        chambers.push(false);
        blanks++;
        for (int i = 0; i < difficulty; i++) {
            if (difficulty > 5) {
                difficulty = 5;
            }
            int type = rand.nextInt(100);
            if (type < 30) { // adjust odds for live round
                chambers.push(true);
                liveRounds++;
            } else {
                chambers.push(false);
                blanks++;
            }
        }
        ArrayList<Boolean> temp = new ArrayList<>(chambers);
        Collections.shuffle(temp);
        chambers.clear();
        chambers.addAll(temp);
    }

    public String fire(Player target) {
        Boolean result = chambers.pop();
        if (result && isSawnOff) {
            firedRounds++;
            liveRounds--;
            isLiveFired = true;
            target.setHealth(target.getHealth() - 2);
            isSawnOff = false;
            return "*BANG!!!";
        } else if (result && !isSawnOff) {
            firedRounds++;
            liveRounds--;
            isLiveFired = true;
            target.setHealth(target.getHealth() - 1);
            isSawnOff = false;
            return "*Bang!";
        } else {
            isLiveFired = false;
            blanks--;
            isSawnOff = false;
            return "*Click!";
        }
    }

    public String viewCurrentShell() {
        if (chambers.peek()) {
            return "Live";
        } else {
            return "Blank";
        }
    }

    public String eject() {
        if (chambers.peek()) {
            chambers.pop();
            liveRounds--;
            return "Ejects a live round!";
        } else {
            chambers.pop();
            blanks--;
            return "Ejects a blank!";
        }
    }

    public Boolean isEmpty() {
        return chambers.isEmpty();
    }
}
