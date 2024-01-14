public class Main {
    public static void main(String[] args) throws GameOver {
        Shotgun gun = new Shotgun();
        gun.createPlayers();
        Shotgun.clearScreen();
        Shotgun.welcomeText();
        gun.gameLogic();
    }
}
