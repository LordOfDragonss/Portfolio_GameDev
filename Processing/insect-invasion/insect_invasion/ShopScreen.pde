class ShopScreen {

  //Variables for the size of the shop window
  float shopX, shopY;
  float shopW = 580; 
  float shopH = 600;

  //Variables for the size of the skin tabs
  float skinTabW = 140;
  float skinTabH = 200;

  //Variable used for alignment of the skin images
  float pictureAlignment = 60;

  //Upper bar of the shop window
  float upperShopBarX; 
  float upperShopBarY;
  float upperShopBarW = shopW;
  float upperShopBarH = 60;

  //Lower bar of the shop window
  float lowerShopBarX;
  float lowerShopBarY;
  float lowerShopBarW = shopW;
  float lowerShopBarH = 60;


  //Variable for space between the tabs and in the shop window.
  float space = 40;

  //Color variables for the shop window
  color shopColor = color(100, 5, 240);
  color textColor = color(255);

  //Multiple strings 
  float textSize = 30;
  String shopText = "Shop";
  String exitUItext = "Press      to exit";
  String buyUItext = "Press      to purchase";

  //Integer to keep track of the selected tab.ppp
  int selectedTab;

  //string with the name of the selected skin
  String selectedSkin ;

  //Making an array of skinTabAmount of skin tabs


  //Method to draw the entire shop;
  //The shop window and the skin tabs.
  void drawEntireShop() { 
    drawShop(width/4, height/8);

    //Every skin tab operates the selectSkinTab function; so it can be selected
    //Every skin tab gets drawn on the screen

    for (int i = 0; i < skinTabAmount; i++) {
      skinTabs[i].selectSkinTab();
      skinTabs[i].draw();
    }
    imageMode(CENTER);
    image(greenPlayer, shopX + space + skinTabW / 2, shopY + space + pictureAlignment, 80, 80);
    image(yellowPlayer, shopX + space + skinTabW / 2 + skinTabW + space, shopY + space + pictureAlignment, 80, 80);
    image(purplePlayer, shopX + space + skinTabW / 2 + skinTabW + space + skinTabW + space, shopY + space + pictureAlignment, 80, 80);
    image(lightBluePlayer, shopX + space + skinTabW / 2, shopY + space + skinTabH + space + pictureAlignment, 80, 80);
    image(grayPlayer, shopX + space + skinTabW / 2 + skinTabW + space, shopY + space + skinTabH + space + pictureAlignment, 80, 80);
    image(diamondPlayer, shopX + space + skinTabW / 2 + skinTabW + space + skinTabW + space, shopY + space + skinTabH + space + pictureAlignment, 80, 80);
    imageMode(CORNER);
  }

  //Method to setup the shop, place all the shop tabs in an array at a certain position
  //Two arguments to place the tabs relative to the shopX and shopY.
  void setupShop(float shopX_, float shopY_) {
    shopX = shopX_;
    shopY = shopY_;

    skinTabs[0] = new SkinTab(shopX + space, shopY + space, 100);
    skinTabs[1] = new SkinTab(shopX + space + skinTabW + space, shopY + space, 200);
    skinTabs[2] = new SkinTab(shopX + space + skinTabW + space + skinTabW + space, shopY + space, 350);
    skinTabs[3] = new SkinTab(shopX + space, shopY + space + skinTabH + space, 500);
    skinTabs[4] = new SkinTab(shopX + space + skinTabW + space, shopY + space + skinTabH + space, 750);
    skinTabs[5] = new SkinTab(shopX + space + skinTabW + space + skinTabW + space, shopY + space + skinTabH + space, 1000);
  }
  //Method where the shop gets drawn (not the skin tabs)
  //Two arguments to place the shop somewhere in the game screen. 
  void drawShop(float shopX_, float shopY_) {
    String playerInventory = "Inventory: " + collectedCoins + "x"; 

    shopX = shopX_;
    shopY = shopY_;

    upperShopBarX = shopX;
    upperShopBarY = shopY - 50;
    lowerShopBarX = shopX;
    lowerShopBarY = shopY + 550;

    textAlign(CENTER);

    //Upper bar of the shop, colored black.
    stroke(0);
    fill(0);
    rect(upperShopBarX, upperShopBarY, upperShopBarW, upperShopBarH); 
    textSize(textSize);
    fill(textColor);
    text(shopText, upperShopBarX + upperShopBarW/2, upperShopBarH + 20);


    //Middle portion of the shop
    fill(shopColor);
    rect(shopX, shopY, shopW, shopH);

    //Lower bar of the shop.
    textAlign(CENTER);
    fill(0);
    rect(lowerShopBarX, lowerShopBarY, lowerShopBarW, lowerShopBarH);
    fill(textColor);
    textSize(textSize);
    text(playerInventory, lowerShopBarX + lowerShopBarW/2, lowerShopBarY - 25);
    image(timeCoin, lowerShopBarX + lowerShopBarW/2 + 57, lowerShopBarY - 52);
    //User Interface text:
    //B to Exit, A to Purchase
    textAlign(CENTER, CENTER);
    text(exitUItext, lowerShopBarX + lowerShopBarW /4, lowerShopBarY + lowerShopBarH /2);
    image(bButton, lowerShopBarX + lowerShopBarW /4 - 22.5, lowerShopBarY + lowerShopBarH/3);

    text(buyUItext, lowerShopBarX + lowerShopBarW * 0.75, lowerShopBarY + lowerShopBarH/2);
    image(aButton, lowerShopBarX + lowerShopBarW * 0.60 + 44, lowerShopBarY + lowerShopBarH/3);
  }

  //Method to navigate within the shop window
  //Uses the variable selectedTab to assign a number to each shop tab.
  void keyPressed() {
    //A || LEFT on the controller
    if (keyCode == 65) {
      selectedTab--;
      if (selectedTab == 0) {
        selectedTab = 6;
      }
    }
    //D || RIGHT on the controller
    if (keyCode == 68) {
      selectedTab++;
      if (selectedTab == 7) {
        selectedTab = 1;
      }
    }
    // S || DOWN on the controller
    if (keyCode == 83 && selectedTab <= 3) {
      selectedTab += 3;
    }
    //W ||UP  on the controller
    if (keyCode == 87 && selectedTab >= 4) {
      selectedTab -= 3;
    }
    //R || A on the controller
    //Buys a skin
    //For every tab; if pressed, sets isBuyable on false
    if (keyCode == 82) {
      for (int i = 0; i < skinTabAmount; i++) {
        if (skinTabs[i].selected && skinTabs[i].isBuyable && skinTabs[i].skinCost < collectedCoins) {
          databasemanager.buySkin();
          selectedPlayer = selectedSkin;
          collectedCoins -= skinTabs[i].skinCost;
          skinTabs[i].isBuyable = false;
        }
        if (skinTabs[i].selected &&skinTabs[i].isBuyable == false) {
          selectedPlayer = selectedSkin;
        }
      }
    }
    //E || B on the controller
    //Exits the shop
    if (keyCode == 69) {
      stage = 3;
    }
  }


  //Nestled in class for the skin tabs.
}
