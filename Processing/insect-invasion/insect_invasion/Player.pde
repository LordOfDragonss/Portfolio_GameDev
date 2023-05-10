class Player
{
  float x, y, w, h; //x and y position, width and height from the player
  float speedX, speedY, maxSpeed; //Player movement speed
  float radius; //Player radius for the collisions
  float distance; //Distance used for the collision between player and coin
  int playerTileX, playerTileY; //Tile the player is located on
  int score; // Score (picked up coins)
  float rotation; //rotation of picture

  /*
   * No argument constructor for the Player class
   */
  Player()
  {
    //x = 400;
    //y = 145;
    x = width/2;
    y = height/2;
    w = 24;
    h = 24;
    maxSpeed = 2;
    speedX = 0;
    speedY = 0;
    radius = w / 2;
    score = 0;
  }

  /*
   * Method to update the player
   * @return void
   */
  void update()
  {
    //checking if the player wants to move left
    if (left)
    {
      // speedY = 0;     // commented so lateral movement works
      //seting the speed
      speedX = -maxSpeed;
      rotation = 1.5*PI;
    }

    //checking if the player wants to move right
    if (right)
    {
      // speedY = 0;     // commented so lateral movement works
      speedX = maxSpeed;
      rotation = 0.5*PI;
    }

    //checking if the player wants to move right and left, or neither of those
    if ((left == false && right == false) || (left && right))
    {
      speedX = 0;
    }

    //checking if the player wants to move up
    if (up)
    {
      // speedX = 0;       // commented so lateral movement works
      speedY = -maxSpeed;
      rotation = 0;
      if (right) {
        rotation = 0.25*PI;
      } else if (left) {
        rotation = 1.75*PI;
      }
    }

    //checking if the player wants to move down
    if (down) 
    {
      // speedX = 0;       // commented so lateral movement works
      speedY = maxSpeed;
      rotation = PI;
      if (right) {
        rotation = 0.75*PI;
      } else if (left) {
        rotation = 1.25*PI;
      }
    }

    //checking if the player wants to move up and down, or neither of those
    if ((up == false && down == false) || (up && down))
    {
      speedY = 0;
    }

    //updating the position
    y += speedY;       
    x += speedX;

    //setting the distance for the coin collision
    distance = radius + coins[0].coinRadius;
  }

  /*
   * Method to display the player
   * @return void
   */
  void display()
  {
    fill(0);
    //displaying the score text
    textAlign(LEFT);
    text("Score: " + score, 20, 700);

    fill(100, 100, 220);

    //   //displaying the player
    if (selectedPlayer == "player") {
      pushMatrix();
      imageMode(CENTER);
      translate(x, y);
      rotate(rotation);
      playerFrame = (playerFrame+1) % playerFrames;
      if (keyPressed && selectedPlayer == "player") {
        image(players[(playerFrame) % playerFrames], 0, 0, w, h);
      } else {
        image(player, 0, 0, w, h);
      }
      //   rotate(0);
      imageMode(CORNER);
      popMatrix();
    } else if (selectedPlayer == "playerGreen") {
      pushMatrix();
      imageMode(CENTER);
      translate(x, y);
      rotate(rotation);
      playerFrame = (playerFrame+1) % playerFrames;
      if (keyPressed) {
        image(greenPlayers[(playerFrame) % playerFrames], 0, 0, w, h);
      } else {
        image(greenPlayer, 0, 0, w, h);
      }
      //   rotate(0);
      imageMode(CORNER);
      popMatrix();
    } else if (selectedPlayer == "playerYellow") {
      pushMatrix();
      imageMode(CENTER);
      translate(x, y);
      rotate(rotation);
      playerFrame = (playerFrame+1) % playerFrames;
      if (keyPressed) {
        image(yellowPlayers[(playerFrame) % playerFrames], 0, 0, w, h);
      } else {
        image(yellowPlayer, 0, 0, w, h);
      }
      //   rotate(0);
      imageMode(CORNER);
      popMatrix();
    } else if (selectedPlayer == "playerPurple") {
      pushMatrix();
      imageMode(CENTER);
      translate(x, y);
      rotate(rotation);
      playerFrame = (playerFrame+1) % playerFrames;
      if (keyPressed) {
        image(purplePlayers[(playerFrame) % playerFrames], 0, 0, w, h);
      } else {
        image(purplePlayer, 0, 0, w, h);
      }
      //   rotate(0);
      imageMode(CORNER);
      popMatrix();
    } else if (selectedPlayer == "playerLightBlue") {
      pushMatrix();
      imageMode(CENTER);
      translate(x, y);
      rotate(rotation);
      playerFrame = (playerFrame+1) % playerFrames;
      if (keyPressed) {
        image(lightBluePlayers[(playerFrame) % playerFrames], 0, 0, w, h);
      } else {
        image(lightBluePlayer, 0, 0, w, h);
      }
      //   rotate(0);
      imageMode(CORNER);
      popMatrix();
    } else if (selectedPlayer == "playerGray") {
      pushMatrix();
      imageMode(CENTER);
      translate(x, y);
      rotate(rotation);
      playerFrame = (playerFrame+1) % playerFrames;
      if (keyPressed) {
        image(grayPlayers[(playerFrame) % playerFrames], 0, 0, w, h);
      } else {
        image(grayPlayer, 0, 0, w, h);
      }
      //   rotate(0);
      imageMode(CORNER);
      popMatrix();
    } else if (selectedPlayer == "playerDiamond") {
      pushMatrix();
      imageMode(CENTER);
      translate(x, y);
      rotate(rotation);
      playerFrame = (playerFrame+1) % playerFrames;
      if (keyPressed) {
        image(diamondPlayers[(playerFrame) % playerFrames], 0, 0, w, h);
      } else {
        image(diamondPlayer, 0, 0, w, h);
      }
      //   rotate(0);
      imageMode(CORNER);
      popMatrix();
    }
  }

  /*
   * Method to place the player
   * @param float x
   * @parm float y
   * @return void
   */
  void place(float x, float y) {
    this.x = x;
    this.y = y;
  }
}
