class NameInput {

  final int LETTERCOUNT = 7; // int to determine the amount of letters you can input

  int[] counters = new int[LETTERCOUNT];//the counters that determine the number for the letter/sign

  String[] Letters = new String[LETTERCOUNT];//saving the letters that are inputted

  int textSize = 100;//the size of the text in the input
  int offset = 50;//the offset of line underneath the letters to align them

  int selected = 0;// the selected letter

  String name;//string to save the letters together

  void draw() {
    background(100, 200, 100);//standard green background

    for (int i= 0; i < LETTERCOUNT; i++) {
      Letters[i] = Getletter(counters[i]);//gets the letter based on the number inside the counter
    }
    drawText();//going to the text drawing function

    name = join(Letters, ""); //saves the letters together in one string

    rect(selected*textSize-offset+width/4, height/2, textSize, 10);//line underneath to show what letter you got selected

    textAlign(CENTER); 
    textSize(36); 
    fill(#FFFFFF); 
    text("press      to view highscores", screenSizeX / 2, screenSizeY / 2 + 250); 
    image(xButton, screenSizeX/2-3*w + 37, screenSizeY/2 + 223); 
    text("press      to continue", screenSizeX / 2, screenSizeY / 2 + 325); 
    image(aButton, screenSizeX/2-w-6, screenSizeY/2 + 298);
  }

  void drawText() {
    //setting the text up
    textSize(textSize);
    textAlign(CENTER);

    for (int i = 0; i < LETTERCOUNT; i++) {
      if (Letters[i] != null) {
        image(upArrow, width/4+textSize*i-offset, height/4, 80, 80);
        text(Letters[i], width/4+textSize*i, height/2);//places the letters next to eachother
        image(downArrow, width/4+textSize*i-offset, height/4*2.2, 80, 80);
      }
    }
  }


  void keyPressed() {
    if (keyCode == 87)//the W key
    {
      if (!(selected >= LETTERCOUNT)) {
        counters[selected]++;//increases the number of the counter to change the letter
      }
    } else if (keyCode == 83)// the S key
    {
      if (!(counters[selected] <= 0)) {
        counters[selected]--;//lowers the number of the counter to change the letter
      } else if (counters[selected] == 0) {
        counters[selected] = 26; //once you reach 0 going to the last letter
      }
    } else if (keyCode == 65) // the A key
    {
      if (!(selected < 0)) {
        selected--;//moves the selected letter to the left
      }
    } else if (keyCode == 68)// the D key
    {
      selected++;//moves the selected letter to the right
    }


    for (int i = 0; i < LETTERCOUNT; i++) {
      if (counters[i] >= 27) {
        counters[i] = 0;// brings you back to the first letter
      }
    }
    if (selected >= 7 || selected < 0) {
      selected = 0; // brings you back to the first selected letter
    }
  }


  //returning the letter as string back based on the number
  String Getletter(int number) {

    switch(number) {
    case 0:
      return "A";
    case 1:
      return "B";
    case 2:
      return "C";
    case 3:
      return "D";
    case 4:
      return "E";
    case 5:
      return "F";
    case 6:
      return "G";
    case 7:
      return "H";
    case 8:
      return "I";
    case 9:
      return "J";
    case 10:
      return "K";
    case 11:
      return "L";
    case 12:
      return "M";
    case 13:
      return "N";
    case 14:
      return "O";
    case 15:
      return "P";
    case 16:
      return "Q";
    case 17:
      return "R";
    case 18:
      return "S";
    case 19:
      return "T";
    case 20:
      return "U";
    case 21:
      return "V";
    case 22:
      return "W";
    case 23:
      return "X";
    case 24:
      return "Y";
    case 25:
      return "Z";
    case 26:
      return " ";
    }
    return null;
  }
}
