class SkinTab {

  //Variables for the size of the skin tabs
  float skinTabW = 140;
  float skinTabH = 200;
  //Variables for the size of the skin tabs
  float x, y;
  float w = 140;
  float h = 200;
  float lineOffset = 160;

  //Colors for the tabs, and for the displayed skin.
  color skinTabColor = color(75, 25, 150);
  color purchaseableColor = color(100);
  color tabStroke = color(0); 
  color circleStroke = color(0);

  //Variables to change the stroke and text color when a tab is selected.
  color selectedColor = color(200, 200, 200);
  color buyColor = color(255);
  color bought = color(255, 0, 0);

  //String to display the text on the tab
  String buyText = "Buy: ";

  //Variable to declare the cost of the skins
  int skinCost; 

  //Boolean to determine if a skin tab is selected.
  boolean selected;
  
  //Boolean to see if a skin is purchaseable
  boolean isBuyable; 

  //Constructor for the skin tab
  //Two arguments to place the tabs somewhere.
  SkinTab(float xPos_, float yPos_, int skinCost_) {
    x = xPos_;
    y = yPos_;
    selected = false;
    isBuyable = true;
    skinCost = skinCost_;

    textAlign(CENTER, BOTTOM);
  }

  //Draw function for the tabs
  void draw() {

    //Making the skin tab. 

    for (int i = 0; i <skinTabAmount; i++) {
      if (skinTabs[i].isBuyable == false) {
        skinTabs[i].buyColor = color(bought);
      }
    }
    stroke(tabStroke);
    fill(skinTabColor);
    rect(x, y, w, h);
    stroke(0);
    line(x, y + lineOffset, x +skinTabW, y +lineOffset);

    //Circle that displays the purchaseable skin
    //------------------------------------------------------CHANGE TO IMAGE-----------------------------------------//
    stroke(circleStroke);
    fill(purchaseableColor);
    circle(x + w /2, y + h /2 - 40, 100 );

    //Text in each tab
    textAlign(CENTER, CENTER);
    fill(buyColor);
    textSize(25);
    text(buyText, x + skinTabW/2 - 15, y + skinTabH - 20);
    text(skinCost + "x", x + skinTabW/1.8, y + skinTabH - 20);
    image(timeCoin, x + skinTabW/1.5, y + skinTabH - 36);
  }

  //Method to select different skin tabs
  void selectSkinTab() {
    shop.selectedTab = constrain(shop.selectedTab, 1, 6);

    //If a tab is selected, change the color of the stroke and the text of that tab
    if (selected == true) {
      tabStroke = color(selectedColor);
      buyColor = selectedColor;
      circleStroke = selectedColor;
    } else { //Otherwise, give them the default colors
      tabStroke = color(0); 
      buyColor = color(255);
      circleStroke = color(0);
    }

    //Connects a skin tab to a number of the 'selectedTab' variable
    //Sets the boolean 'selected' to true if they are selected, and false if they are not selected
    if (shop.selectedTab == 1) {
      skinTabs[0].selected = true;
      shop.selectedSkin = "playerGreen";
    } else if (shop.selectedTab != 1) {
      skinTabs[0].selected = false;
    }
    if (shop.selectedTab == 2) {
      skinTabs[1].selected = true;
      shop.selectedSkin = "playerYellow";
    } else if (shop.selectedTab != 2) {
      skinTabs[1].selected = false;
    }
    if (shop.selectedTab == 3) {
      skinTabs[2].selected = true;
      shop.selectedSkin = "playerPurple";
    } else if (shop.selectedTab != 3) {
      skinTabs[2].selected = false;
    }
    if (shop.selectedTab == 4) {
      skinTabs[3].selected = true;
      shop.selectedSkin = "playerLightBlue";
    } else if (shop.selectedTab != 4) {
      skinTabs[3].selected = false;
    }
    if (shop.selectedTab == 5) {
      skinTabs[4].selected = true;
      shop.selectedSkin = "playerGray";
    } else if (shop.selectedTab != 5) {
      skinTabs[4].selected = false;
    }
    if (shop.selectedTab == 6) {
      skinTabs[5].selected = true;
      shop.selectedSkin = "playerDiamond";
    } else if (shop.selectedTab != 6) {
      skinTabs[5].selected = false;
    }
  }
}
