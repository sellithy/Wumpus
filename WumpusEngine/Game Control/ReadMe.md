# GameControl object
**[Object Planning Document](https://docs.google.com/document/d/1RWwt2DquSxO1Eg8SOsu_p8WMxbsyERVIMltQUCzdgFg/edit?usp=sharing)**

This is the GameControl Object. The job of this object is to manage the connections between the other objects and Unity. 
During the communictions between the objects, GameControl runs logic that helps run the game.

NOTES:
  1. For the controller to work correctly you need to GetMaintaned
   AND to ChooseALayout() with a certain number. This is to
   intialize all the objects needed properly
  2. For a Trivia game to work it is necessary to call one of the 
   methods under the Trivia region. This is necessary because
   these are the methods that start

## Working with Markdown syntax

_See the main WumpusEngine Readme.md_