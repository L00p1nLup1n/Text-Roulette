public class Main {
    public static void main(String[] args) throws GameOver {
        Shotgun gun = new Shotgun();
        Shotgun.welcomeText();
        gun.createPlayers();
        Shotgun.clearScreen();
        Shotgun.welcomeText();
        gun.gameLogic();
    }
}
