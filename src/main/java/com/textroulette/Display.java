package com.textroulette;
import java.io.IOException;
import java.util.*;

public class Display {
    Scanner scn = new Scanner(System.in);

    String art1 = """
             ,________________________________
            |__________,----------._ [____]  //-,_______...-----===
                    (_(||||||||||||)___________/                   |
                       `----------' 	[ ))-                  |
                                                  `,  _,--..._____ |
                                                    `/
                        """;

    String shotgunSawnOff = """
            ,__________________________
            |___,----------._ [____]  //-,_______...-----===
             (_(||||||||||||)___________/                   |
                `----------' 	 [ ))-                  |
                                           `,  _,--..._____ |
                                             `/
                 """;

    String shotgunLive = """
             ,________________________________
            |__________,----------._ [Live]  //-,_______...-----===
                    (_(||||||||||||)___________/                   |
                       `----------' 	[ ))-                  |
                                                  `,  _,--..._____ |
                                                    `/
                        """;

    String sawnOffLive = """
            ,__________________________
            |___,----------._ [Live]  //-,_______...-----===
             (_(||||||||||||)___________/                   |
                `----------' 	 [ ))-                  |
                                           `,  _,--..._____ |
                                             `/
                 """;
    String shotgunBlank = """
             ,________________________________
            |__________,----------._ [Blnk]  //-,_______...-----===
                    (_(||||||||||||)___________/                   |
                       `----------' 	[ ))-                  |
                                                  `,  _,--..._____ |
                                                    `/
                        """;

    String sawnOffBlank = """
            ,__________________________
            |___,----------._ [Blnk]  //-,_______...-----===
             (_(||||||||||||)___________/                   |
                `----------' 	 [ ))-                  |
                                           `,  _,--..._____ |
                                             `/
                 """;

    String beer = """
             ___
            |   | 
            |333|
            |___|
            """;
    String handcuff = "()--()";
    String cig = """
                      (  )/  
                       )(/
    ________________  ( /)
   ()__)____________)))))    
            """;

    public void printMsg(String msg) {
        System.out.println(msg);
    }

    public void welcomeText() {
        String msg = "//Welcome to Text Roulette//\n//This game is played with 2 players//\n//There are blanks and live shells in the shotgun, loaded in random order//\n//Type 'y' to shoot the other player, 'm' to shoot yourself//\n//Shooting yourself with a blank will skip the other player's turn//\n//Type 'cmd' to view list of basic commands//\nEnter 1 to play basic mode, no items will be dropped (for players who is unfamiliar with the game)\nEnter 2 to play fun mode, where item are dropped (for pro players)\nEnter q to quit";
        printMsg(msg);
    }

    public String viewCommands() {
        return "//Type 'm' to shoot yourself//\n//Type 'y' to shoot the other player//";
    }

    public void printPlayerHealth(Player player) {
        System.out.print("Player " + player.playerID + ": ");
        for (int i = 0; i < player.getHealth(); i++) {
            System.out.print("|");
        }
    }

    public String itemInfo() {
        return "//To use an item, enter its first letter//\n//(b)eer: ejects a shell //\n//(g)lass: views the current shell in the chamber//\n//(c)igarette: heals for 1 health//\n//(s)aw: next shotgun blast will deal 2 damage/\n//(h)andcuff: Skips the other player's turn\n//Players are given 3 items each turn and can hold a maximum of 8 items total";
    }

    public void printPlayerTurn(int playerID) {
        System.out.println("Player " + playerID + "'s turn!");
    }

    public void printGameplay(GameLogic game) throws Exception {
        for (game.rounds = 0; game.rounds >= 0; game.rounds++) {
            game.shotgun.load();
            String msg = "// " + game.shotgun.liveRounds + " live(s), " + game.shotgun.blanks + " blank(s) //";
            while (!game.shotgun.chambers.isEmpty()) {
                System.out.println(msg);
                msg = "////";
                System.out.println(game.results);
                System.out.println(art1);
                System.out.println("---ROUND " + (game.rounds + 1) + "---");
                printPlayerHealth(game.players[0]);
                System.out.println("");
                printPlayerHealth(game.players[1]);
                System.out.println();
                System.out.println("-------------");
                System.out.print("Player " + game.players[game.currentPlayer].playerID + " input: ");
                String cmd = scn.next();
                game.gameplay(cmd);
                Display.clearScreen();
            }
            game.shotgun.difficulty++;
        }
    }

    public void printGameplayPro(GameLogic game) throws Exception {
        for (game.rounds = 0; game.rounds >= 0; game.rounds++) {
            game.shotgun.load();
            game.itemDrop();
            String msg = "//" + game.shotgun.liveRounds + " lives, " + game.shotgun.blanks + " blanks //";
            while (!game.shotgun.chambers.isEmpty()) {
                if (game.shotgun.liveRounds == 0 && game.shotgun.blanks == 2) {
                    break;
                }
                System.out.println(msg);
                msg = "////";
                System.out.println(game.results);
                if (game.shotgun.isSawnOff) {
                    switch (game.chamberResults) {
                        case ("Live") -> {
                            System.out.println(sawnOffLive);
                            game.chamberResults = "";

                        }
                        case ("Blank") -> {
                            System.out.println(sawnOffBlank);
                            game.chamberResults = "";
                        }
                        default -> {
                            System.out.println(shotgunSawnOff);
                        }
                    }
                } else {
                    switch (game.chamberResults) {
                        case ("Live") -> {
                            System.out.println(shotgunLive);
                            game.chamberResults = "";
                        }

                        case ("Blank") -> {
                            System.out.println(shotgunBlank);
                            game.chamberResults = "";
                        }
                        default -> {
                            System.out.println(art1);
                        }
                    }
                }
                System.out.println("---ROUND " + (game.rounds + 1) + "---");
                printPlayerHealth(game.players[0]);
                System.out.println("");
                printPlayerHealth(game.players[1]);
                System.out.println();
                System.out.println("-------------");
                game.viewItems();
                System.out.print("Player " + game.players[game.currentPlayer].playerID + " input: ");
                String cmd = scn.next();
                game.gameplayPro(cmd);
                Display.clearScreen();
            }
            game.shotgun.difficulty++;
        }

    }

    public static void clearScreen() {
        try {
            if (System.getProperty("os.name").contains("Windows"))
                new ProcessBuilder("cmd", "/c",
                        "cls").inheritIO().start().waitFor();
            else
                Runtime.getRuntime().exec("clear");
        } catch (IOException | InterruptedException ex) {
        }
    }
}
