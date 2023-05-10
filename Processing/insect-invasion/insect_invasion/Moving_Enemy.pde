class MovingEnemy extends MainEnemy {

  String direction = "north"; // setting the moving enemy's moving direction

  /*
   * Constructor for the MovingEnemy class
   * @param Integer xspeed
   * @param Integer yspeed
   * @param Integer circleX
   * @param Integer circleY
   * @param Boolean isEnabled
   * @param Integer level
   */
  MovingEnemy(int xSpeed_, int ySpeed_) {
    this.enemyvx = xSpeed_;
    this.enemyvy = ySpeed_;
  }

  void placeMovingEnemy(float xPos_, float yPos_) {
    enemyX = xPos_;
    enemyY = yPos_;
    isEnabled = true;
  }


  /*
   * Method to draw the moving enemy
   * @return void
   */
  void draw() {
    //updating the moving enemy
    update();

    //inherits everything inside the draw of all enemy classes

    super.draw();
    image(enemies[(enemyFrame) % enemyFrames], 0, 0, enemyDiameter, enemyDiameter);
    popMatrix();
  }

  /*
   * Method to update the moving enemy
   * @return void
   */
  void update() {
    //assigns the tile where the enemy is standing on to a variabele.
    Tile tileStanding = TileEnemy(0, 0);

    //Checks if the enemy is standing in the middle of a tile.
    if ((direction == "north" && tileStanding.y + 0.5 * tileStanding.h  >= enemyY) || (direction == "south" && tileStanding.y + 0.5 * tileStanding.w  <= enemyY) ||
      (direction == "west" && tileStanding.x + 0.5 * tileStanding.w >= enemyX) || (direction == "east" && tileStanding.x + 0.5 * tileStanding.w <= enemyX) ) {
      //if the enemy is standing on a tile it checks the tiles around him to see which way he has to go.
      pathcheck(tileStanding);
    }
    enemyX = enemyX + enemyvx;
    enemyY = enemyY + enemyvy;
  }


  /*
   * Method to check the path for the enemy
   * @return void
   */
  void pathcheck(Tile tileStanding) {

    Tile frontTile = null;
    Tile leftTile = null;
    Tile rightTile = null;
    Tile backTile = null;

    //Checks which tiles are around this enemy
    switch(direction) {
    case "north":

      frontTile = TileEnemy(0, -1);
      leftTile = TileEnemy(-1, 0);
      rightTile = TileEnemy(1, 0);
      backTile = TileEnemy(0, 1);

      break;
    case "south":
      frontTile = TileEnemy(0, 1);
      leftTile = TileEnemy(1, 0);
      rightTile = TileEnemy(-1, 0);
      backTile = TileEnemy(0, -1);
      break;

    case "east" :
      frontTile = TileEnemy(1, 0);
      leftTile = TileEnemy(0, -1);
      rightTile = TileEnemy(0, 1);
      backTile = TileEnemy(-1, 0);
      break;

    case "west" :
      frontTile = TileEnemy(-1, 0);
      leftTile = TileEnemy(0, 1);
      rightTile = TileEnemy(0, -1);
      backTile = TileEnemy(1, 0);
      break;
    }
    //checks if the enemy can continue walking forward
    if ((frontTile.type.equals("enemywalkable") || frontTile.type.equals("doorOpen") || frontTile.type.equals("button") || frontTile.type.equals("windStop") 
      || frontTile.type.equals("enemyOneWay")||(frontTile.type.equals("oneWay")&&frontTile.direction.equals(direction))) == false) {
      enemyX = int(tileStanding.x + 0.5*tileStanding.w);
      enemyY = int(tileStanding.y + 0.5*tileStanding.w);
      //checks if the enemy can go right and if there is no open door on the left
      if ((((rightTile.type.equals("enemywalkable") || rightTile.type.equals("button")|| frontTile.type.equals("windStop") ||
        (rightTile.type.equals("oneWay")&&((direction == "north" && rightTile.direction.equals("east"))||(direction == "east" && rightTile.direction.equals("south"))
        ||(direction == "south" && rightTile.direction.equals("west"))||(direction == "west" && rightTile.direction.equals("north")))))
        && !leftTile.type.equals("doorOpen")) || rightTile.type.equals("doorOpen"))) {

        switch(direction) {
        case "north":
          enemyvx = speed;
          enemyvy = 0;
          direction = "east";
          rotation = 0.5*PI;
          break;

        case "east":
          enemyvx = 0;
          enemyvy = speed;
          direction = "south";
          rotation = PI;
          break;

        case "south":
          enemyvx = -speed;
          enemyvy = 0;
          direction = "west";
          rotation = 1.5*PI;
          break;

        case "west":
          enemyvx = 0;
          enemyvy = -speed;
          direction = "north";
          rotation = 0;
          break;
        }
      } else if (leftTile.type.equals("enemywalkable") || leftTile.type.equals("doorOpen")|| leftTile.type.equals("button")|| frontTile.type.equals("windStop") ||
        (leftTile.type.equals("oneWay")&&((direction == "north" && leftTile.direction.equals("west"))||(direction == "east" && leftTile.direction.equals("north"))
        ||(direction == "south" && leftTile.direction.equals("east"))||(direction == "west" && leftTile.direction.equals("south"))))) {
        switch(direction) {

        case "south":
          enemyvx = speed;
          enemyvy = 0;
          direction = "east";
          rotation = 0.5*PI;
          break;

        case "west":
          enemyvx = 0;
          enemyvy = speed;
          direction = "south";
          rotation = PI;
          break;

        case "north":
          enemyvx = -speed;
          enemyvy = 0;
          direction = "west";
          rotation = 1.5*PI;
          break;

        case "east":
          enemyvx = 0;
          enemyvy = -speed;
          direction = "north";
          rotation = 0;
          break;
        }
      } else { 
        switch(direction) {
        case "north":
          enemyvx = 0;
          enemyvy = speed;
          direction = "south";
          rotation = PI;
          break;

        case "east":
          enemyvx = -speed;
          enemyvy = 0;
          direction = "west";
          rotation = 1.5*PI;
          break;

        case "south":
          enemyvx = 0;
          enemyvy = -speed;
          direction = "north";
          rotation = 0;
          break;

        case "west":
          enemyvx = speed;
          enemyvy = 0;
          direction = "east";
          rotation = 0.5*PI;
          break;
        }
      }
    }
  }
}
