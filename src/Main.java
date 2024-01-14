public class Main {
    public static void main(String[] args) throws GameOver {
        Shotgun gun = new Shotgun();
        gun.welcomeText();
        gun.createPlayers();
        gun.gameLogic();
    }
}
