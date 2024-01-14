public class Main {
    public static void main(String[] args) throws GameOver {
        Shotgun gun = new Shotgun(0);
        gun.welcomeText();
        gun.createPlayers();
        gun.gameLogic();
    }
}
