# Bowling_Challenge
Notes on the game:  
	A bowling game consists of a Player  
	A player plays 10 Frames  
	A frame has pins on it  
	A player can roll a Ball on a frame  
	A frame can have a limited amount of balls rolled on it  
	A players Score is computed based on the results of the balls he has thrown (Has additional conditions depending on which frame a ball was thrown in)  

From this we can gather we need the following classes:  
	Player:  
		-Has an ID (name)  
		-Has a Score  
		-Has a record of the frames he has played  
		-Has a record of the Balls he has played  
	Frame:  
		-Has pins on it  
		-Has a record of how many shots have been played on it  
		-knows what number frame it is  
		-Can reset the amount of pins it has (If we hit a strike or spare on the 10th frame)  
	Ball:  
		-Is played on a certain Frame  
		-Can be thrown to knock pins (Method)  
		-Has a record of how many pins it knocked  
		-Knows if it was a strike or a spare  
	Score:  
		-Can determine the totalScore resulting from a series(List<>) of rolled balls  

Major design notes:    
We put the responsibility of keeping track of strikes and spares on the balls that resulted in them  
This enables the following:  
	-Every Frame can be handled by the frame class. The logic for the 10th Frame is handled outside the Frame class  
	-The only special thing about the 10th/last frame is that it allows 3 balls to be played on it, and it should be able to reset the pins if this is the case  
	-Frames do not need to worry about other frames using this logic  
	-The Score of a Player can be computed simply by knowing what balls were rolled by the Player  
		-The logic behind computing the score can be abstracted to be independent of the number of frames and pins on a frame.  
	-We push the responsibility of computing the score up, by assigning it its own class (Lowers coupling)  
Minor design note:  
	-We make a GameOptions class to define any static elements of the problems, such as the number of frames in a game, and the number of pins on a frame, in base rules of bowling were to change.  
	-We abuse a small quirk in the rules, noting that the point system does not destinguish between a strike or a spare on the second to last throw (other than the amount of pins the shot itself knocks)  
		this makes handling the 10th frame case a bit easier, but is ultimately a quirk/weakpoint.  

Some notes on the GUI:  
  -This was done very last minute  
  -This was my second time making a GUI using windowsForms, so the resulting code is quite messy  
  -Is not a representation for "Good code" in a Front-end related setting.  
	
	
	

