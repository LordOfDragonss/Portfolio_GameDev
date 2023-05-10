class Timer {
  float time = 45000; //Amount of starting time in ms
  float maxTime = time; //Max amount of time in ms
  int timerMaxWidth = 1280; //Max width timer
  int timerHeight = 25; //Height timer
  int timeBarX = 0; //Timer position X
  int timeBarY = 0; //Timer position Y
  int nowTime; //Time passed since starting the game
  int lastTime; //Last time the amount of time decreased
  int extraTime = 5000; //Amount of seconds added when grabbing a coin in MS
  float timerWidth;

  //draws the timer
  void drawTimer()
  {
    fill(0);
    rect(timeBarX, timeBarY, width, timerHeight);
    float greenColor = time / maxTime *255 *2; //The timer transitions from green to red as time decreases
    float redColor = 255 *2 - time / maxTime * 255 *2;
    float blueColor = 0;
    fill(redColor, greenColor, blueColor);

    noStroke();
    timerWidth = (time / maxTime) * timerMaxWidth;  //Width of the drawn timer
    if (time > maxTime) {
      time = maxTime; //Prevents the value of time exceeding the maximum
      timerWidth = timerMaxWidth; //Prevents the timer from being drawn wider than the maximum width
    }
    nowTime = millis(); //Decreasign the value of time
    time -= (nowTime - lastTime);
    if (achievementDisplayTime > 0) {
      achievementDisplayTime -= (nowTime - lastTime);
    }

    lastTime = nowTime;

    rect(timeBarX, timeBarY, timerWidth, timerHeight); //Drawing the timer

    if (time<0) { //If time goes below zero, its gameover and the cause of death gets set to time for the purpose of analytics.
      deathCause = "time";
      stage = 4;
    }
  }
  void resetTimer() {
    time = maxTime;
    lastTime = millis();
  }
}
