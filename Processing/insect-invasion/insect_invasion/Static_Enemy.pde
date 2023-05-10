class StaticEnemy extends MainEnemy {
  String direction = null;

  float originX, originY;
  /*
   * 2 argument constructor from the StaticEnemy class
   * @param Integer circleX
   * @param Integer circleY
   */
  //StaticEnemy(int circleX_, int circleY_) {
  //  this.circleX = circleX_;
  //  this.circleY = circleY_;
  //}


  void placeStaticEnemy(float xPos_, float yPos_) {
    enemyX = xPos_;
    enemyY = yPos_;
    originX = xPos_;
    originY = yPos_;
    isEnabled = true;
    direction = "north";

    if (TileEnemy(0, 1).type.equals("enemywalkable") || TileEnemy(0, 1).type.equals("door") || TileEnemy(0, 1).type.equals("doorOpen")) {
      direction = "south";
      rotation = PI;
    }    
    if (TileEnemy(0, -1).type.equals("enemywalkable") || TileEnemy(0, -1).type.equals("door") || TileEnemy(0, -1).type.equals("doorOpen")) {
      direction = "north";
      rotation = 0;
    }    
    if (TileEnemy(-1, 0).type.equals("enemywalkable") || TileEnemy(-1, 0).type.equals("door") || TileEnemy(-1, 0).type.equals("doorOpen")) {
      direction = "west";
      rotation = 1.5*PI;
    }    
    if (TileEnemy(1, 0).type.equals("enemywalkable") || TileEnemy(1, 0).type.equals("door") || TileEnemy(1, 0).type.equals("doorOpen")) {
      direction = "east";
      rotation = 0.5*PI;
    }
  }

  /*
   * Method to draw the static enemy
   * @return void
   */


  void draw() {
    //Draws a vision area of the static enemy
    //noFill();
    //noStroke();

    //variables for the vision area of thestatic enemy
    float visionW = 150;
    float visionH = 125;
    float visionX = 0;
    float visionY = 0;
    float visionRadius =  visionW/2;

    //switch for the vision area of the static enemy
    switch (direction) {

    case "south":
      visionY = 75;
      visionX = 0;
      visionW = 40;
      break;

    case "east":
      visionY = 0;
      visionX = 75;
      visionH = 40;
      break;

    case "north":
      visionY = -75;
      visionX = 0;
      visionW = 40;
      break;

    case "west":
      visionY = 0;
      visionX = -75;
      visionH = 40;
      break;
    }

    float PlayerToEnemy = dist(enemyX + visionX, enemyY + visionY, p.x, p.y);

    //checks if the player is in range of static enemy vision area
    //switch makes enemy move towards player direction
    if (PlayerToEnemy <= p.radius + visionRadius) {
      if (!TileEnemy(0, 0).type.equals("door")|| (TileEnemy(0, 0).type.equals("grass"))) {

        switch (direction) {

        case "south": 
          enemyY += speed; 
          break;

        case "east":
          enemyX += speed;
          break;

        case "north":
          enemyY -= speed;
          break;

        case "west":
          enemyX -= speed;
          break;
        }
      }
    } else if (TileEnemy(0, 0).type.equals("door") || (!(TileEnemy(0, 0).type.equals("grass")) || (TileEnemy(0, 0).type.equals("enemywalkable") && (!(PlayerToEnemy <= p.radius + visionRadius))))) {
      switch (direction) {

      case "south": 
        if (enemyY > originY) {
          enemyY -= speed;
        }
        break;

      case "east":
        if (enemyX>originX) {
          enemyX -= speed;
        }
        break;

      case "north":
        if (enemyY<originY) {
          enemyY += speed;
        }
        break;

      case "west":
        if (enemyX<originX) {
          enemyX += speed;
        }
        break;
      }
    }


    super.draw();//inherits everything inside the draw of all enemy classes
    for (int se = 0; se < staticEnemyCounter; se++) {
      if (staticEnemys[se].enemyX == staticEnemys[se].originX && staticEnemys[se].enemyY == staticEnemys[se].originY) {
        image(enemy, 0, 0, enemyDiameter, enemyDiameter);
      } else {
        image(enemies[(enemyFrame) % enemyFrames], 0, 0, enemyDiameter, enemyDiameter);
      }
    }
    popMatrix();
  }
}
