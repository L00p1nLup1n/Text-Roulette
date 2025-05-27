package com.textroulette;

public class Items {

    public String beer(Shotgun shotgun) {
        return shotgun.eject();
    }

    public String handcuff(Player player) {
        player.handcuffed = true;
        return "Player " + player.playerID + " is handcuffed, their turn is skipped!";
    }

    public String cig(Player player) {
        player.setHealth(player.getHealth() + 1);
        return "Smoking may kill you, but a shotgun will do it quicker.";
    }

    public String saw(Shotgun shotgun) {
        shotgun.isSawnOff = true;
        return "The barrel is sawn-off, dealing 2 damage for 1 turn!";
    }

    public String glass(Shotgun shotgun) {
        return shotgun.viewCurrentShell();
    }
}
