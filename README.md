# **Tempo Artist**
# *Videogame for Percussive Arts Society*

## Game design document

---


##
## Index

---

1. [Game Design](#game-design)
    1. [Summary](#summary)
    2. [Gameplay](#gameplay)
    3. [Mindset](#mindset)
3. [Technical](#technical)
    1. [Screens](#screens)
    2. [Controls](#controls)
    3. [Mechanics](#mechanics)
4. [Development](#development)
    1. [Abstract Classes](#abstract-classes--components)
    2. [Derived Classes](#derived-classes--component-compositions)
5. [Graphics](#graphics)
    1. [Style Attributes](#style-attributes)
    2. [Graphics Needed](#graphics-needed)
6. [Sounds/Music](#soundsmusic)
    1. [Style Attributes](#style-attributes)
    2. [Sounds Needed](#sounds-needed)
    3. [Music Needed](#music-needed)
7. [Schedule](#schedule)

## Game design

---

### **Summary**

A simple yet challenging game whose objective is to help players to learn percusion tab reading and practice rhythm sense.

The player will be able to play the game through our webpage. The player statisctics will also be available on our webpage.

### **Gameplay**

The player must press the corresponding keys to the rythm of the notes that will be apearing on screen. The more accurate the clicks are the more points the player gets. If the player misses a note the combo is reset, and if the player misses several notes in rapid succession the level ends.

 <img width="607" alt="Captura de Pantalla 2022-03-09 a la(s) 13 47 10" src="https://user-images.githubusercontent.com/57450093/157521932-d8ed2dcd-26c9-42d0-961c-06e978c9db43.png">


### **Mindset**

The game aims to feel the player motivated to learn about musical rhythms, as well as improve their coordination and cognitive skills.
It is a challenging game where the user must strive to improve and reach the next levels.

## Technical

---

### **Screens**


1. Main Menu
    * Play
    * Options
2. Select Level
    * Back
    * Level Info (hover)
3. Game Screen
    * Pause (Esc key)
4. Pause Sceen
    * Resume
    * Re-try
    * Exit
5 Options
    * Back

### **Controls**

The player must click the notes to the rhytm.



## _Development_

---

### **Abstract Classes / Components**

1. Note
2. Lane

### **Derived Classes / Component Compositions**

1. GameController
2. LevelGenerator
3. Note
	1. SingleNote
	2. DoubleNote
	3. TripleNote
4. UIControler
5. Button
6. Life Bar
7. Music Sheet

## _Graphics_
--
Most game elements graphics will be made with neon colors, using sprites for the notes, music sheet, life bar.

### **Style Attributes**

The sound effects for simple actions will be minimalist sounds, emulating real life sounds (such as pressing a button) in each event that happens in the game. The more elaborate sounds will be in the notes of the main screen of the game.

It is not planned to use any specific instrument, because the game will focus on teaching rhythms, sheet music reading and coordination in general.

Moving on to the visual style, we plan to use simple and minimal sprites, in the aforementioned neon style, to reduce distractions. The sprites will be used in the notes, scores, life bar, etc.

### **Graphics Needed**

1. Background
2. Notes
	1. Single Note
	2. Double Note
	3. Triple Note
3. Lanes
4. HitArea 
5. Life bar

## _Sounds/Music_

1. Correct note hit
2. Fail note hit
3. Menu bgm?
4. Level Complete
5. Level failed
6. Menu button click

### **Sounds Needed**

1. Effects
    1. Note hit
    2. Note miss
    3. Menu button click
2. Music
    1. Menu bgm
    2. Level failed
    3. Level Comlete
    3. Pause Screen

### **Music Needed**

1. Slow-paced, nerve-racking track
2. Exciting &quot;castle&quoe track
3. Happy ending credits track

## _Schedule_

---

1. develop main user interface
    1. Main Menu
    2. Game options Menu
    3. Game Over Menu 
    
    Expected finish date: April 1
  	
2. Develop player and notes behavior
        1. Add event listeners for key presses 
        2. Move notes throught the screen
        3. Add colliders to know if a note was pressed on the correct place and time 
        4. Add sound elements to game
 
    	Expected finish date: April 8
        

5. design levels
    1. Create Game Manager class in charge of managing the state of the game.
    2. Create random-level generator function.
    3. Add all the graphics needed.
    
    Expected finish date: April 15
    
7. Web and DB integration.
    1. Link the game with the database
    2. Create web application that will host the game
    3. Deploy web App.
   
    Expected finish date: April 22
    
