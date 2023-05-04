# Side-scroller Task
 A task from Orizon Group to make a 2D sidescroller.

## Requirements

* The game should have a 2D side-scrolling view.
* The player should control a character that runs forward automatically.
* The player should be able to move the character left or right by moving the mouse.
* Obstacles should randomly appear on the screen and move towards the player.
* The player should avoid the obstacles by moving left or right.
* The player should score points for each second that they survive.
* The game should end when the player collides with an obstacle.

## Theme

Decided to go with a space launch vibe. The player is
a space shuttle and dodges asteroids.

## Technicalities

* score will be incremented only when a full second has passed
* there will be a somewhat constant stream of random asteroids
* any collision with an asteroid results in death
* asteroids has varying speeds
* there will be power ups, which will be collected for immediate
or later use:
  * shield, which destroys asteroids on collision
  * blast, which destroys all asteroids in a radius
  * backwards thrusters, which slows down the movement of asteroids - 
  not implemented as it is not needed without asteroid patterns
* sfx
* vfx
* background music, synthwave
* background view, black background + particle effects
* varying asteroid sizes
* varying asteroid patterns (a series of asteroids with pre determined
timing, count, position and size) - not implemented as it would make the game less casual

## Gameplay loop

The game waits for input before starting a count down (5 seconds).
After the count down finishes the game starts and slowly accelerates to top speed.
Asteroids start to come after a set amount of time.
After death the player can start the game again by providing any input.