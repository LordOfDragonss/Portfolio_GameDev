class DatabaseManager {


  //shows the highscores
  void drawHighScores() {
    background(52, 190, 130);
    textSize(32);
    fill(208, 98, 36); 

    insertValues();
    showHighscores ();
  }

  //adds data to the database
  void insertValues() {
    if (online) {
      insertDeath();
      insertNewHighscore();
    }
  }

  //adds score to the highscore table
  void insertNewHighscore() {
    connection.updateQuery("INSERT INTO Highscore (Player_playerId, highscore) VALUES('"+ playerId + "', '"+ p.score +"');");
  }

  //adds the death position and cause to the deaths table
  void insertDeath() {
    connection.updateQuery("INSERT INTO Deaths (map, xPos, yPos, xTile, yTile, cause) VALUES('"+ currentMap +"', '"+ p.x +"', '"+p.y+"', '"+ p.playerTileX +"', '"+ p.playerTileY +"', '"+ deathCause +"');");
  }

  /*
  * Method to insert a new Achievement
   * @return void
   */
  void insertAchievement() {
    if(online)
    connection.updateQuery("INSERT INTO Player_has_Achievements (Player_playerId, Achievements_achievementName, is_achieved) VALUES (" + playerId + ", '" + lastAchievement + "' , '" + "true');" );
  }

  /*
  * Method to insert a new Player
   * @return void
   */
  void createNewPlayer() {
    if(online)
    connection.updateQuery("INSERT INTO Player (name) VALUES ('"+nameinput.name+"');");
  }

  /*
  * Method to get the latest player
   * @return void
   */
  void getLatestPlayer() {
    if(online){
    Table player = connection.runQuery("SELECT playerId FROM Player ORDER BY playerId  DESC LIMIT 1");
    TableRow row = player.getRow(0);
    playerId = int(row.getString(0));
    }
  }

  boolean isAchievementAchieved(String achievementName) {
    if(online){
    Table player_has_achievement = connection.runQuery("SELECT is_achieved FROM Player_has_Achievements WHERE Player_playerId = " + playerId + " AND Achievements_achievementName = '" + achievementName + "';");
    TableRow row = player_has_achievement.getRow(0);
    

    //array exception 
    try {
      boolean achieved = boolean(row.getString(0));
      return true;
    }
    catch (Exception E) {
      return false;
    }
    }
    return false;
  }


  //shows the highscores on the highscores screen
  void showHighscores() {

    textAlign(LEFT);
    text("NAME", xPositionName, yPosition);
    text("HIGHSCORE", xPositionScores, yPosition);
    text("________________________", xPositionName, yPosition + 10);
    if (online) {
      Table highscores = connection.runQuery("SELECT Player.name, Highscore.highscore FROM Player INNER JOIN Highscore ON Player.playerId = Highscore.Player_playerId ORDER BY highscore DESC LIMIT 0, 10;");
      for (int i = 0; i < highscores.getRowCount(); i++) {
        TableRow row = highscores.getRow(i);
        text(row.getString(0), xPositionName, yPosition + (i+1) * 50);
        text(row.getString(1), xPositionScores, yPosition + (i+1) * 50);
      }
    } else {

      image(noConnection, width/2-180, height/2-100, 240, 200);
    }
  }


  //shows the deathpoints on the map
  void showDeaths() {
    if (online) {
      Table deathLocation = connection.runQuery("SELECT xPos, yPos FROM Deaths WHERE map ="+ currentMap +" AND cause !='time';");

      for (int i=0; i<deathLocation.getRowCount(); i++) {
        TableRow row = deathLocation.getRow(i);
        int Xpos = row.getInt(0), Ypos = row.getInt(1);
        fill(255, 0, 0);
        circle(Xpos, Ypos, 10);
      }
    }
  }
  void retrieveSkinData() {
    if (online) {
      Table skinData = connection.runQuery("SELECT skinName, value FROM Skin ORDER BY value ASC");

      for (int i=0; i<skinData.getRowCount(); i++) {
        TableRow row = skinData.getRow(i);
      }
    }
  }
  void buySkin() {
    if (online) {
      connection.updateQuery("INSERT INTO Player_has_Skins (Player_playerId, Skin_skinName) VALUES('"+ playerId +"', '"+ shop.selectedSkin + "');");
    }
  }
  void seePlayerSkins() {
    if (online) {
      connection.updateQuery("SELECT Player.name, Skin.skinName FROM Player INNER JOIN Player_has_Skins ON Player_has_Skins.Player_playerId = Player.playerId INNER JOIN Skin ON Skin.skinName = Player_has_Skins.Skin_skinName ORDER BY Player.name ASC;");
    }
  }
}
